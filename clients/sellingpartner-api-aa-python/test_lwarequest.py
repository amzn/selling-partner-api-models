import unittest
from unittest.mock import patch
from auth.LwaRequest import AccessTokenCache
from auth.LwaException import LwaException

class TestAccessTokenCache(unittest.TestCase):

    @patch('auth.LwaRequest.requests.post')
    def test_token_retrieval_success(self, mock_post):
        # Mock a successful token response
        mock_response = mock_post.return_value
        mock_response.raise_for_status.side_effect = None
        mock_response.json.return_value = {"access_token": "test_token", "expires_in": 3600}

        token_cache = AccessTokenCache()
        token = token_cache.get_lwa_access_token("client_id", "client_secret", "refresh_token")

        self.assertEqual(token, "test_token")

    @patch('auth.LwaRequest.requests.post')
    def test_token_retrieval_failure(self, mock_post):
        # Mock a failing token response
        mock_post.side_effect = Exception("Network error")

        token_cache = AccessTokenCache()
        with self.assertRaises(LwaException):
            token_cache.get_lwa_access_token("client_id", "client_secret", "refresh_token")

# Add more test cases as needed

if __name__ == '__main__':
    unittest.main()
