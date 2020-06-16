import datetime
import motor.motor_asyncio as async_motor
from bson import ObjectId

from src.utils.json_helpers import maybe_object_id

from src.services.application_service import ApplicationService
from src.services.basic_token_service import ErtisBasicTokenService
from src.services.bearer_token_service import ErtisBearerTokenService
from src.services.event_service import EventService
from src.services.password_service import PasswordService
from src.services.provider_service import ProviderService
from src.services.role_service import RoleService
from src.services.user_service import UserService
from src.services.user_type_service import UserTypeService
from src.utils.events import EventPersister


def init_startup_methods(app, settings):
    @app.listener('before_server_start')
    async def register_mongo_db(app, loop):
        # Create a database connection pool
        connection_uri = settings['mongo_connection_string']
        database_name = settings['default_database']
        app.db = async_motor.AsyncIOMotorClient(
            connection_uri,
            maxIdleTimeMS=10000,
            minPoolSize=10,
            maxPoolSize=50,
            connectTimeoutMS=10000,
            retryWrites=True,
            waitQueueTimeoutMS=10000,
            serverSelectionTimeoutMS=10000
        )[database_name]

        await migrate_db(app.db, "5e652757aac6b140f9250316", "test_membership", "5ee8b87c349db52e05d55311")

        app.application_service = ApplicationService(app.db)
        app.bearer_token_service = ErtisBearerTokenService(app.db)
        app.basic_token_service = ErtisBasicTokenService(app.db)
        app.password_service = PasswordService(app.db)
        app.role_service = RoleService(app.db)
        app.user_service = UserService(app.db)
        app.user_type_service = UserTypeService(app.db)
        app.event_service = EventService(app.db)
        app.persist_event = EventPersister(app.db)
        app.provider_service = ProviderService(
            app.db,
            app.bearer_token_service,
            app.user_type_service,
            app.user_service,
            app.persist_event
        )


async def migrate_db(db, membership_id, membership_name, admin_user_id):
    membership = await db.memberships.find_one({
        '_id': maybe_object_id(membership_id)
    })

    if not membership:
        membership_doc = {
            "_id": ObjectId(membership_id),
            "name": membership_name,
            "slug": membership_name,
            "token_ttl": 52560000,
            "refresh_token_ttl": 52560000
        }

        inserted_doc = await db.memberships.insert_one(membership_doc)
        membership_doc['_id'] = inserted_doc.inserted_id

        admin_role = await db.roles.find_one({
            'name': "admin"
        })

        if not admin_role:
            role_doc = {
                "name": "admin",
                "permissions": [
                    "users.*",
                    "applications.*",
                    "roles.*",
                    "user_types.*"
                ],
                "slug": "admin",
                "membership_id": str(membership_doc['_id']),
                "sys": {
                    "created_at": datetime.datetime.utcnow(),
                    "created_by": "system"
                }
            }

            inserted_doc = await db.roles.insert_one(role_doc)
            role_doc['_id'] = inserted_doc.inserted_id

        admin_user = await db.users.find_one({
            '_id': maybe_object_id(admin_user_id)
        })

        if not admin_user:
            user_doc = {
                "_id": ObjectId(admin_user_id),
                "status": "active",
                "username": "admin",
                "password": "$2b$12$gbhe5Vy778tTP22TlTd0.uzZTu.xk6FBSR5sMDt2HyXFeDkdMdtpW",
                "email": "admin@ertisauth.com",
                "membership_id": str(membership_doc['_id']),
                "sys": {
                    "created_at": datetime.datetime.utcnow(),
                    "created_by": "system"
                },
                "firstname": "Admin",
                "lastname": "Admin",
                "email_verified": True,
                "providers": [],
                "role": "admin",
                "token": {}
            }

            inserted_doc = await db.users.insert_one(user_doc)
            user_doc['_id'] = inserted_doc.inserted_id


