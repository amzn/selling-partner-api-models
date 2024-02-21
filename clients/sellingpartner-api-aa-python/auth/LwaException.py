
class LwaException(Exception):
    def __init__(self, error_code, error_message, cause=None):
        super().__init__(f"LWA Error - Code {error_code}, Message: {error_message}")
        self.error_code = error_code
        self.error_message = error_message
        self.cause = cause

    def __str__(self):
        cause_str = f", Cause: {self.cause}" if self.cause else ""
        return f"LWA Error - Code: {self.error_code}, Message: {self.error_message}{cause_str}"

    def get_error_code(self):
        return self.error_code

    def get_error_message(self):
        return self.error_message

