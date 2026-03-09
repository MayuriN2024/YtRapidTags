using System.Collections.Generic;
using System.Linq;

namespace YouTubeTools.Models
{
    public class SearchVideo
    {
        public Video? PrimaryVideo { get; set; }
        public List<Video> RelatedVideos { get; set; } = new List<Video>();

        public string AllTagsAsString => string.Join(",", 
            (PrimaryVideo?.Tags ?? new List<string>())
            .Concat(RelatedVideos.SelectMany(v => v.Tags))
            .Distinct());
    }
}
