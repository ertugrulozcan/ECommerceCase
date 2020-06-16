import json
import requests
from src.utils.errors import ErtisError

FACEBOOK_SCHEMA = {
    '$schema': 'http://json-schema.org/schema#',
    'type': 'object',
    'properties': {
        'token': {
            'type': 'string'
        },
        'user_id': {
            'type': 'string'
        }
    },
    "required": [
        'token',
        'user_id'
    ],
    "additionalProperties": True
}


def validate_with_facebook(body, settings, membership_id):
    """
    {
        "id":"114182086928505",
        "first_name":"Ertuğrul",
        "last_name":"Özcan",
        "name":"Ertuğrul Özcan",
        "picture":{
            "data":{
                "height":50,
                "is_silhouette":True,
                "url":"https://scontent.fist6-1.fna.fbcdn.net/v/t1.30497-1/cp0/c15.0.50.50a/p50x50/84628273_176159830277856_972693363922829312_n.jpg?_nc_cat=1&_nc_sid=f72489&_nc_ohc=n944xujon9kAX-wHzBP&_nc_ht=scontent.fist6-1.fna&oh=22612f908913ffa83674b32ca4e5aee8&oe=5ECB4938",
                "width":50
            }
        },
        "short_name":"Ertuğrul",
        "name_format":"{first} {last}",
        "email":"aertugrulozcan@gmail.com"
    }
    """

    token = body.get("access_token")
    user_id = body.get("user_id")
    fields = [
        "id",
        "first_name",
        "last_name",
        "middle_name",
        "name",
        "picture",
        "short_name",
        "name_format",
        "email",
        "birthday"
    ]

    response = requests.get(
        "{}/{}?fields={}&access_token={}".format(settings['facebook_graph_api_url'], user_id, ",".join(fields), token)
    )

    if response.status_code != 200:
        raise ErtisError(
            err_msg="Invalid token provided.",
            err_code="errors.invalidToken",
            status_code=400
        )

    response_json = json.loads(response.text)

    if response_json['id'] != body.get('user_id'):
        raise ErtisError(
            err_code="errors.mismatchUserId",
            err_msg="User id mismatch error!",
            status_code=400
        )

    return {
        'user_id': response_json['id'],
        'email': response_json['email'],
        'email_verified': False,
        'firstname': response_json['first_name'],
        'lastname': response_json['last_name'],
        'name': response_json['name'],
        'picture': response_json['picture']['data']['url'],
        'locale': 'tr-TR',
        'membership_id': membership_id
    }


def logout_from_facebook(provider, settings):
    response = requests.get(
        "{}/{}/permissions?access_token={}".format(settings['facebook_graph_api_url'], provider.user_id, provider.token)
    )

    '''
    if response.status_code != 200:
        raise ErtisError(
            err_msg="Token could not revoked.",
            err_code="errors.facebookTokenCouldNotRevoked",
            status_code=400
        )
    '''

