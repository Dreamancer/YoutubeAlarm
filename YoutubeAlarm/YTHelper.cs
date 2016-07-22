using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Threading.Tasks;

namespace YoutubeAlarm
{
    public static class YTHelper
    {
        static WebClient wClient = new WebClient();
        private static string _apiKey = "AIzaSyDbo9Mx7Vap6AgC17D6Jswy7jAtcVwf3jw";
        private static string _appName = "YoutubeAlarmClock";
        private static YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
        {
            ApiKey = _apiKey,
            ApplicationName = _appName
        });

        public static async Task<List<string>> Search(string keyword, int maxResults)
        {
           var request = youtubeService.Search.List("snippet");
            request.Q = keyword;
            request.
            request.MaxResults = maxResults;

            SearchListResponse response = await request.ExecuteAsync();

            List<string> videos = new List<string>();

            foreach(var result in response.Items)
            {
                if(result.Id.Kind == "youtube#video")
                    videos.Add(String.Format("{0} ({1})", result.Snippet.Title, result.Id.VideoId));
            }

            return videos;
        }


    }
}