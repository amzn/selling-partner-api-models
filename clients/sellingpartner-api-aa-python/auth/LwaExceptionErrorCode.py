from enum import Enum

class LwaExceptionErrorCode(Enum):
    ACCESS_DENIED = "access_denied"
    INVALID_GRANT = "invalid_grant"
    INVALID_REQUEST = "invalid_request"
    INVALID_SCOPE = "invalid_scope"
    SERVER_ERROR = "server_error"
    TEMPORARILY_UNAVAILABLE = "temporarily_unavailable"
    UNAUTHORIZED_CLIENT = "unauthorized_client"
    INVALID_CLIENT = "invalid_client"
    OTHER = "other"
