using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Support.V7.Widget;
using MyModuleTwoApp;
using Plugin.Geolocator.Abstractions;
using Android.Widget;

namespace MyModuleTwoApp.Droid.Pages
{
    [Activity(Label = "BaseActivity")]
    public class BaseActivity : ThemeActivity
    {
        Android.Support.V7.Widget.Toolbar toolbarApp;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //View view = Android.Views.View.Inflate(this, Resource.Layout.Toolbar, null);
            //toolbarApp = (Toolbar) view.FindViewById(Resource.Id.toolbar);
            //SetActionBar(toolbarApp);
        }

        private void Bt_showLocation_Click(object sender, EventArgs e)
        {
            showPosition();
        }

        protected override void OnStart()
        {
            base.OnStart();

            View bt_showLocation = FindViewById(Resource.Id.bt_show_location);
            bt_showLocation.Click += Bt_showLocation_Click;

            toolbarApp = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            if (toolbarApp != null)
            {
                toolbarApp.Click += (sender, e) =>
                {
                    if (ThemeSupport.getCurrentGlobalTheme() == ThemeSupport.THEME_BLUE_GRAY)
                    {
                        ThemeSupport.setGlobalTheme(self, ThemeSupport.THEME_TEAL);
                    }
                    else if (ThemeSupport.getCurrentGlobalTheme() == ThemeSupport.THEME_TEAL)
                    {
                        ThemeSupport.setGlobalTheme(self, ThemeSupport.THEME_ORANGE);
                    }
                    else if (ThemeSupport.getCurrentGlobalTheme() == ThemeSupport.THEME_ORANGE)
                    {
                        ThemeSupport.setGlobalTheme(self, ThemeSupport.THEME_DEEP_PURPLE);
                    }
                    else
                    {
                        ThemeSupport.setGlobalTheme(self, ThemeSupport.THEME_BLUE_GRAY);
                    }
                };
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.menuitem_location:
                    showPosition();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        public async void showPosition()
        {
            Position pos = await LocationTracker.getLocation();
            Toast.MakeText(this, "Latitude: " + pos.Latitude + " Longitude: " + pos.Longitude, ToastLength.Short).Show();
        }
    }
}