using Application.Responses;
using MediatR;

namespace Application.Features.PhotoUsers.Command.Models
{
    public class DeletePhotoCommand : IRequest<DataResponse<string>>
    {
        public string PublicId { get; set; }
        public string UserId { get; set; }
    }
}
