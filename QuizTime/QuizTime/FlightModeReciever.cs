﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace QuizTime
{
    [BroadcastReceiver(Enabled = true)]
    public class FlightModeReciever : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            string action = intent.Action;
            if (action.Equals("android.intent.action.AIRPLANE_MODE"))
            {
                Toast.MakeText(context, "airplane mode changed", ToastLength.Short).Show();
            }



        }
    }
}