using System;
using System.Text;

namespace UnityGoogleAnalytics.Core
{
    public static class Parser
    {

        private const String HOST = "http://www.google-analytics.com";
        private const String PATH = "/collect";
        private const String METHOD = "GET";
        private const String PROTOCOL_VERSION = "1";

        public static String OpenSession()
        {
            return Parse(GeneratePayloadData(HitType.Event, category: "ApplicationSession", action: "Start", label: "GameStarted", sessionControl: SessionControl.Start));
        }

        public static String CloseSession()
        {
            return Parse(GeneratePayloadData(HitType.Event, category: "ApplicationSession", action: "End", label: "GameEnded", sessionControl: SessionControl.End));
        }

        public static String PageView(String pageName)
        {
            //if (!pageName.StartsWith("/")) pageName = "/" + pageName;
            return Parse(GeneratePayloadData(HitType.PageView, pageName: pageName));
        }

        public static String ScreenView(String screenName)
        {
            return Parse(GeneratePayloadData(HitType.ScreenView, screenName: screenName));
        }

        public static String Event(String category, String action, String label = null, uint? value = null)
        {
            return Parse(GeneratePayloadData(HitType.Event, category: category, action: action, label: label, value: value));
        }

        private static String GeneratePayloadData(HitType hitType, String pageName = null, String screenName = null, String category = null, String action = null, String label = null, uint? value = null, SessionControl sessionControl = SessionControl.None)
        {
            if (String.IsNullOrEmpty(TrackConfig.TrackingID))
                throw new ArgumentException("TrackingID can not be empty.");
            if (String.IsNullOrEmpty(TrackConfig.ClientID))
                throw new ArgumentException("ClientID can not be empty.");

            String v = PROTOCOL_VERSION;
            String tid = TrackConfig.TrackingID;
            String cid = TrackConfig.ClientID;
            String t = hitType.ToString().ToLower();
            String sr =
                TrackConfig.ScreenWidth != null
                && TrackConfig.ScreenHeight != null
                ? TrackConfig.ScreenWidth + "x" + TrackConfig.ScreenHeight
                : null;
            String dp = pageName;
            String cd = screenName;
            String ec = category;
            String ea = action;
            String el = label;
            String ev = value != null ? value.ToString() : null;
            String sc = sessionControl != SessionControl.None ? sessionControl.ToString().ToLower() : null;
            String uid = TrackConfig.UserID;
            String an = TrackConfig.ApplicationName;
            String aid = TrackConfig.ApplicationID;
            String av = TrackConfig.ApplicationVersion;

            var payload_data = new StringBuilder();

            // Required Information
            payload_data.Append("v=" + v);
            payload_data.Append("&tid=" + tid);
            payload_data.Append("&cid=" + cid);
            payload_data.Append("&t=" + t);

            // Tracking Information
            if (dp != null) payload_data.Append("&dp=" + dp);
            if (cd != null) payload_data.Append("&cd=" + cd);
            if (ec != null) payload_data.Append("&ec=" + ec);
            if (ea != null) payload_data.Append("&ea=" + ea);
            if (el != null) payload_data.Append("&el=" + el);
            if (ev != null) payload_data.Append("&ev=" + ev);

            // Session Information
            if (sc != null) payload_data.Append("&sc=" + sc);

            // Opitional Information
            if (uid != null) payload_data.Append("&uid=" + uid);
            if (sr != null) payload_data.Append("&sr=" + sr);
            if (an != null) payload_data.Append("&an=" + an);
            if (aid != null) payload_data.Append("&aid=" + aid);
            //if (av != null) payload_data.Append("&av=" + av);

            return payload_data.ToString();
        }

        private static String Parse(String payload_data)
        {
            String url = String.Format("{0}{1}?{2}", HOST, PATH, payload_data).Replace(" ", String.Empty);
            return url;
        }

    }
}
