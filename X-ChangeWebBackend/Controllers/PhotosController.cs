using Application.Features.PhotoUsers.Command.Models;
using Application.Features.PhotoUsers.Query.Model;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities.Business;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using X_ChangeWebBackend.BaseControl;

namespace X_ChangeWebBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : BaseController
    {
        private readonly IPhotoService _photoService;
        private readonly ILogger<PhotosController> _logger;
        private readonly ApplicationDbContext _context;

        public PhotosController(
            IPhotoService photoService,
            ILogger<PhotosController> logger, ApplicationDbContext context)
        {
            _photoService = photoService;
            _logger = logger;
            _context = context;
        }
        [HttpGet("user/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserPhotos(string userId, [FromQuery] int? photoType)
        {
            try
            {
                var query = _context.UserPhotos
                    .Where(p => p.UserId == userId);

                if (photoType.HasValue)
                {
                    var photoTypeEnum = (PhotoType)photoType.Value;
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
                    .ToListAsync();

                return Ok(DataResponse<List<PhotoDto>>.Success(photos, ""));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting photos for user {UserId}", userId);
                return StatusCode(500, DataResponse<List<PhotoDto>>.Failure("Failed to retrieve photos"));
            }
        }
        [HttpGet("my-photos")]
        [Authorize]
        public async Task<IActionResult> GetMyPhotos([FromQuery] PhotoType? photoType = null)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var query = new GetUserPhotosQuery { UserId = userId, PhotoType = (int?)photoType };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpPut("set-current/{photoId}")]
        [Authorize]
        public async Task<IActionResult> SetCurrentPhoto(int photoId, [FromQuery] int photoType)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var command = new SetCurrentPhotoCommand
            {
                PhotoId = photoId,
                PhotoType = (PhotoType)photoType,
                UserId = userId
            };

            var result = await Mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> UploadPhoto(IFormFile file, [FromQuery] PhotoType photoType)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var command = new UploadPhotoCommand
                {
                    File = file,
                    PhotoType = photoType,
                    UserId = userId
                };

                var result = await Mediator.Send(command);

                // ✅ دايمًا رجع 200 مع isSuccess
                return Ok(result);
            }
            catch (Exception ex)
            {
                // ✅ لو في خطأ، رجع 200 برضو مع isSuccess = false
                return Ok(new { isSuccess = false, error = ex.Message });
            }
        }

        [HttpDelete("{publicId}")]
        [Authorize]
        public async Task<IActionResult> DeletePhoto(string publicId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();

                var command = new DeletePhotoCommand
                {
                    PublicId = publicId,
                    UserId = userId
                };

                var result = await Mediator.Send(command);

                if (result.Succeeded)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting photo");
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }
}
