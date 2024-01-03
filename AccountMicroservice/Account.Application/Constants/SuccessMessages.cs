namespace Account.Application.Constants
{
    public class SuccessMessages
    {
        public const string SUCCESSFUL_ACCOUNT_REGISTRATION = "Congratulations! Your registration process has been initiated and an Otp sent to the Email {EmailAddress} or Phone Number {PhoneNumber}.";
        public const string SUCCESSFUL_REGISTRATION_COMPLETION = "Congratulations! Your registration process has been successfully completed";
        public const string SUCCESSFUL_PASSWORD_RESET_INITIATION = "Your password reset request is successful and currently being processed at the moment. Kindly validate your otp to continue..";

        public const string SUCCESSFUL_LOGIN_OF_USER = "Congratulations! you have successfully logged in.";
        public const string SUCCESSFUL_TWO_FACTOR_LOGIN_INITIATION = "Your login request has been initiated and an Otp sent to the Email {EmailAddress} or Phone Number {PhoneNumber}.";

        public const string SUCCESSFUL_RETRIEVAL_OF_USER_LIST = "The Users you have requested for have been successfully fetched.";
        public const string SUCCESSFUL_RETRIEVAL_OF_ROLE_LIST = "The Roles you have requested for have been successfully fetched.";
        public const string SUCCESSFUL_RETRIEVAL_OF_DISTRIBUTOR_ACCOUNT = "The Distributor account you have requested for has been successfully fetched.";
        public const string SUCCESSFUL_RETRIEVAL_OF_USER = "User has been retrieved successfully .";
        
        public const string SUCCESSFUL_DELETED_SAPACCOUNT = "Customer Account has been deleted successfully .";
        public const string SAP_ACCOUNT_SUCCESSFULLY_LINK = "Customer Account linking initiated.";
        
        public const string SUCCESSFUL_CREATING_OF_USER = "User has been created successfully .";
        public const string SUCCESSFUL_UPDATED_USER = "User has been updated successfully .";
        public const string SUCCESSFUL_DELETED_USER = "User has been deleted successfully .";
        public const string SUCCESSFUL_OTP_VALIDATION = "Your One Time Pin has successfully been validated.";
        public const string SUCCESSFUL_OTP_RESENDING = "Your One Time Pin has been resent to the Email {EmailAddress} or Phone Number {PhoneNumber}.";
        public const string SUCCESSFUL_USER_DELETION_REQUESTED = "Your deletion request has been successfully created.";
        public const string SUCCESSFUL_PASSWORD_RESET_COMPLETION = "Congratulations, the Password Reset process has been successfully completed.";

        public const string SUCCESSFUL_AUTO_EXPIRE_ACCOUNT = "Accounts selected successfully expired.";
        public const string SUCCESSFUL_AUTO_REFRESH_ACCOUNT = "Accounts selected successfully refreshed.";

        public const string SUCCESSFUL_DEFAULT = "Record Successfully Retrieved";
        public const string SUCCESSFUL_STATEMENT_REQUEST = "Congratulations, your statement has been successfully requested and being processed. Once completed, you will receive the statement as an attachment in your email.";
        public const string SUCCESSFUL_INVOICE_LIST_REQUEST = "Congratulations, your invoice list has been successfully requested and being processed. Once completed, you will receive the invoice list as an attachment in your email.";
        public const string SUCCESSFUL_INVOICE_REQUEST = "Congratulations, your invoice has been successfully requested and being processed. Once completed, you will receive the invoice as an attachment in your email.";
        public const string COMPANY_STATEMENT_REQUEST_NOTFOUND = "Your statement has been received but there is none available to be processed for you at the moment.";
        public const string INVOICE_REQUEST_NOTFOUND = "Your invoice request has been received but there is none available to be processed for you at the moment.";
        public const string INVOICE_LIST_REQUEST_NOTFOUND = "Your invoice list request has been received but there is none available to be processed for you at the moment.";
    }
}
