using Microsoft.AspNetCore.Mvc;
using YouTubeTools.Services;

namespace YouTubeTools.Controllers
{
    [Route("youtube")]
    public class YouTubeTagsController : Controller
    {
        private readonly IYouTubeService _youTubeService;

        public YouTubeTagsController(IYouTubeService youTubeService)
        {
            _youTubeService = youTubeService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(string videoTitle)
        {
            if (string.IsNullOrEmpty(videoTitle))
            {
                ViewData["Error"] = "Video Title is Required";
                return View("~/Views/Home/Index.cshtml");
            }

            try
            {
                var result = await _youTubeService.SearchVideosAsync(videoTitle);
                return View("~/Views/Home/Index.cshtml", result);
            }
            catch (Exception ex)
            {
                ViewData["Error"] = ex.Message;
                return View("~/Views/Home/Index.cshtml");
            }
        }
    }
}
