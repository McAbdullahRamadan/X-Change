using Application.Responses;
using Domain.Entities.Business;
using MediatR;

namespace Application.Features.PhotoUsers.Command.Models
{
    public class SetCurrentPhotoCommand : IRequest<DataResponse<string>>
    {
        public int PhotoId { get; set; }
        public PhotoType PhotoType { get; set; }
        public string UserId { get; set; }
    }
}

