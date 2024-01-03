using Account.Application.ViewModels.Responses.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.ViewModels.Responses
{
    public record ViewDeletionRequestResponse(IReadOnlyList<DeletionResponseDto> Requests);
}
