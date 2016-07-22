using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using YoutubeExtractor;

namespace YoutubeAlarm
{
    [Activity(Label = "YoutubeAlarm", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            EditText edText = FindViewById<EditText>(Resource.Id.queryText);

            button.Click += async delegate
            {
                var results =  await YTHelper.Search(edText.Text, 50);
                var result = results.First();

                if(await YTHelper.DownloadAudio(result.Value, "downloads/"))
                {
                    Toast.MakeText(this, String.Format("Audio of {0} finished downloading!", result.Key),ToastLength.Short);
                }
            };
        }
    }
}

