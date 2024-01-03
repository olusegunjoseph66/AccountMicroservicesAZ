namespace Shared.ExternalServices.DTOs
{
    public class NameAndCodeDto
    {
        public NameAndCodeDto(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public string Code { get; set; }
        public string Name { get; set; }
    }
}
