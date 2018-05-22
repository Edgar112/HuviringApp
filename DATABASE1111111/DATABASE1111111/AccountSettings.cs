using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Flurl.Http;


namespace DATABASE1111111
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    class AccountSettings : Activity
    {
        EditText etNimi, etMeesNaine, etVanus, etKirjeldus;
        Button btnSave;
        TextView SystemLog;
        string AccountName;
        int AccountID;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AccountSettings);

            etNimi = FindViewById<EditText>(Resource.Id.etNimi);
            etMeesNaine = FindViewById<EditText>(Resource.Id.etMeesNaine);
            etVanus = FindViewById<EditText>(Resource.Id.etVanus);
            etKirjeldus = FindViewById<EditText>(Resource.Id.etKirjeldus);
            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            SystemLog = FindViewById<TextView>(Resource.Id.SystemLog);

            btnSave.Click += btnSave_Click;

            AccountName = Intent.GetStringExtra("AccountName");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SystemLog.Text = String.Empty;
            StartAsync();

        }
        private async void StartAsync()
        {
         // get account id
            string responseString = await "https://sempai.ee/"
             .PostUrlEncodedAsync(new { AccountName = AccountName})
             .ReceiveString();

            AccountID = Convert.ToInt32(responseString);
            SystemLog.Text = responseString;


            //save account settings
            responseString = await "https://sempai.ee/"
             .PostUrlEncodedAsync(new { AccountID = AccountID, Name = etNimi.Text, MaleFamale = etMeesNaine.Text, Age = etVanus.Text, Description = etKirjeldus.Text })
             .ReceiveString();

            SystemLog.Text = responseString;

            Intent LoginRegView = new Intent(this, typeof(MainActivity));
            LoginRegView.AddFlags(ActivityFlags.ClearTop | ActivityFlags.NewTask);
            StartActivity(LoginRegView);
         }
    }
}