using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace QuizTime
{
    [Activity(Label = "SignUp")]
    public class SignUp : AppCompatActivity
    {
        EditText textInputEditTextName, textInputEditTextPassword, textInputEditTextPasswordAgain;      
        Button buttonSignUp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signup);
            this.Initialize();
        }
        public void Initialize()
        {
            textInputEditTextName = FindViewById<EditText>(Resource.Id.textInputEditTextName);
            textInputEditTextPassword = FindViewById<EditText>(Resource.Id.textInputEditTextPassword);
            textInputEditTextPasswordAgain = FindViewById<EditText>(Resource.Id.textInputEditTextPasswordAgain);
            buttonSignUp = FindViewById<Button>(Resource.Id.buttonSignUp);
            buttonSignUp.Click += ButtonSignUp_Click;
        }

        private void ButtonSignUp_Click(object sender, EventArgs e)
        {
            string userName = textInputEditTextName.Text.Trim();
            string password = textInputEditTextPassword.Text;
            string confirmPassword = textInputEditTextPasswordAgain.Text;
            if (userName.Length < 3)
            {
                Toast.MakeText(this, "Username length too short", ToastLength.Short).Show();
                return;
            }
            if (password != confirmPassword)
            {
                Toast.MakeText(this, "The passwords are not match", ToastLength.Short).Show();
                return;
            }
            if (User.Exist(userName))
            {
                textInputEditTextName.Text = "";
                Toast.MakeText(this, "Username exists already", ToastLength.Short).Show();
                return;
            }
            if (User.Save(userName, password))
            {
                Toast.MakeText(this, "Welcome to QuizTime! " + userName, ToastLength.Short).Show();
                Intent intent = new Intent(this, typeof(GameMenu));
                this.StartActivity(intent);
                Finish();
            }
            else
            {
                Toast.MakeText(this, "...", ToastLength.Short).Show();
            }
        }
    }
}