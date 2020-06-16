import json
from sanic import response
from src.plugins.validator import validated
from src.plugins.authorization import authorized, TokenTypes
from src.resources.generic import ensure_membership_is_exists
from src.resources.tokens.tokens import (
    SET_PASSWORD_SCHEMA,
    CREATE_TOKEN_SCHEMA,
    REFRESH_TOKEN_SCHEMA,
    RESET_PASSWORD_SCHEMA
)
from src.resources.users.users import CHANGE_PASSWORD_SCHEMA
from src.utils.json_helpers import bson_to_json
from src.utils.errors import ErtisError

def init_token_api(app, settings):
    # region Generate Token
    @app.route('/api/v1/generate-token', methods=['POST'])
    @validated(CREATE_TOKEN_SCHEMA)
    async def create_token(request, **kwargs):
        body = request.json
        membership_id = request.headers.get('x-ertis-alias', None)

        membership = await ensure_membership_is_exists(app.db, membership_id, user=None)

        result = await app.bearer_token_service.generate_token(
            settings,
            membership,
            body,
            app.persist_event
        )

        return response.json(json.loads(json.dumps(result[0], default=bson_to_json)), 201)

    # endregion

    # region Refresh Token
    @app.route('/api/v1/refresh-token', methods=['POST'])
    @validated(REFRESH_TOKEN_SCHEMA)
    async def refresh_token(request, **kwargs):
        body = request.json
        refreshable_token = body['token']
        do_revoke = request.args.get('revoke', 'true').lower()

        refreshed_token = await app.bearer_token_service.refresh_token(
            do_revoke,
            refreshable_token,
            settings,
            app.persist_event
        )

        return response.json(json.loads(json.dumps(refreshed_token, default=bson_to_json)), 200)

    # endregion

    # region Verify Token
    @app.route('/api/v1/verify-token', methods=['POST'])
    @validated(REFRESH_TOKEN_SCHEMA)
    async def verify_token(request, **kwargs):
        body = request.json
        token = body['token']

        token_response = await app.bearer_token_service.verify_token(token, settings, app.persist_event)

        return response.json(json.loads(json.dumps(token_response, default=bson_to_json)), 200)

    # endregion

    # region Revoke Token
    @app.route('/api/v1/revoke-token', methods=['POST'])
    @validated(REFRESH_TOKEN_SCHEMA)
    async def revoke_token(request, **kwargs):
        body = request.json
        token = body['token']
        await app.bearer_token_service.revoke_token(token, settings, app.persist_event)

        # Revoke provider token
        app.provider_service.revoke_token(token, settings)

        return response.json({}, 204)

    # endregion

    # region Reset Password
    @app.route('/api/v1/reset-password', methods=['POST'])
    @validated(RESET_PASSWORD_SCHEMA)
    async def reset_password(request, **kwargs):
        membership_id = request.headers.get('x-ertis-alias', None)
        body = request.json
        await ensure_membership_is_exists(app.db, membership_id, user=None)

        user = await app.password_service.reset_password(body, membership_id, app.persist_event)
        return response.json(user['reset_password'], 200)

    # endregion

    # region Set New Password
    @app.route('/api/v1/set-password', methods=['POST'])
    @validated(SET_PASSWORD_SCHEMA)
    async def set_password(request, **kwargs):
        membership_id = request.headers.get('x-ertis-alias', None)
        body = request.json
        await ensure_membership_is_exists(app.db, membership_id, user=None)

        await app.password_service.set_password(body, membership_id, app.persist_event)
        return response.json({}, 204)

    # endregion

    # region Change Password
    @app.route('/api/v1/change-password', methods=['POST'])
    @authorized(app, settings, methods=['POST'], allowed_token_types=[TokenTypes.BEARER])
    @validated(CHANGE_PASSWORD_SCHEMA)
    async def change_password(request, **kwargs):
        user = request.ctx.utilizer
        body = request.json
        await app.password_service.change_password(body, user, app.persist_event)

        return response.json({}, 200)

    # endregion

    # region Verify Password
    @app.route('/api/v1/verify-password', methods=['POST'])
    @authorized(app, settings, methods=['POST'], allowed_token_types=[TokenTypes.BEARER])
    @validated(CREATE_TOKEN_SCHEMA)
    async def verify_password(request, **kwargs):
        user = request.ctx.utilizer
        body = request.json
        provided_password = body['password']

        if user['username'] != body['username']:
            raise ErtisError(
                status_code=403,
                err_code="errors.userCannotVerifyOtherPasswords",
                err_msg="Username mismatch for this token. The users can verify only own passwords."
            )

        app.bearer_token_service.verify_password(provided_password, user['password'])

        return response.json({ 'verified': True }, 200)

    # endregion