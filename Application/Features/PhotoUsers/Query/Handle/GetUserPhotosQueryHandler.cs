using Application.Features.PhotoUsers.Command.Models;
using Application.Features.PhotoUsers.Query.Model;
using Application.Responses;
using Domain.Entities.Business;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Features.PhotoUsers.Query.Handle
{
    public class GetUserPhotosQueryHandler : IRequestHandler<GetUserPhotosQuery, DataResponse<List<PhotoDto>>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetUserPhotosQueryHandler> _logger;

        public GetUserPhotosQueryHandler(
            ApplicationDbContext context,
            ILogger<GetUserPhotosQueryHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DataResponse<List<PhotoDto>>> Handle(GetUserPhotosQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _context.UserPhotos
                    .Where(p => p.UserId == request.UserId);

                if (request.PhotoType.HasValue)
                {
                    var photoTypeEnum = (PhotoType)request.PhotoType.Value;
                    query = query.Where(p => p.PhotoType == photoTypeEnum);
                }

                var photos = await query
                    .OrderByDescending(p => p.UploadedAt)
                    .Select(p => new PhotoDto
                    {
                        Id = p.Id,
                        PhotoUrl = p.PhotoUrl,
                        PublicId = p.PublicId,
                        PhotoType = p.PhotoType,
                        IsCurrent = p.IsCurrent,
                        UploadedAt = p.UploadedAt,
                        FileName = p.FileName,
                        FileSize = p.FileSize,
                        FormattedDate = p.UploadedAt.ToString("dd MMM yyyy")
                    })
                    .ToListAsync(cancellationToken);

                return DataResponse<List<PhotoDto>>.Success(photos, "");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting photos for user {UserId}", request.UserId);
                return DataResponse<List<PhotoDto>>.Failure("Failed to retrieve photos");
            }
        }
    }
}

