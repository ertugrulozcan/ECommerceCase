import json

from sanic import response

from src.plugins.validator import validate_by_schema
from src.resources.generic import ensure_membership_is_exists
from src.resources.providers.facebook import validate_with_facebook, FACEBOOK_SCHEMA
from src.resources.providers.google import validate_with_google, GOOGLE_SCHEMA
from src.utils.errors import ErtisError
from src.utils.json_helpers import bson_to_json

VALIDATION_LOOKUP = {
    'google': validate_with_google,
    'facebook': validate_with_facebook
}

BODY_VALIDATION_LOOKUP = {
    'google': GOOGLE_SCHEMA,
    'facebook': FACEBOOK_SCHEMA
}


def init_provider_sign_in_api(app, settings):
    @app.route('/api/v1/sign-in/<provider_slug>', methods=["POST"])
    async def sign_in_with(request, provider_slug, *args, **kwargs):
        membership_id = request.headers.get('x-ertis-alias', None)
        membership = await ensure_membership_is_exists(app.db, membership_id, user=None)

        exists_provider = await app.provider_service.get_provider_by_slug(provider_slug, str(membership['_id']))

        validation_method = VALIDATION_LOOKUP.get(exists_provider['type'], None)
        if not validation_method:
            raise ErtisError(
                err_code="Provider not in provider validation lookup",
                err_msg="errors.providerNotExists",
                status_code=400
            )

        body = request.json
        validate_by_schema(BODY_VALIDATION_LOOKUP[exists_provider['type']], body)

        user = validation_method(body, settings, str(membership['_id']))
        exists_user = await app.provider_service.check_user(user)
        if exists_user:
            affected_user = await app.provider_service.update_user(user, exists_user, exists_provider, body)
        else:
            affected_user = await app.provider_service.create_user(user, exists_provider, body)

        result = await app.bearer_token_service.generate_token(
            settings,
            membership,
            affected_user,
            app.persist_event,
            skip_auth=True
        )

        result[1].pop('token', None)
        response_model = {
            'token': result[0],
            'user': result[1]
        }

        return response.json(json.loads(json.dumps(response_model, default=bson_to_json)), 201)
