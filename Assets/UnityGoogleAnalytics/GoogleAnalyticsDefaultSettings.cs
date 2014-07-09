using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityGoogleAnalytics.Core;

public static class GoogleAnalyticsDefaultSettings
{

    public const String TRACKING_ID = "UA-34576486-3";
    public const String GAME_VERSION = "1.0";

    public static void Configure()
    {
        TrackConfig.TrackingID = TRACKING_ID;
        TrackConfig.ClientID = SystemInfo.deviceUniqueIdentifier;
        TrackConfig.ScreenWidth = Screen.width;
        TrackConfig.ScreenHeight = Screen.height;
        TrackConfig.ApplicationVersion = GAME_VERSION;
    }

}

