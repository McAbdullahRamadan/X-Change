using Application.Features.PhotoUsers.Command.Models;
using Application.Responses;
using MediatR;

namespace Application.Features.PhotoUsers.Query.Model
{
    public class GetUserPhotosQuery : IRequest<DataResponse<List<PhotoDto>>>
    {
        public string UserId { get; set; }
        public int? PhotoType { get; set; }
    }
}
