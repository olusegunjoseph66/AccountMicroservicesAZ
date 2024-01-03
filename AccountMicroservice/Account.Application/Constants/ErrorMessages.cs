namespace Account.Application.Constants
{
    public class ErrorMessages
    {
        public const string DEFAULT_VALIDATION_MESSAGE = "Sorry, you have supplied one or more wrong inputs. Kindly check your input and try again.";
        internal const string DEFAULT_AUTHORIZATION_MESSAGE = "Sorry, you do not have the right to perform this operation.";

        public const string SERVER_ERROR = "Sorry, we are unable to fulfill your request at the moment, kindly try again later.";
        public const string CONFLICT_ERROR = "Sorry, there seems to be a request conflict, kindly check your input and try again.";
        public const string DATABASE_CONFLICT_ERROR = "One or more unique fields already exist, kindly try again later.";
        public const string NOT_FOUND_ERROR = "Sorry, the resource you have requested for is not available at the moment.";

        public const string CANNOT_LINK_DANGOTE_ACCOUNT = "Sorry,Cannot link account  as this distributor Number has already been registered";
        public const string CANNOT_UNLINK_DANGOTE_ACCOUNT = "Sorry, Cannot unlink Dangote Account because it is the only Dangote Account linked to your profile";
        public const string DISTRIBUTOR_ACCOUNT_ALREADY_EXIST = "Sorry, the distributor account you tried to register already exist. Kindly login to continue.";
        public const string SAP_ACCOUNT_NOTFOUND = "Sorry, this account does not exist or has not been previously onboarded. Kindly contact support for further clarifications.";
        public const string SAP_ACCOUNT_INACTIVE = "Sorry, we cannot continue with your request as your account is currently inactive. Kindly contact support for further clarification.";

        public const string OTP_INVALID = "Sorry, the Otp you have requested for is invalid. Kindly check your input and try again.";
        public const string OTP_EXPIRED = "This Otp Code has expired, ensure to request for a new one.";
        public const string OTP_USED = "This Otp Code has previously been used.";
        public const string RESEND_OTP_TIME_NOT_ELAPSED = "Sorry, you cannot request for an OTP at this moment. Kindly wait for {n} minutes to elapse before requesting for another one.";
        public const string OTP_AUTHORIZED_USER_REQUIRED = "Sorry, the OTP does not have an authorized user, therefore this action cannot be completed.";

        public const string REGISTRATION_NOTFOUND = "Sorry, you have not previously registered. Kindly proceed to initiate your registration.";
        public const string REGISTRATION_PREVIOUSLY_COMPLETED = "Sorry, your registration has previously been completed. Kindly proceed to Login to continue.";

        public const string USERNAME_ALREADY_EXIST = "This username is already taken.";
        public const string PASSWORD_INVALID = "The Password does not match the format specified. You must use the combination of atleast One Alphanumeral, One Lowercase character, One Uppercase character, 1 Number and a minimum password length of 8.";

        public const string DEFAULT_ROLE_NOTFOUND = "The System Default Role cannot be found.";
        public const string ROLE_NOTFOUND = "The Role you specified does not exist.";
        public const string USERNAME_ALEADY_EXIST = "Sorry, the User details you requested already exist.";
        public const string USER_WITH_USERNAME_NOTFOUND = "Sorry, the User with the Username does not exist.";

        public const string ACCOUNT_LOCKED_RESET_PASSWORD = "Sorry, Cannot login as your account is currently locked.  Perform a password reset";
        public const string USERNAME_PASSWORD_NOT_EXIST = "Sorry, Incorrect username or password provided";
        public const string ACCOUNT_EXPIRED_RESET_PASSWORD = "Sorry, Cannot login as your password has expired.  Perform a password reset";
        public const string ACCOUNT_DISABLED_RESET_PASSWORD = "Sorry, Cannot login as your account has been disabled.  Contact Support";
        public const string PROVIDED_EMAIL_CONFLICT_WITH_ANOTHER = "Sorry, provided email address conflicts with another user’s email address";
        public const string RETRIES_EXCEEDED = "Sorry, Maximum number of retries exceeded";
        public const string UNAUTHORIZED_ACCESS = "Sorry, you do not have the permission to access the resource you are looking for.";
        public const string USER_RECORD_NOT_FOUND = "Sorry, this user does not exist.";
        public const string DANGOTE_RECORD_NOT_FOUND = "This Dangote account cannot be found";
        public const string ACCOUNT_DELETION_REQUEST_ALREADY_EXIST = "Sorry, your account deletion request already exist. Kindly wait till the operation is completed or call the Customer Support for assistance.";
        public const string USER_WITH_RESET_TOKEN_NOTFOUND = "Sorry, the user with the Password Reset Token you supplied does not exist.";
        public const string RESET_TOKEN_EXPIRED = "Sorry, the reset token has expired, kindly try initiating again.";
        public const string INVALIDATED_RECORD = "Sorry, the records required for the completion of this process has been invalidated. Kindly contact support to continue.";

        public const string SAP_ACCOUNT_INFORMATION_INCOMPLETE = "Sorry, the details attached to this account is incomplete. Kindly contact the Customer Support for further assistance.";

        public const string INCORRECT_COUNTRY_CODE = "Sorry, you have supplied a wrong country code.";
        public const string INCORRECT_COMPANY_CODE = "Sorry, you have supplied a wrong company code.";

        public const string INVALID_FILE_DOWNLOAD_FORMAT = "Sorry, you have selected an invalid format for downloading this file";
        public const string INVALID_FILE_UPLOAD_FORMAT = "Sorry, you have supplied an invalid file format for the file to be uploaded.";
        public const string COMPANY_STATEMENT_UNAVAILABLE = "Sorry, the statement is not available for accounts tied to this company at the moment.";
        public const string COMPANY_STATEMENT_REQUEST_FAILURE = "Sorry, we cannot process your statement request at the moment, kindly try again later.";

        public const string COMPANY_NOT_FOUND = "Sorry, the company you have selected does not exist.";

        public const string INVOICE_REQUEST_FAILURE = "Sorry, we cannot process your statement request at the moment, kindly try again later.";
        public const string INVOICE_LIST_REQUEST_FAILURE = "Sorry, we cannot process your statement request at the moment, kindly try again later.";
        public const string UNABLE_TO_UPDATE_USER_PROFILE = "Sorry, we are unable to update your profile at the moment. Do check your input and try again later.";
        public const string PASSWORD_POLICY_VIOLATION = "Sorry you cannot reuse your last {number} passwords. Please choose a new password.";

        public const string PRIVACY_POLICY_NOT_ACCEPTED = "Sorry, we cannot complete your request because you have not accepted the Privacy Policy. Kindly accept the privacy policy and try again.";

        public const string PASSWORD_COMBINATION_INVALID = "Sorry, your password cannot contain your UserName. Kindly check and try again.";
    }
}
