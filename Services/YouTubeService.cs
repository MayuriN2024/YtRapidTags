using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using YouTubeTools.Models;

namespace YouTubeTools.Services
{
    public class YouTubeService : IYouTubeService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;
        private readonly int _maxRelated;

        public YouTubeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["YouTube:ApiKey"];
            _maxRelated = int.Parse(configuration["YouTube:MaxRelatedVideos"] ?? "3");
        }

        public async Task<SearchVideo> SearchVideosAsync(string query)
        {
            // 1. Search for videos by title
            var searchUrl = $"https://www.googleapis.com/youtube/v3/search?part=snippet&q={Uri.EscapeDataString(query)}&type=video&maxResults={_maxRelated + 1}&key={_apiKey}";
            var searchResponse = await _httpClient.GetAsync(searchUrl);
            searchResponse.EnsureSuccessStatusCode();
            
            var searchData = await searchResponse.Content.ReadFromJsonAsync<JsonElement>();
            var videoIds = new List<string>();
            foreach (var item in searchData.GetProperty("items").EnumerateArray())
            {
                videoIds.Add(item.GetProperty("id").GetProperty("videoId").GetString()!);
            }

            if (!videoIds.Any()) return new SearchVideo();

            // 2. Get details (including tags) for these videos
            var idsParam = string.Join(",", videoIds);
            var result = new SearchVideo();
            
            var videoUrl = $"https://www.googleapis.com/youtube/v3/videos?part=snippet,statistics&id={idsParam}&key={_apiKey}";
            var videoResponse = await _httpClient.GetAsync(videoUrl);
            videoResponse.EnsureSuccessStatusCode();

            var videoData = await videoResponse.Content.ReadFromJsonAsync<JsonElement>();
            var videos = new List<Video>();

            foreach (var item in videoData.GetProperty("items").EnumerateArray())
            {
                videos.Add(MapToVideo(item));
            }

            result.PrimaryVideo = videos.FirstOrDefault();
            result.RelatedVideos = videos.Skip(1).ToList();
            
            return result;
        }

        public async Task<Video?> GetVideoDetailsAsync(string videoId)
        {
            var videoUrl = $"https://www.googleapis.com/youtube/v3/videos?part=snippet,statistics&id={videoId}&key={_apiKey}";
            var videoResponse = await _httpClient.GetAsync(videoUrl);
            videoResponse.EnsureSuccessStatusCode();

            var videoData = await videoResponse.Content.ReadFromJsonAsync<JsonElement>();
            var items = videoData.GetProperty("items").EnumerateArray();
            
            if (!items.Any()) return null;

            return MapToVideo(items.First());
        }

        private Video MapToVideo(JsonElement item)
        {
            var snippet = item.GetProperty("snippet");
            var tags = new List<string>();
            if (snippet.TryGetProperty("tags", out var tagsElement))
            {
                foreach (var tag in tagsElement.EnumerateArray())
                {
                    tags.Add(tag.GetString()!);
                }
            }

            return new Video
            {
                Id = item.GetProperty("id").GetString(),
                Title = snippet.GetProperty("title").GetString(),
                ChannelTitle = snippet.GetProperty("channelTitle").GetString(),
                Description = snippet.GetProperty("description").GetString(),
                PublishedAt = snippet.GetProperty("publishedAt").GetDateTime().ToString("dd-MMM-yyyy"),
                ThumbnailUrl = snippet.GetProperty("thumbnails").GetProperty("high").GetProperty("url").GetString(),
                Tags = tags
            };
        }
    }
}
