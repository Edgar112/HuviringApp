using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Flurl.Http;


namespace DATABASE1111111
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    class AccountSettingsView : Activity
    {
        List<String> UserData = new List<String>();
        string User = string.Empty, Nickname = string.Empty, MaleFamale = string.Empty, Age = string.Empty, Description = string.Empty;
        string AccountName;
        int AccountID;

        TextView txtUser, txtNickname, txtMaleFamale, txtAge, txtDescription;
        Button btnChat;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AccountSettingsView);
            AccountName = Intent.GetStringExtra("AccountName");

            txtUser = FindViewById<TextView>(Resource.Id.txtUser);
            txtNickname = FindViewById<TextView>(Resource.Id.txtNimi);
            txtMaleFamale = FindViewById<TextView>(Resource.Id.txtMeesNaine);
            txtAge = FindViewById<TextView>(Resource.Id.txtVanus);
            txtDescription = FindViewById<TextView>(Resource.Id.txtKirjeldus);
            btnChat = FindViewById<Button>(Resource.Id.btnChat);

            StartRequest();

            btnChat.Click += delegate
            {

            };

        }
        private async void StartRequest()
        {
            //get account id
            string responseString = await "https://sempai.ee/"
             .PostUrlEncodedAsync(new { AccountName = AccountName })
             .ReceiveString();

            AccountID = Convert.ToInt32(responseString);
            //get AccountSettings
            responseString = await "https://sempai.ee/"
           .PostUrlEncodedAsync(new { AccountID = AccountID })
           .ReceiveString();

            ParseResponseAccountSettings(responseString, User, Nickname, MaleFamale, Age, Description);
        }
        public void ParseResponseAccountSettings(string responseString, string User, string Nickname, string MaleFamale, string Age, string Description)
        {
            User = responseString.Split(new char[] { '╙' })[0];
            Nickname = responseString.Split(new char[] { '♂' })[0].Split(new char[] { '╙' })[1];
            MaleFamale = responseString.Split(new char[] { '┼' })[0].Split(new char[] { '♂' })[1];
            Age = responseString.Split(new char[] { '╗' })[0].Split(new char[] { '┼' })[1];
            Description = responseString.Split(new char[] { '╒' })[0].Split(new char[] { '╗' })[1];

            txtUser.Text = User;
            txtNickname.Text = Nickname;
            txtMaleFamale.Text = MaleFamale;
            txtAge.Text = Age;
            txtDescription.Text = Description;

        }
    }
}