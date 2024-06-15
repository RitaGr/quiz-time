using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QuizTime
{
    [Service(Label = "MusicService")]
    public class MusicService : Service
    {
        IBinder binder;
        MediaPlayer mp;
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            // start your service logic here
            mp = MediaPlayer.Create(this, Resource.Raw.Pyre);
            mp.Start();
            // Return the correct StartCommandResult for the type of service you are building
            return StartCommandResult.NotSticky;
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            if (mp != null)
            {
                mp.Stop();
                mp.Release();
                mp = null;
            }

        }
        public override IBinder OnBind(Intent intent)
        {
            binder = new MusicServiceBinder(this);
            return binder;
        }

        public class MusicServiceBinder : Binder
        {
            private MusicService musicService;

            public MusicServiceBinder(MusicService musicService)
            {
                this.musicService = musicService;
            }
        }
    }
}