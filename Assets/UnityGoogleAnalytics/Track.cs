using UnityGoogleAnalytics.Core;

namespace UnityGoogleAnalytics
{
    public static class Track
    {

        public static void Event(string category, string action, string label = null, uint? value = null)
        {
            GoogleAnalyticsDefaultSettings.Check();

            TrackService.Event(category, action, label, value);
        }

    }
}
