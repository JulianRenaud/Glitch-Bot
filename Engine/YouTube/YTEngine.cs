using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System;
using System.Linq.Expressions;

namespace Glitch_Bot.YouTube
{
    public class YTEngine
    {
        public string channelId = "UCr0Iij3aGgGx3uzskNBDfnQ";
        public string apiKey1 = new hiddenAPIs().YTAPIKey1();
        public string apiKey2 = new hiddenAPIs().YTAPIKey2();
        public string apiKey3 = new hiddenAPIs().YTAPIKey3();
        public string apiKey4 = new hiddenAPIs().YTAPIKey4();
        public YouTubeVideo _video = new YouTubeVideo();

        public YouTubeVideo GetLatestVideo()
        {
            
            try
            {
                string videoId;
                string videoUrl;
                string videoTitle;
                string thumbnail;
                DateTime? videoPublishedAt;

                var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = apiKey1,
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
            }
            catch (Exception ex)
            {
                try
                {
                    string videoId;
                    string videoUrl;
                    string videoTitle;
                    string thumbnail;
                    DateTime? videoPublishedAt;

                    var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                    {
                        ApiKey = apiKey2,
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
                }
                catch (Exception e)
                {
                    try
                    {
                        string videoId;
                        string videoUrl;
                        string videoTitle;
                        string thumbnail;
                        DateTime? videoPublishedAt;

                        var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                        {
                            ApiKey = apiKey3,
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
                    }
                    catch (Exception exc)
                    {
                        try
                        {
                            string videoId;
                            string videoUrl;
                            string videoTitle;
                            string thumbnail;
                            DateTime? videoPublishedAt;

                            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                            {
                                ApiKey = apiKey4,
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
                        }
                        catch (Exception exep)
                        {
                            
                        }
                    }
                }
            }
            return null;
        }


    }
}
