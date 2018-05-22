using System;
using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Widget;
using Flurl.Http;
using Plugin.Geolocator;
using System.Collections;
using System.Collections.Generic;

namespace DATABASE1111111
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    class Map : Activity, IOnMapReadyCallback
    {
        string AccountName;
        int AccountID;
        double Latitude, Longitude;
        public string[] Accounts = new string[0];
        public List<String> UserData = new List<String>();
        public List<String> Username = new List<String>();
        public List<Double> Latitude1 = new List<Double>();
        public List<Double> Longitude1 = new List<Double>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            AccountName = Intent.GetStringExtra("AccountName");
            LocatonAsync();

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Map);

        }
        public async void LocatonAsync()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Latitude = position.Latitude;
            Longitude = position.Longitude;

            StartRequest();

        }
        private async void StartRequest()
        {
            //get account id
            string responseString = await "https://sempai.ee/"
                .PostUrlEncodedAsync(new { AccountName = AccountName })
                .ReceiveString();

            AccountID = Convert.ToInt32(responseString);

            //insert account location
            responseString = await "https://sempai.ee/"
                .PostUrlEncodedAsync(new { AccountID = AccountID, Latitude = Latitude, Longitude = Longitude })
                .ReceiveString();

            //Get all accounts location
            responseString = await "https://sempai.ee/"
            .PostUrlEncodedAsync(new { GetAllAccLocation = "true" })
            .ReceiveString();

            ParseResponseLocation(responseString, Accounts, UserData, Username, Latitude1, Longitude1);

        }
        public void ParseResponseLocation(string responseString, string[] Accounts, List<String> UserData, List<String> Username, List<Double> Latitude, List<Double> Longitude)
        {
            Accounts = responseString.Split(new char[] { '╗' });
            int i = 0;
            while (Accounts.Length > i)
            {
                Console.WriteLine(Accounts[i]);
                UserData.Add(Accounts[i]);
                i++;
            }
            UserData.RemoveAt(UserData.Count - 1); // remove  empty string
            i = 0;
            while (UserData.Count > i)
            {
                Username.Add(UserData[i].Split(new char[] { '╙' })[0]);
                Latitude.Add(Convert.ToDouble(UserData[i].Split(new char[] { '♂' })[0].Split(new char[] { '╙' })[1]));
                Longitude.Add(Convert.ToDouble(UserData[i].Split(new char[] { '┼' })[0].Split(new char[] { '♂' })[1]));
                i++;
            }
            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            LatLng latlng = new LatLng(Latitude, Longitude);
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(latlng, 10);
            googleMap.MoveCamera(camera);

            MarkerOptions markerOptions = new MarkerOptions();
            int i = 0;
            while (UserData.Count > i)
            {
                markerOptions.SetPosition(new LatLng(Latitude1[i], Longitude1[i]));
                markerOptions.SetTitle(Username[i]);
                googleMap.AddMarker(markerOptions);
                i++;
            }

            //optional
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());
            googleMap.InfoWindowClick += MapOnInfoWindowClick;
        }
        private void MapOnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            //open Account Settings
            Marker myMarker = e.Marker;
            Intent AccountSettingsViewView = new Intent(this, typeof(AccountSettingsView));
            AccountSettingsViewView.PutExtra("AccountName", myMarker.Title);
            StartActivity(AccountSettingsViewView);
        }

        
    }
}