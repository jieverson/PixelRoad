using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityGoogleAnalytics.Core;

namespace Assets.UnityGoogleAnalytics
{
    public static class Track
    {
        public static void Event(string category, string action, string label = null, uint? value = null)
        {
            GoogleAnalyticsDefaultSettings.Configure();
            
            String url = Parser.Event(category, action, label, value);
            Debug.Log(url);
            WWW www = new WWW(url);
            while (www.isDone == false) { }
            Debug.Log("Ok");
        }

    }
}
