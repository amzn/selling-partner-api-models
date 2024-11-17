class SPAPIConfig:
    def __init__(self, client_id, client_secret, refresh_token, region="SANDBOX", access_token=None, scope=None):
        self.client_id = client_id
        self.client_secret = client_secret
        self.refresh_token = refresh_token
        self.region = region
        self.scope = scope
        self.access_token = access_token  # Initially empty, filled by LWA request method
