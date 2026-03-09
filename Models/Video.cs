using System.Collections.Generic;

namespace YouTubeTools.Models
{
    public class Video
    {
        public string? Id { get; set; }
        public string? ChannelTitle { get; set; }
        public string? Title { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public string? ThumbnailUrl { get; set; }
        public string? Description { get; set; }
        public string? PublishedAt { get; set; }

        public string TagsAsString => string.Join(",", Tags);
    }
}
