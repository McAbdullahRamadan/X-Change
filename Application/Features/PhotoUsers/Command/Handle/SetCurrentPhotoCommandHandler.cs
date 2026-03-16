using Application.Features.PhotoUsers.Command.Models;
using Application.Responses;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Features.PhotoUsers.Command.Handle
{
    public class SetCurrentPhotoCommandHandler : IRequestHandler<SetCurrentPhotoCommand, DataResponse<string>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SetCurrentPhotoCommandHandler> _logger;

        public SetCurrentPhotoCommandHandler(
            ApplicationDbContext context,
            ILogger<SetCurrentPhotoCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<DataResponse<string>> Handle(SetCurrentPhotoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. جلب الصورة
                var photo = await _context.UserPhotos
                    .FirstOrDefaultAsync(p => p.Id == request.PhotoId, cancellationToken);

                if (photo == null)
                    return DataResponse<string>.NotFound("Photo not found");

                // 2. إلغاء تعيين أي صورة حالية من نفس النوع
                var currentPhotos = await _context.UserPhotos
                    .Where(p => p.UserId == photo.UserId && p.PhotoType == request.PhotoType && p.IsCurrent)
                    .ToListAsync(cancellationToken);

                foreach (var currentPhoto in currentPhotos)
                {
                    currentPhoto.IsCurrent = false;
                }

                // 3. تعيين الصورة الجديدة كحالية
                photo.IsCurrent = true;
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Photo {PhotoId} set as current for user {UserId}", photo.Id, photo.UserId);

                return DataResponse<string>.Success("Photo set as current successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting photo {PhotoId} as current", request.PhotoId);
                return DataResponse<string>.Failure("Failed to set photo as current");
            }
        }
    }
}
