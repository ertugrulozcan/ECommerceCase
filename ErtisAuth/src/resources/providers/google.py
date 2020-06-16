import json
import requests
from src.utils.errors import ErtisError
from src.utils.json_helpers import _parse_boolean


GOOGLE_SCHEMA = {
    '$schema': 'http://json-schema.org/schema#',
    'type': 'object',
    'properties': {
        'access_token': {
            'type': 'string'
        },
        'id_token': {
            'type': 'string'
        }
    },
    "required": [
        'access_token',
        'id_token'
    ],
    "additionalProperties": True
}


def validate_with_google(body, settings, membership_id):
    token = body.get('access_token')

    response = requests.get(
        "{}?id_token={}".format(settings['google_token_validator_url'], token)
    )

    if response.status_code != 200:
        raise ErtisError(
            err_msg="Invalid token provided. ",
            err_code="errors.invalidToken",
            status_code=400
        )

    response_json = json.loads(response.text)

    if response_json['sub'] != body.get('user_id'):
        raise ErtisError(
            err_code="errors.mismatchUserId",
            err_msg="User id mismatch error!",
            status_code=400
        )

    return {
        'user_id': response_json['sub'],
        'email': response_json['email'],
        'email_verified': _parse_boolean(response_json['email_verified']),
        'firstname': response_json['given_name'],
        'lastname': response_json['family_name'],
        'name': response_json['name'],
        'picture': response_json['picture'],
        'locale': response_json['locale'],
        'membership_id': membership_id
    }


def logout_from_google(provider, settings):
    response = requests.get(
        "{}/revoke?token={}".format(settings['google_oauth2_url'], provider.token)
    )

    '''
    if response.status_code != 200:
        raise ErtisError(
            err_msg="Token could not revoked.",
            err_code="errors.googleTokenCouldNotRevoked",
            status_code=400
        )
    '''
