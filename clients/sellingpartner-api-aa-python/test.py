if __name__ == "__main__":
    print("Starting the Script...")

    from auth.credentials import SPAPIConfig
    # User inputs their credentials in the config
    config = SPAPIConfig(
        client_id="Your Client-id",
        client_secret="Your Client-secret",
        refresh_token="Your Refresh-token",
        region="SANDBOX",  # Possible values NA, EU, FE, and SANDBOX
        scope = None # Required for grant_type='client_credentials' ; Possible values "sellingpartnerapi::notifications" and "sellingpartnerapi::migration"
    )

    from spapi.spapiclient import SPAPIClient

    # Create the API Client
    print("Config and client initialized...")
    api_client = SPAPIClient(config)

    marketplace_ids = ["ATVPDKIKX0DER"]
    created_after = "2024-01-19T12:34:56.789012"

    orders_api = api_client.get_api_client('OrdersV0Api')
    orders_response = orders_api.get_orders(marketplace_ids=marketplace_ids, created_after=created_after)
    print("Orders API Response:")
    print(orders_response)

