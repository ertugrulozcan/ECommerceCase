import json
from sanic import response
from src.plugins.validator import validated
from src.plugins.authorization import authorized, TokenTypes
from src.resources.generic import ensure_membership_is_exists
from src.resources.tokens.tokens import (
    CREATE_TOKEN_SCHEMA
)
from src.resources.users.users import CHANGE_PASSWORD_SCHEMA
from src.utils.json_helpers import bson_to_json


def init_login_api(app, settings):
    # region Generate Token
    @app.route('/api/v1/login', methods=['POST'])
    @validated(CREATE_TOKEN_SCHEMA)
    async def create_token(request, **kwargs):
        body = request.json
        membership_id = request.headers.get('x-ertis-alias', None)

        membership = await ensure_membership_is_exists(app.db, membership_id, user=None)

        token_result = await app.bearer_token_service.generate_token(
            settings,
            membership,
            body,
            app.persist_event
        )

        user = token_result[1]
        user.pop('token', None)

        response_model = {
            'token': token_result[0],
            'user': user
        }

        return response.json(json.loads(json.dumps(response_model, default=bson_to_json)), 201)

    # endregion