using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Flurl.Http;


namespace DATABASE1111111
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    class Login : Activity
    {
        EditText etUser,etPass;
        Button btnLog;
        TextView SystemLog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);

            etUser = FindViewById<EditText>(Resource.Id.etUser);
            etPass = FindViewById<EditText>(Resource.Id.etPass);
            btnLog = FindViewById<Button>(Resource.Id.btnLog);
            SystemLog = FindViewById<TextView>(Resource.Id.SystemLog);

            btnLog.Click += btnLog_Click;


        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            SystemLog.Text = String.Empty;
            StartRequest();

        }
        private async void StartRequest()
        {
            string responseString = await "https://sempai.ee/"
             .PostUrlEncodedAsync(new { UserLogin = etUser.Text, PassLogin = etPass.Text })
             .ReceiveString();


            SystemLog.Text = responseString;

            if (responseString == "Correct")
            {
                Intent MapView = new Intent(this, typeof(Map));
                MapView.PutExtra("AccountName", etUser.Text);
                MapView.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
                StartActivity(MapView);
            }
        }
    }
}