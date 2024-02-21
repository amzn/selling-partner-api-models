import unittest
from auth.LwaException import LwaException

class TestLwaException(unittest.TestCase):

    def test_exception_creation(self):
        error = LwaException("invalid_client", "Invalid client ID")
        self.assertEqual(str(error), "LWA Error - Code: invalid_client, Message: Invalid client ID")

# Add more test cases as needed

if __name__ == '__main__':
    unittest.main()
