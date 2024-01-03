using Account.Application.Constants;
using Account.Application.DTOs.APIDataFormatters;
using System.Globalization;

namespace Account.Application.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException() : base()
        {
            Response = ResponseHandler.FailureResponse(ErrorCodes.SERVER_ERROR_CODE, ErrorMessages.SERVER_ERROR);
        }

        public InternalServerException(string message) : base(message)
        {
            Response = ResponseHandler.FailureResponse(ErrorCodes.SERVER_ERROR_CODE, message);
        }

        public ApiResponse Response { get; private set; }

        public InternalServerException(string message, string code, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
            Response = ResponseHandler.FailureResponse(code, message);
        }
    }
}
