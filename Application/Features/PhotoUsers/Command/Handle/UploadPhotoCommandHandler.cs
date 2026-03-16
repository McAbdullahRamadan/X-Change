using Application.Features.PhotoUsers.Command.Models;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities.Business;
using Domain.Entities.System;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Features.PhotoUsers.Command.Handle
{
    public class UploadPhotoCommandHandler : IRequestHandler<UploadPhotoCommand, DataResponse<PhotoDto>>
    {
        private readonly IPhotoService _photoService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<UploadPhotoCommandHandler> _logger;

        public UploadPhotoCommandHandler(
            IPhotoService photoService,
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            ILogger<UploadPhotoCommandHandler> logger)
        {
            _photoService = photoService;
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public async Task<DataResponse<PhotoDto>> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. التحقق من المستخدم
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user == null)
                    return DataResponse<PhotoDto>.NotFound("User not found");

                // 2. حفظ الصورة في wwwroot عبر PhotoService
                var uploadResult = await _photoService.AddPhotoAsync(request.File,
                    request.PhotoType == PhotoType.Profile ? "profiles" : "covers");

                // 3. إنشاء سجل في قاعدة البيانات
                var userPhoto = new UserPhoto
                {
                    UserId = request.UserId,
                    PhotoUrl = uploadResult.Url,
                    PublicId = uploadResult.PublicId,
                    PhotoType = request.PhotoType,
                    IsCurrent = false,
                    UploadedAt = DateTime.UtcNow,
                    FileName = request.File.FileName,
                    FileSize = request.File.Length
                };

                // 4. حفظ في قاعدة البيانات
                await _context.UserPhotos.AddAsync(userPhoto, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Photo saved - Local: {LocalPath}, DB: {PhotoId}",
                    uploadResult.PublicId, userPhoto.Id);

                // 5. تحويل النتيجة
                var photoDto = new PhotoDto
                {
                    Id = userPhoto.Id,
                    PhotoUrl = userPhoto.PhotoUrl,
                    PublicId = userPhoto.PublicId,
                    PhotoType = userPhoto.PhotoType,
                    IsCurrent = userPhoto.IsCurrent,
                    UploadedAt = userPhoto.UploadedAt,
                    FileName = userPhoto.FileName
                };

                return DataResponse<PhotoDto>.Success(photoDto, "Photo uploaded successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in photo upload handler");
                return DataResponse<PhotoDto>.Failure("Failed to upload photo");
            }
        }
    }
}
