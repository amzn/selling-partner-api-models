import requests
import time
import logging
import sys

#Update path
sys.path.append("path_to_folder/SellingPartnerAPIAuthAndAuthPython")

from auth.LwaException import LwaException
from auth.LwaExceptionErrorCode import LwaExceptionErrorCode

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

class AccessTokenCache:
    def __init__(self, max_retries=3):
        self.token_info = None
        self.max_retries = max_retries

    def get_lwa_access_token(self, client_id, client_secret, refresh_token=None, grant_type="refresh_token", scope=None):
        if self.token_info and time.time() < self.token_info["expires_at"]:
            return self.token_info["access_token"]
        return self.request_new_token(client_id, client_secret, refresh_token, grant_type, scope)


    def request_new_token(self, client_id, client_secret, refresh_token, grant_type, scope):
        self.validate_token_request(grant_type, refresh_token, scope)
        data = self.prepare_token_request_data(client_id, client_secret, refresh_token, grant_type, scope)

        retries = 0
        while retries <= self.max_retries:
            try:
                response = requests.post("https://api.amazon.com/auth/o2/token", data=data)
                response.raise_for_status()
                token_response = response.json()
                token_response["expires_at"] = time.time() + token_response.get("expires_in", 1800) - 30
                self.token_info = token_response
                return token_response["access_token"]
            except requests.RequestException as e:
                if retries < self.max_retries:
                    retries += 1
                    time.sleep(2 ** retries)
                    continue
                error_code = self.map_http_status_to_lwa_exception_code(e.response.status_code if e.response else None)
                error_message = f"Token request failed with status code {e.response.status_code}: {e.response.text}" if e.response else "Token request failed."
                logger.error(error_message)
                raise LwaException(error_code, error_message) from e


    def validate_token_request(self, grant_type, refresh_token, scope):
        if grant_type == "refresh_token" and not refresh_token:
            raise LwaException(LwaExceptionErrorCode.INVALID_REQUEST.value, "Refresh token must be provided for grant_type 'refresh_token'")
        if grant_type == "client_credentials" and not scope:
            raise LwaException(LwaExceptionErrorCode.INVALID_SCOPE.value, "Scope must be provided for grant_type 'client_credentials'")


    def prepare_token_request_data(self, client_id, client_secret, refresh_token, grant_type, scope):
        data = {
            "client_id": client_id,
            "client_secret": client_secret,
            "grant_type": grant_type
        }
        if grant_type == "refresh_token":
            if not refresh_token:
                raise LwaException(LwaExceptionErrorCode.INVALID_REQUEST.value, "Refresh token must be provided for grant_type 'refresh_token'")
            data["refresh_token"] = refresh_token
        elif grant_type == "client_credentials":
            if not scope:
                raise LwaException(LwaExceptionErrorCode.INVALID_SCOPE.value, "Scope must be provided for grant_type 'client_credentials'")
            data["scope"] = scope
        return data

    def is_retriable(self, error_code):
        retriable_codes = [
            LwaExceptionErrorCode.SERVER_ERROR.value,
            LwaExceptionErrorCode.TEMPORARILY_UNAVAILABLE.value
        ]
        return error_code in retriable_codes

    def format_error_message(self, e):
        return f"Token request failed with status code {e.response.status_code}: {e.response.text}" if e.response else f"Token request failed: {e}"

    def map_http_status_to_lwa_exception_code(self, status_code):
        if status_code is None:
            return LWAExceptionErrorCode.SERVER_ERROR.value
        if status_code == 400:
            return LWAExceptionErrorCode.INVALID_REQUEST.value
        if status_code == 401:
            return LWAExceptionErrorCode.UNAUTHORIZED_CLIENT.value
        if status_code == 403:
            return LWAExceptionErrorCode.ACCESS_DENIED.value
        if status_code == 500:
            return LWAExceptionErrorCode.SERVER_ERROR.value
        if status_code == 503:
            return LWAExceptionErrorCode.TEMPORARILY_UNAVAILABLE.value
        return LWAExceptionErrorCode.OTHER.value
