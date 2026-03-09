using System.Threading.Tasks;
using YouTubeTools.Models;

namespace YouTubeTools.Services
{
    public interface IYouTubeService
    {
        Task<SearchVideo> SearchVideosAsync(string query);
        Task<Video?> GetVideoDetailsAsync(string videoId);
    }
}
