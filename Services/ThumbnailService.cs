using System.Text.RegularExpressions;

namespace YouTubeTools.Services
{
    public class ThumbnailService
    {
        public string? ExtractVideoId(string url)
        {
            if (Regex.IsMatch(url, @"^[a-zA-Z0-9_-]{11}$"))
            {
                return url;
            }

            var patterns = new[]
            {
                @"(?:https?:\/\/)?(?:www\.)?youtube\.com\/watch\?v=([a-zA-Z0-9_-]{11})",
                @"(?:https?:\/\/)?(?:www\.)?youtu\.be\/([a-zA-Z0-9_-]{11})",
                @"(?:https?:\/\/)?(?:www\.)?youtube\.com\/embed\/([a-zA-Z0-9_-]{11})"
            };

            foreach (var pattern in patterns)
            {
                var match = Regex.Match(url, pattern);
                if (match.Success)
                {
                    return match.Groups[1].Value;
                }
            }

            return null;
        }
    }
}
