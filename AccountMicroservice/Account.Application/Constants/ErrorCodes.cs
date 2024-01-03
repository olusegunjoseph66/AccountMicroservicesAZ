namespace Account.Application.Constants
{
    public class ErrorCodes
    {
        public const string INCOMPLETE_INPUT = "Error-A-01";
        public const string DEFAULT_VALIDATION_CODE = "Error-A-02";
        public const string USERNAME_ALEADY_EXIST = "Error-A-03";
        public const string EMAIL_ALEADY_EXIST = "Error-A-04";
        public const string ACCOUNT_LOCKED_RESET_PASSWORD = "Error-A-13";
        public const string USERNAME_PASSWORD_NOT_EXIST = "Error-A-12";
        public const string ACCOUNT_EXPIRED_RESET_PASSWORD = "Error-A-14";
        public const string ACCOUNT_DISABLED_RESET_PASSWORD = "Error-A-15";
        public const string PROVIDED_EMAIL_CONFLICT_WITH_ANOTHER = "Error-A-16";
        public const string RETRIES_EXCEEDED = "Error-A-21";
        public const string UNAUTHORIZED_ACCESS = "Error-A-28";
        public const string DANGOTE_RECORD_NOT_FOUND = "Error-A-32";
        public const string CANNOT_LINK_DANGOTE_ACCOUNT = "Error-A-30";
        public const string CANNOT_UNLINK_DANGOTE_ACCOUNT = "Error-A-31";
        public const string USER_RECORD_NOT_FOUND = "Error-A-29";
        public const string SERVER_ERROR_CODE = "E04";
        public const string NOTFOUND_ERROR_CODE = "E05";
        public const string CONFLICT_ERROR_CODE = "E06";
        public const string SERVER_CONFIGURATION_ERROR_CODE = "E07";
        public const string DEFAULT_AUTHORIZATION_CODE = "E02";
        public const string DATABASE_INSERT_CONFLICT_CODE = "E03";
        public const string OTP_AUTHORIZED_USER_REQUIRED_CODE = "E08";
        public const string FAILED_SAP_REQUEST_CODE = "E08";

        public const int SqlServerViolationOfUniqueIndex = 2601;
        public const int SqlServerViolationOfUniqueConstraint = 2627;

        public const string DISTRIBUTOR_ACCOUNT_ALREADY_EXIST_CODE = "Error-A-19";
        public const string SAP_ACCOUNT_NOTFOUND_CODE = "Error-A-05";
        public const string SAP_ACCOUNT_INACTIVE_CODE = "Error-A-06";
        public const string SAP_ACCOUNT_INFORMATION_INCOMPLETE_CODE = "Error-A-33";
        public const string OTP_NOTFOUND_CODE = "Error-A-07";
        public const string OTP_EXPIRED_CODE = "Error-A-08";
        public const string OTP_ALREADY_VALIDATED_CODE = "Error-A-20";
        public const string OTP_ALREADY_INVALIDATED_CODE = "Error-A-21";
        public const string OTP_MISMATCH_CODE = "Error-A-10";

        public const string REGISTRATION_NOTFOUND_CODE = "Error-A-22";
        public const string REGISTRATION_PREVIOUSLY_COMPLETED_CODE = "Error-A-23";

        public const string USER_WITH_USERNAME_NOTFOUND_CODE = "Error-A-09";
        public const string USERNAME_ALREADY_EXIST_CODE = "Error-A-24";
        public const string PASSWORD_INVALID_CODE = "Error-A-25";
        public const string RESEND_OTP_TIME_NOT_ELAPSED_CODE = "Error-A-18";
        public const string USER_WITH_RESET_TOKEN_NOTFOUND_CODE = "Error-A-26";
        public const string RESET_TOKEN_EXPIRED_CODE = "Error-A-27";
        public const string COMPANY_STATEMENT_UNAVAILABLE_CODE = "Error-A-35";
        public const string PASSWORD_POLICY_VIOLATION_CODE = "Error-A-36";
        public const string COMPANY_NOT_FOUND = "Error-A-37";
        public const string PRIVACY_POLICY_NOT_ACCEPTED = "Error-A-38";
        public const string PASSWORD_COMBINATION_INVALID = "Error-A-39";
    }
}
