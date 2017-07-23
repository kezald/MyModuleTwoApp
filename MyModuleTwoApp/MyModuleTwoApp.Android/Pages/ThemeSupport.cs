using Android.Graphics;
using Android.Views;
using Xamarin.Forms.Platform.Android;

namespace MyModuleTwoApp.Droid.Pages
{
    public class ThemeSupport
    {
        public interface IActivityTheme
        {
            string getLocalThemeName();

            void setLocalThemeName(string theme);
        }

        //    Theme identifiers. These must match those of color resource names (their postfixes).
        public const string THEME_UNDEFINED = "ThemeUndefined";
        public const string THEME_BLUE_GRAY = "ThemeBlueGray";
        public const string THEME_TEAL = "ThemeTeal";
        public const string THEME_ORANGE = "ThemeOrange";
        public const string THEME_DEEP_PURPLE = "ThemeDeepPurple";

        public static string[] THEME_NAMES = new string[] { THEME_BLUE_GRAY, THEME_TEAL, THEME_ORANGE, THEME_DEEP_PURPLE };

        private static string currentGlobalTheme = THEME_BLUE_GRAY;

        private ThemeActivity themeActivity;
        private Color themeBackgroundColor;

        public ThemeSupport(ThemeActivity themeActivity)
        {
            if (themeActivity == null)
            {
                //Raise Exception
            }

            this.themeActivity = themeActivity;
        }

        /**
         * @param activity    Activity that requested global theme change must apply this theme itself through recreation.
         *                    Only ThemeActivity applies themes on recreation. Other activities are not recreated
         * @param globalTheme
         */
        public static void setGlobalTheme(FormsAppCompatActivity activity, string globalTheme)
        {
            currentGlobalTheme = globalTheme;
            //        activity.finish();
            //        activity.startActivity(new Intent(activity, activity.getClass()));
            //        activity.overridePendingTransition(android.R.anim.fade_in,
            //                android.R.anim.fade_out);
            if (activity is ThemeActivity) {
                activity.Recreate();
            }
        }

        public static string getCurrentGlobalTheme()
        {
            return currentGlobalTheme;
        }


        /**
         * Use this to ALWAYS update activity theme BEFORE setContentView.
         * If you avoid updating a single time - only properties from default theme will be applied.
         * Checking for current activity theme and matching it against current global theme is unnecessary.
         */
        public void updateActivityTheme()
        {
            Color color;
            switch (currentGlobalTheme)
            {
                default:
                case THEME_BLUE_GRAY:
                    themeActivity.Theme.ApplyStyle(Resource.Style.ThemeBlueGray, true);
                    color = Color.ParseColor("#263238");
                    break;
                case THEME_TEAL:
                    themeActivity.Theme.ApplyStyle(Resource.Style.ThemeTeal, true);
                    color = Color.ParseColor("#004D40");
                    break;
                case THEME_ORANGE:
                    themeActivity.Theme.ApplyStyle(Resource.Style.ThemeOrange, true);
                    color = Color.ParseColor("#DF7800");
                    break;
                case THEME_DEEP_PURPLE:
                    themeActivity.Theme.ApplyStyle(Resource.Style.ThemeDeepPurple, true);
                    color = Color.ParseColor("#311B92");
                    break;
            }
            //Xamarin Hack below. Why background doesn't work otherwise - no idea
            themeActivity.Window.DecorView.SetBackgroundColor(color);
            themeActivity.setLocalThemeName(currentGlobalTheme);
            themeBackgroundColor = color;
        }

        public Color getThemeBackgroundColor()
        {
            if (themeBackgroundColor == null)
            {
                themeBackgroundColor = Color.ParseColor("#263238");
            }
            return themeBackgroundColor;
        }
    }
}