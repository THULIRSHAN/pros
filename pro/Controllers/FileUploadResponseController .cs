using Microsoft.AspNetCore.Mvc;
using YourNamespace.Models;
using YourNamespace.DTOs;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using pro.Data;
using pro.Models;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadResponseController : ControllerBase
    {
        private readonly Context _context; // Database access
        private readonly UserManager<User> _userManager; // Register user

        public FileUploadResponseController(Context context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<ActionResult<FileUploadResponse>> UploadFile(FileUploadRequestDTO fileUploadRequest)
        {
            try
            {
                // Ensure the ModelState is valid
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Map the DTO to the FileUploadResponse model
                var fileUploadResponse = new FileUploadResponse
                {
                    FileName = fileUploadRequest.FileName,
                    FilePath = fileUploadRequest.FilePath,
                    FileSize = fileUploadRequest.FileSize,
                    ContentType = fileUploadRequest.ContentType,
                    User = user // Assign the user to the file upload response
                };

                // Add the file upload response to the database
                user.FileUploadResponses.Add(fileUploadResponse); // Add the file upload response to user's collection
                await _context.SaveChangesAsync();

                return Ok(fileUploadResponse);
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
