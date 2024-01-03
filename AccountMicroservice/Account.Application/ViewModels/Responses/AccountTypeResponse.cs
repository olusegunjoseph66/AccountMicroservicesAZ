namespace Account.Application.ViewModels.Responses
{
    public record AccountTypeResponse
    {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
