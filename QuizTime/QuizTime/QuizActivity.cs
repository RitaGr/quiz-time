using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using QuizTime;


namespace QuizTime
{
    [Activity(Label = "QuizActivity")]
    public class QuizActivity : AppCompatActivity
    {
        RadioButton optionARadio, optionBRadio, optionCRadio, optionDRadio;

        TextView optionATextView, optionBTextView, optionCTextView, optionDTextView, questionTextView, quizPositionTextView, timerCounterTextView;

        Button proccedQuizButton;

        //Variables
        List<Question> quizQuestionList = new List<Question>();
        readonly QuizHelper quizHelper = new QuizHelper();

        string quizTopic;
        int quizPosition;
        double correctAnswerCount = 0;

        int timerCounter = 0;
        DateTime dateTime;
        System.Timers.Timer countDown = new System.Timers.Timer();

        public Timer CountDown { get => countDown; set => countDown = value; }

        public QuizHelper QuizHelper => quizHelper;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.quiz_page);
            quizTopic = Intent.GetStringExtra("topic");

            Android.Support.V7.App.ActionBar actionBar = SupportActionBar;

            ConnectViews();
            BeginQuiz();

            CountDown.Interval = 1000;
            CountDown.Elapsed += CountDown_Elapsed;
        }

        private void CountDown_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerCounter++;

            DateTime dt = new DateTime();
            dt = dateTime.AddSeconds(-1);

            var dateDifference = dateTime.Subtract(dt);
            dateTime -= dateDifference;

            RunOnUiThread(() =>
            {
                timerCounterTextView.Text = dateTime.ToString("mm:ss");
            });

            // End Quiz on Timeout
            if (timerCounter == 120)
            {
                CountDown.Enabled = false;
                CompleteQuiz();
            }
        }

        void BeginQuiz()
        {
            quizPosition = 1;
            quizQuestionList = QuizHelper.GetQuizQuestions(quizTopic);
            questionTextView.Text = quizQuestionList[0].QuizQuestion;
            optionATextView.Text = quizQuestionList[0].OptionA;
            optionBTextView.Text = quizQuestionList[0].OptionB;
            optionCTextView.Text = quizQuestionList[0].OptionC;
            optionDTextView.Text = quizQuestionList[0].OptionD;

            quizPositionTextView.Text = "Question " + quizPosition.ToString() + "/" + quizQuestionList.Count();

            dateTime = new DateTime();
            dateTime = dateTime.AddMinutes(2);
            timerCounterTextView.Text = dateTime.ToString("mm:ss");

            CountDown.Enabled = true;

        }

        private void ConnectViews()
        {
            // RadioButtons
            optionARadio = (RadioButton)FindViewById(Resource.Id.optionARadio);
            optionBRadio = (RadioButton)FindViewById(Resource.Id.optionBRadio);
            optionCRadio = (RadioButton)FindViewById(Resource.Id.optionCRadio);
            optionDRadio = (RadioButton)FindViewById(Resource.Id.optionDRadio);

            optionARadio.Click += OptionARadio_Click;
            optionBRadio.Click += OptionBRadio_Click;
            optionCRadio.Click += OptionCRadio_Click;
            optionDRadio.Click += OptionDRadio_Click;

            //TextViews
            optionATextView = (TextView)FindViewById(Resource.Id.optionATextView);
            optionBTextView = (TextView)FindViewById(Resource.Id.optionBTextView);
            optionCTextView = (TextView)FindViewById(Resource.Id.optionCTextView);
            optionDTextView = (TextView)FindViewById(Resource.Id.optionDTextView);
            questionTextView = (TextView)FindViewById(Resource.Id.questionTextView);
            quizPositionTextView = (TextView)FindViewById(Resource.Id.quizPositionTextView);
            timerCounterTextView = (TextView)FindViewById(Resource.Id.timeCounterTextView);

            // Button
            proccedQuizButton = (Button)FindViewById(Resource.Id.proceedQuizButton);
            proccedQuizButton.Click += ProccedQuizButton_Click;
        }

        private void OptionDRadio_Click(object sender, EventArgs e)
        {
            ClearOptionsSelected();
            optionDRadio.Checked = true;
        }

        private void OptionCRadio_Click(object sender, EventArgs e)
        {
            ClearOptionsSelected();
            optionCRadio.Checked = true;
        }

        private void OptionBRadio_Click(object sender, EventArgs e)
        {
            ClearOptionsSelected();
            optionBRadio.Checked = true;
        }

        private void OptionARadio_Click(object sender, EventArgs e)
        {
            ClearOptionsSelected();
            optionARadio.Checked = true;
        }

        private void ProccedQuizButton_Click(object sender, EventArgs e)
        {
            if (!optionARadio.Checked && !optionBRadio.Checked && !optionCRadio.Checked && !optionDRadio.Checked)
            {
                Snackbar.Make((View)sender, "Please choose your answer!!", Snackbar.LengthShort).Show();
            }

            // Checks option A for Correct Answer 
            else if (optionARadio.Checked)
            {
                if (optionATextView.Text == quizQuestionList[quizPosition - 1].Answer)
                {
                    correctAnswerCount++;
                    CorrectAnswer();
                }
                else
                {
                    IncorrectAnswer();
                }

            }
            // Checks option B for Correct Answer 
            else if (optionBRadio.Checked)
            {
                if (optionBTextView.Text == quizQuestionList[quizPosition - 1].Answer)
                {
                    correctAnswerCount++;
                    CorrectAnswer();
                }
                else
                {
                    IncorrectAnswer();
                }

            }
            // Checks option C for Correct Answer 
            else if (optionCRadio.Checked)
            {
                if (optionCTextView.Text == quizQuestionList[quizPosition - 1].Answer)
                {
                    correctAnswerCount++;
                    CorrectAnswer();
                }
                else
                {
                    IncorrectAnswer();
                }
            }
            // Checks option D for Correct Answer 
            else if (optionDRadio.Checked)
            {
                if (optionDTextView.Text == quizQuestionList[quizPosition - 1].Answer)
                {
                    correctAnswerCount++;
                    CorrectAnswer();
                }
                else
                {
                    IncorrectAnswer();
                }
            }
        }

        private void IncorrectAnswer()
        {
            IncorrectFragments incorrectFragments = new IncorrectFragments(quizQuestionList[quizPosition - 1].Answer);
            var trans = SupportFragmentManager.BeginTransaction();
            incorrectFragments.Cancelable = false;
            incorrectFragments.Show(trans, "Incorrect");
            incorrectFragments.NextQuestion += CorrectFragments_NextQuestion;
        }

        private void CorrectFragments_NextQuestion(object sender, EventArgs e)
        {
            // Next Question
            quizPosition++;

            if (quizPosition > quizQuestionList.Count)
            {
                CompleteQuiz();
                return;
            }

            int indx = quizPosition - 1;
            ClearOptionsSelected();

            questionTextView.Text = quizQuestionList[indx].QuizQuestion;
            optionATextView.Text = quizQuestionList[indx].OptionA;
            optionBTextView.Text = quizQuestionList[indx].OptionB;
            optionCTextView.Text = quizQuestionList[indx].OptionC;
            optionDTextView.Text = quizQuestionList[indx].OptionD;

            quizPositionTextView.Text = "Question " + quizPosition.ToString() + "/" + quizQuestionList.Count.ToString();
        }

        private void ClearOptionsSelected()
        {
            optionARadio.Checked = false;
            optionBRadio.Checked = false;
            optionCRadio.Checked = false;
            optionDRadio.Checked = false;
        }

        private void CompleteQuiz()
        {
            timerCounterTextView.Text = "00:00";
            CountDown.Enabled = false;

            string score = correctAnswerCount.ToString() + "/" + quizQuestionList.Count.ToString();
            double percentage = (correctAnswerCount / double.Parse(quizQuestionList.Count.ToString())) * 100;
            string remarks = "";
            string image = "";

            if (percentage > 50 && percentage < 70)
            {
                remarks = "Very Good result, you\nReally tried";
            }
            else if (percentage >= 70)
            {
                remarks = "Very Outstanding result, you\nKilled it!!";
            }
            else if (percentage == 50)
            {
                remarks = "You really made it,\nAverage result";
            }
            else if (percentage < 50)
            {
                remarks = "So sad you didn't make it, \nBut you can try again";
                image = "failed";
            }

            CompletedFragment completedFragment = new CompletedFragment(remarks, score, image)
            {
                Cancelable = false
            };
            var trans = SupportFragmentManager.BeginTransaction();
            completedFragment.Show(trans, "Complete");

            completedFragment.GoHome += (sender, e) =>
            {
                this.Finish();
            };
        }

        private void CorrectAnswer()
        {
            CorrectFragments correctFragments = new CorrectFragments();
            var trans = SupportFragmentManager.BeginTransaction();
            correctFragments.Cancelable = false;
            correctFragments.Show(trans, "Correct");
            correctFragments.NextQuestion += CorrectFragments_NextQuestion;
        }
    }
}