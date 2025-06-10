import os
import sys
import logging
import backoff
from requests.exceptions import HTTPError

#Update Path
sys.path.append('path_to_folder/SellingPartnerAPIAuthAndAuthPython/client/orders')
from swagger_client.configuration import Configuration
from swagger_client.api_client import ApiClient

#Update Path
sys.path.append('path_to_folder/SellingPartnerAPIAuthAndAuthPython')
from auth.LwaRequest import AccessTokenCache

logging.basicConfig(level=logging.INFO)

def is_rate_limit_error(e):
    """Check if the exception is a rate limit error (HTTP 429)."""
    return isinstance(e, HTTPError) and e.response.status_code == 429

class SPAPIClient:
    region_to_endpoint = {
        "NA": "https://sellingpartnerapi-na.amazon.com",
        "EU": "https://sellingpartnerapi-eu.amazon.com",
        "FE": "https://sellingpartnerapi-fe.amazon.com",
        "SANDBOX": "https://sandbox.sellingpartnerapi-na.amazon.com"
    }

    def __init__(self, config):
        self.config = config
        self.api_base_url = self.region_to_endpoint.get(config.region)
        self.access_token_cache = AccessTokenCache()
        self.api_client = None
        self._initialize_client()

    def _initialize_client(self):
        logging.debug("Initializing API Client...")

        access_token = self.access_token_cache.get_lwa_access_token(
            client_id=self.config.client_id,
            client_secret=self.config.client_secret,
            refresh_token=self.config.refresh_token
        )
        configuration = Configuration()
        configuration.host = self.api_base_url
        configuration.access_token = access_token

        self.api_client = ApiClient(configuration=configuration)

    @backoff.on_exception(backoff.expo,
                          HTTPError,
                          giveup=is_rate_limit_error,
                          max_tries=5,
                          on_giveup=lambda e: logging.error(f"Too Many Retries: {e}"))

    def get_api_client(self, api_name):
        try:
            module = __import__('swagger_client.api', fromlist=[api_name])
            api_class = getattr(module, api_name)
            return api_class(self.api_client)
        except AttributeError:
            raise Exception(f"API client for {api_name} not found.")