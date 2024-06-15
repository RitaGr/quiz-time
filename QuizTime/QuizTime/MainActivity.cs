using System;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Android.Views;

namespace QuizTime
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        

        EditText mainActivityUsername, mainActivityPassword;
        Button buttonSignIn, buttonSignUp;       
        private ISharedPreferences sp;
        private Context context;

        public Context Context { get => context; set => context = value; }
        public ISharedPreferences Sp { get => sp; set => sp = value; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            this.Initialize();

            DataBase.CreateTables();           
           
        }

        private void Initialize()
        {
            this.mainActivityUsername = this.FindViewById<EditText>(Resource.Id.mainActivityUsername);
            this.mainActivityPassword = (EditText)this.FindViewById(Resource.Id.mainActivityPassword);
            this.buttonSignIn = (Button)this.FindViewById(Resource.Id.buttonSignIn);
            this.buttonSignIn.Click += ButtonSignIn_Click;
            this.buttonSignUp = (Button)this.FindViewById(Resource.Id.buttonSignUp);
            this.buttonSignUp.Click += ButtonSignUp_Click1;
            
        }

        private void ButtonSignUp_Click1(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(SignUp));
            this.StartActivity(intent);
        }

        

        private void ButtonSignIn_Click(object sender, EventArgs e)
        {
            string userName = mainActivityUsername.Text.Trim();
            string password = mainActivityPassword.Text.Trim();
            if (userName.Length == 0 || password.Length == 0)
            {
                Toast.MakeText(this, "There is a mistake", ToastLength.Long).Show();
                return;
            }
          

            if (User.Exist(userName, password))
            {
                Toast.MakeText(this, "Logged in! Welcome back " + userName , ToastLength.Long).Show();
                Intent intent = new Intent(this, typeof(GameMenu));
                this.StartActivity(intent);
                
            }
            else
            {
                Toast.MakeText(this, "Failed to sign in", ToastLength.Long).Show();

            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}