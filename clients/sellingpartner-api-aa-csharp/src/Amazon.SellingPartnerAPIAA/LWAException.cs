using System;
namespace Amazon.SellingPartnerAPIAA
{
	public class LWAException:Exception
	{
        private String errorMessage;

        private String errorCode;

        public LWAException():base()
        {

        }

        public LWAException(String errorCode, String errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
        }

        public LWAException(String errorCode, String errorMessage, String message): base(message)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
        }

        public LWAException(String errorCode, String errorMessage, String message, Exception innerException) : base(message,innerException)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
        }


        public String getErrorCode()
        {
            return this.errorCode;
        }

        public String getErrorMessage()
        {
            return this.errorMessage;
        }

    }
}

