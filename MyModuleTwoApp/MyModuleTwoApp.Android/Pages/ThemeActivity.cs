using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Android.Content.Res;

namespace MyModuleTwoApp.Droid.Pages
{
    [Activity(Label = "ThemeActivity")]
    public class ThemeActivity : FormsAppCompatActivity, ThemeSupport.IActivityTheme
    {
        private const String STATE_ACTIVITY_THEME = "currentTheme";

        String currentActivityTheme = ThemeSupport.THEME_UNDEFINED;
        private ThemeSupport themeSupport;
        protected ThemeActivity self;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            themeSupport = new ThemeSupport(this);
            if (savedInstanceState != null)
            {
                currentActivityTheme = savedInstanceState.GetString(STATE_ACTIVITY_THEME, ThemeSupport.THEME_UNDEFINED);
            }

            // MUST BE SET BEFORE setContentView
            themeSupport.updateActivityTheme();

            self = this;
        }


        protected override void OnStart()
        {
            base.OnStart();

            if (!currentActivityTheme.Equals(ThemeSupport.getCurrentGlobalTheme()))
            {
                // Reapply global theme if it has been changed.
                this.Recreate();
            }
        }


        protected override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutString(STATE_ACTIVITY_THEME, currentActivityTheme);
        }


        public string getLocalThemeName()
        {
            return currentActivityTheme;
        }


        public void setLocalThemeName(string theme)
        {
            currentActivityTheme = theme;
        }

        public ThemeSupport getThemeSupport()
        {
            if (themeSupport == null)
            {
                themeSupport = new ThemeSupport(this);
            }
            return themeSupport;
        }
    }
}