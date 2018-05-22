using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Content;
using MySql.Data.MySqlClient;
using System.Data;

namespace DATABASE1111111
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            Button btnRegistreeri = FindViewById<Button>(Resource.Id.BtnRegistreeri);
            Button btnLogiSisse = FindViewById<Button>(Resource.Id.btnLogiSisse);

            btnRegistreeri.Click += delegate
            {
                Intent RegistrerView = new Intent(this, typeof(Registreeri));
                StartActivity(RegistrerView);
            };
            btnLogiSisse.Click += delegate
            {
                Intent LoginView = new Intent(this, typeof(Login));
                StartActivity(LoginView);
            };
        }
    }
}