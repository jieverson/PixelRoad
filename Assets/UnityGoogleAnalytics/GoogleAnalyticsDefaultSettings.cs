using System;
using UnityEngine;
using UnityGoogleAnalytics.Core;

namespace UnityGoogleAnalytics
{
    public static class GoogleAnalyticsDefaultSettings
    {

        public const String TRACKING_ID = "UA-34576486-3";
        public const String GAME_VERSION = "1.0";

        public static void Check()
        {
            TrackConfig.TrackingID = TRACKING_ID;
            TrackConfig.ClientID = SystemInfo.deviceUniqueIdentifier;
            TrackConfig.ScreenWidth = Screen.width;
            TrackConfig.ScreenHeight = Screen.height;
            TrackConfig.ApplicationVersion = GAME_VERSION;
        }

    }
}
