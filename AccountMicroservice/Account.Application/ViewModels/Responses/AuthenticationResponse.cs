using Account.Application.ViewModels.Responses.ResponseDto;

namespace Account.Application.ViewModels.Responses
{
    public record AuthenticationResponse(string BearerToken, UserDto user);
}
