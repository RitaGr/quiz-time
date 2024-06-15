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
using Android.Support.V7.App;
using Android.Support.Design.Widget;

namespace QuizTime
{
    [Activity(Label = "QuizTime")]
    public class GameMenu : AppCompatActivity
    {

        Android.Support.V4.Widget.DrawerLayout drawerLayout;
        

        LinearLayout historyLayout;
        LinearLayout spaceLayout;
        LinearLayout geographyLayout;
        TextView tv;
        FlightModeReciever flightModeReciever;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.gameMenu);

            historyLayout = (LinearLayout)FindViewById(Resource.Id.historyLayout);
            spaceLayout = (LinearLayout)FindViewById(Resource.Id.spaceLayout);
            geographyLayout = (LinearLayout)FindViewById(Resource.Id.geographyLayout);
            tv = FindViewById<TextView>(Resource.Id.tv);
            RegisterForContextMenu(tv);
            flightModeReciever = new FlightModeReciever();





            historyLayout.Click += HistoryLayout_Click;
            geographyLayout.Click += GeographyLayout_Click;
            spaceLayout.Click += SpaceLayout_Click;

            drawerLayout = (Android.Support.V4.Widget.DrawerLayout)FindViewById(Resource.Id.drawerLayout);


        }
        protected override void OnResume()
        {
            base.OnResume();
            RegisterReceiver(flightModeReciever, new IntentFilter("android.intent.action.AIRPLANE_MODE"));
        }
        protected override void OnPause()
        {
            UnregisterReceiver(flightModeReciever);
            base.OnPause();
        }


        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
         {
            base.OnCreateContextMenu(menu, v, menuInfo);
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
         }
        public override bool OnContextItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.firstline)
            {
                Intent intent = new Intent(this, typeof(MusicService));
                StartService(intent);
                return true;
            }
            else if (item.ItemId == Resource.Id.secondline)
            {
                Intent intent = new Intent(this, typeof(MusicService));
                StopService(intent);
                return true;
            }
           return false;
        }


        void SpaceLayout_Click(object sender, EventArgs e)
        {
            InitSpace();
        }

        void InitSpace()
        {
            Intent intent = new Intent(this, typeof(QuizDescriptionActivity));
            intent.PutExtra("topic", "Space");
            StartActivity(intent);
        }

        void GeographyLayout_Click(object sender, EventArgs e)
        {
            InitGeography();
        }

        void InitGeography()
        {
            Intent intent = new Intent(this, typeof(QuizDescriptionActivity));
            intent.PutExtra("topic", "Geography");
            StartActivity(intent);
        }

        void HistoryLayout_Click(object sender, EventArgs e)
        {
            InitHistory();
        }

        void InitHistory()
        {
            Intent intent = new Intent(this, typeof(QuizDescriptionActivity));
            intent.PutExtra("topic", "History");
            StartActivity(intent);
        }
        
    }
}