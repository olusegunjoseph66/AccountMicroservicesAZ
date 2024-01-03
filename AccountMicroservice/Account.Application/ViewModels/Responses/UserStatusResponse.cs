namespace Account.Application.ViewModels.Responses
{
    public record UserStatusResponse
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
