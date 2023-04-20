using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;

namespace Glitch_Bot.YouTube
{
    public class YTEngine
    {
        public string channelId = "UCr0Iij3aGgGx3uzskNBDfnQ";
        public string apiKey = new hiddenAPIs().YTAPIKey1();
        public YouTubeVideo _video = new YouTubeVideo();

        public YouTubeVideo GetLatestVideo()
        {
            string videoId;
            string videoUrl;
            string videoTitle;
            string thumbnail;
            DateTime? videoPublishedAt;

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = "MyApplication"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = channelId;
            searchListRequest.MaxResults = 1;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Date;

            var searchListResponse = searchListRequest.Execute();

            foreach (var searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    videoId = searchResult.Id.VideoId;
                    videoUrl = $"https://www.youtube.com/watch?v={videoId}";
                    videoTitle = searchResult.Snippet.Title;
                    videoPublishedAt = (DateTime)searchResult.Snippet.PublishedAt;
                    thumbnail = searchResult.Snippet.Thumbnails.Default__.Url;

                    return new YouTubeVideo()
                    {
                        videoId = videoId,
                        videoUrl = videoUrl,
                        videoTitle = videoTitle,
                        thumbnail = thumbnail,
                        PublishedAt = (DateTime)videoPublishedAt
                    };
                }
            }

            return null;
        }
    }
}
