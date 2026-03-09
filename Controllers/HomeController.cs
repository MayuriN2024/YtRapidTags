using Microsoft.AspNetCore.Mvc;
using YouTubeTools.Services;

namespace YouTubeTools.Controllers
{
    public class HomeController : Controller
    {
        private readonly IYouTubeService _youTubeService;
        private readonly ThumbnailService _thumbnailService;

        public HomeController(IYouTubeService youTubeService, ThumbnailService thumbnailService)
        {
            _youTubeService = youTubeService;
            _thumbnailService = thumbnailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Thumbnails()
        {
            return View();
        }

        public IActionResult VideoDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FetchVideoDetails(string videoUrlOrId)
        {
            var videoId = _thumbnailService.ExtractVideoId(videoUrlOrId);
            if (string.IsNullOrEmpty(videoId))
            {
                ViewData["Error"] = "Invalid YouTube URL or ID";
                return View("VideoDetails");
            }

            try
            {
                var details = await _youTubeService.GetVideoDetailsAsync(videoId);
                if (details == null)
                {
                    ViewData["Error"] = "Video not found";
                    return View("VideoDetails");
                }
                ViewData["VideoUrlOrId"] = videoUrlOrId;
                return View("VideoDetails", details);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View("VideoDetails");
            }
        }
    }
}
