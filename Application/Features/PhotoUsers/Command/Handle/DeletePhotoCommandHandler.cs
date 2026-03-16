using Application.Features.PhotoUsers.Command.Models;
using Application.Interfaces;
using Application.Responses;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Features.PhotoUsers.Command.Handle
{
    public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, DataResponse<string>>
    {
        private readonly IPhotoService _photoService;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DeletePhotoCommandHandler> _logger;

        public DeletePhotoCommandHandler(
            IPhotoService photoService,
            ApplicationDbContext context,
            ILogger<DeletePhotoCommandHandler> logger)
        {
            _photoService = photoService;
            _context = context;
            _logger = logger;
        }

        public async Task<DataResponse<string>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. التحقق من إن الصورة بتاعة المستخدم
                var photo = await _context.UserPhotos
                    .FirstOrDefaultAsync(p => p.PublicId == request.PublicId && p.UserId == request.UserId);

                if (photo == null)
                    return DataResponse<string>.NotFound("Photo not found");

                // 2. حذف من قاعدة البيانات
                _context.UserPhotos.Remove(photo);
                await _context.SaveChangesAsync(cancellationToken);

                // 3. حذف من wwwroot
                await _photoService.DeletePhotoAsync(request.PublicId);

                _logger.LogInformation("Photo deleted successfully: {PublicId}", request.PublicId);
                return DataResponse<string>.Success("Photo deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting photo: {PublicId}", request.PublicId);
                return DataResponse<string>.Failure("Failed to delete photo");
            }
        }
    }
}
