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
    [Activity(Label = "Quiz Description")]
    public class QuizDescriptionActivity : AppCompatActivity
    {
        TextView quizTopicTextView;
        TextView descriptionTextView;
        ImageView quizImageView;
        Button startQuizButton;
        Button backToTopics;

        // Varibales
        string quizTopic;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.selected_topic);

            quizTopicTextView = (TextView)FindViewById(Resource.Id.quizTopicText);
            descriptionTextView = (TextView)FindViewById(Resource.Id.quizDescriptionText);
            quizImageView = (ImageView)FindViewById(Resource.Id.quizImage);
            startQuizButton = (Button)FindViewById(Resource.Id.startQuizButton);
            backToTopics = (Button)FindViewById(Resource.Id.backToTopics);


            quizTopic = Intent.GetStringExtra("topic");
            quizTopicTextView.Text = quizTopic;
            quizImageView.SetImageResource(GetImage(quizTopic));

            // Retrieve Description
            QuizHelper quizHelper = new QuizHelper();
            descriptionTextView.Text = quizHelper.GetTopicDescription(quizTopic);
            startQuizButton.Click += StartQuizButton_Click;
            backToTopics.Click += BackToTopics_Click;
        }

        private void BackToTopics_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GameMenu));
            StartActivity(intent);
        }

        int GetImage(string topic)
        {
            int imageInt = 0;

            if (topic == "History")
            {
                imageInt = Resource.Drawable.history;
            }
            else if (topic == "Geography")
            {
                imageInt = Resource.Drawable.geography;
            }
            else if (topic == "Space")
            {
                imageInt = Resource.Drawable.space;
            }          

            return imageInt;
        }

        private void StartQuizButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(QuizActivity));
            intent.PutExtra("topic", quizTopic);
            StartActivity(intent);
            Finish();
        }
    }
}