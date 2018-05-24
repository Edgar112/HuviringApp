using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Flurl.Http;


namespace DATABASE1111111
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    class Registreeri : Activity
    {
        string server, database, uid, password;
        EditText etUser,etPass;
        Button btnReg;
        TextView SystemLog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Registrer);

            etUser = FindViewById<EditText>(Resource.Id.etUser);
            etPass = FindViewById<EditText>(Resource.Id.etPass);
            btnReg = FindViewById<Button>(Resource.Id.btnReg);
            SystemLog = FindViewById<TextView>(Resource.Id.SystemLog);
             //SystemLog.Visibility = Android.Views.ViewStates.Invisible;
            btnReg.Click += btnReg_Click;


        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            SystemLog.Text = String.Empty;
            StartRequest();

        }

        private async void StartRequest()
        {
            string responseString = await "https://sempai.ee/"
             .PostUrlEncodedAsync(new { UserReg = etUser.Text, PassReg = etPass.Text })
             .ReceiveString();

            SystemLog.Text = responseString;


            if (SystemLog.Text == "Correct")
            {
                Intent AccountSettingsView = new Intent(this, typeof(AccountSettings));
                AccountSettingsView.PutExtra("AccountName", etUser.Text);
                StartActivity(AccountSettingsView);
            }
        }
    }
}