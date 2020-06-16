from functools import wraps
from jsonschema import ValidationError, validate

from src.utils.errors import ErtisError


def validate_by_schema(payload, schema):
    try:
        validate(payload, schema)
    except ValidationError as e:
        raise ErtisError(
            err_code="errors.validationError",
            err_msg=str(e.message),
            status_code=400,
            context={
                'required': e.schema.get('required', []),
                'properties': e.schema.get('properties', {})
            }
        )


def validated(schema):
    def decorator(f):
        @wraps(f)
        async def validate_payload_by_schema(request, *args, **kwargs):
            payload = request.json
            validate_by_schema(payload, schema)
            response = await f(request, *args, **kwargs)
            return response

        return validate_payload_by_schema

    return decorator