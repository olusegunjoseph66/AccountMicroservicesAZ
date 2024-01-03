using Account.Application.Constants;
using Account.Application.DTOs.APIDataFormatters;
using System.Globalization;

namespace Account.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException() : base()
        {
            Response = ResponseHandler.FailureResponse(ErrorCodes.CONFLICT_ERROR_CODE, ErrorMessages.CONFLICT_ERROR);
        }

        public ConflictException(string message) : base(message)
        {
            Response = ResponseHandler.FailureResponse(ErrorCodes.CONFLICT_ERROR_CODE, message);
        }

        public ApiResponse Response { get; private set; }

        public ConflictException(string message, string code, params object[] args) : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
            Response = ResponseHandler.FailureResponse(code, message);
        }
    }
}
