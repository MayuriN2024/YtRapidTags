using Microsoft.AspNetCore.Mvc;
using YouTubeTools.Services;

namespace YouTubeTools.Controllers
{
    public class ThumbnailController : Controller
    {
        private readonly ThumbnailService _service;

        public ThumbnailController(ThumbnailService service)
        {
            _service = service;
        }

        [HttpPost("/get-thumbnail")]
        public IActionResult ShowThumbnail(string videoUrlOrId)
        {
            var videoId = _service.ExtractVideoId(videoUrlOrId);
            if (videoId == null)
            {
                ViewData["Error"] = "Invalid YouTube URL";
                return View("~/Views/Home/Thumbnails.cshtml");
            }

            ViewData["ThumbnailUrl"] = $"https://img.youtube.com/vi/{videoId}/maxresdefault.jpg";
            ViewData["VideoId"] = videoId;
            return View("~/Views/Home/Thumbnails.cshtml");
        }
    }
}
