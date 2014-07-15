using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_WP8

using FacebookWinPhone8;

//Note: LOGIN STEPS
//==========================
//1. Call Login() after Init()
//2. Login using app then FacebookWP8.AccessToken is obtained but not UserID
//3. Call Login again to get UserID.
//4. Save UserID and AccessToken using DLL call LoginComplete();

//This means the login button has to be pressed twice to complete login.

namespace Facebook
{
	
	public class WindowsPhoneFacebook : AbstractFacebook, IFacebook{
		
		public const int BrowserDialogMode = 0;
			
		FacebookDelegate cb = null;
		
		public override int DialogMode { get { return BrowserDialogMode; } set { } }
		
		protected override void OnAwake()
        {
           	userId = "";
			accessToken = "";
        }
#region Init
        public override void Init(
            InitDelegate onInitComplete,
            string appId,
            bool cookie = false,
            bool logging = true,
            bool status = true,
            bool xfbml = false,
            string channelUrl = "",
            string authResponse = null,
            bool frictionlessRequests = false,
            Facebook.HideUnityDelegate hideUnityDelegate = null)
        {
			//Init Facebook. This will load in UserID and AccessToken if made a previous login.
			FacebookWP8.Init(appId); 


			if(FacebookWP8.IsLoggedIn){
				//If Logged in (UserID and Token was saved and valid then set login = true
				//Dll checks if the token if valid.
				isLoggedIn = FacebookWP8.IsLoggedIn; //Only true if token and User are not blank.
				accessToken = FacebookWP8.AccessToken;
				userId = FacebookWP8.UserID;
			}
			else if(FacebookWP8.AccessToken.Length > 0){
				//If Only access token is there, because login with DLL only produces the Acess Token,
				//Then Complete Login
				isLoggedIn = false;
				accessToken = FacebookWP8.AccessToken;
				StartCoroutine(LoginComplete());
			}
			else{
				//If AccessToken and User are blank in FacebookWP8 class then login if false.
				isLoggedIn = false;
			}

			if (onInitComplete != null){
                onInitComplete();
            }
			
        }

        
        #endregion

        public override void Login(string scope = "", FacebookDelegate callback = null)
		{
			if(FacebookWP8.IsLoggedIn){
				//If logged in then do nothing.
				return;
			}
			else if(!FacebookWP8.IsLoggedIn && FacebookWP8.AccessToken.Length > 0){
				//if AccessToken is only avaiable then Complete login (get UserID)
				StartCoroutine(LoginComplete());
			}
			else{
				FacebookWP8.Login(scope);
				StartCoroutine(CheckLoginLoop()); //Start LoginCheck for login complete.
			}
			cb = callback;
		}

		public override void Logout()
		{
			FacebookWP8.Logout();
			userId = "";
			accessToken = "";
			isLoggedIn = false;
		}

		private IEnumerator CheckLoginLoop(){
			//Checks every one second to se if login is half done.
			while(true){
				yield return new WaitForSeconds(1f);
				if(!FacebookWP8.IsLoggedIn && FacebookWP8.AccessToken.Length > 0){
					//if AccessToken is only avaiable then Complete login (get UserID)
					StartCoroutine(LoginComplete());
					break;
				}
			}
		}

		private IEnumerator  LoginComplete(){

			accessToken = FacebookWP8.AccessToken;
			string url = "https://graph.facebook.com/me?method=get&access_token=" + accessToken;
			WWW www = new WWW(url);
			yield return www;
			if(www.error != null)
			{
				Debug.LogError("www.error");
				Logout();
			}
			else if(www.text.Contains("first_name")){
				//If Data comes back then get User ID and Save it. Also be sure to save AccessToken
				var dict = Facebook.MiniJSON.Json.Deserialize(www.text) as Dictionary<string,object>;
				FacebookWP8.UserID = (string)dict["id"];
				userId = FacebookWP8.UserID;
				FacebookWP8.SaveUserIDToken();
				FacebookWP8.SaveAccessToken();
			}
			else{ 
				//Token is not valid. Delete session. Set to not logged in;
				Logout();
			}

			isLoggedIn = FacebookWP8.IsLoggedIn;

			if(cb != null) 
				cb(new FBResult((isLoggedIn) ?  "Login sucessful" : "Login failed", null));
			 
		}


        public override void AppRequest(
            string message,
            string[] to = null,
            string filters = "",
            string[] excludeIds = null,
            int? maxRecipients = null,
            string data = "",
            string title = "",
            FacebookDelegate callback = null)
        {
			throw new UnityException("There is no Facebook AppRequest on Windows Phone 8");
        }

        public override void FeedRequest(
            string _toId = "",
            string _link = "",
            string _linkName = "",
            string _linkCaption = "",
            string _linkDescription = "",
            string _picture = "",
            string _mediaSource = "",
            string _actionName = "",
            string _actionLink = "",
            string _reference = "",
            Dictionary<string, string[]> _properties = null,
            FacebookDelegate callback = null)
        {
			if(!isLoggedIn) return;
			
			string url = "https://graph.facebook.com/" + _toId +"/feed?method=post";
			if(_link.Length > 0) url += "&link=" +_toId;
			if(_linkName.Length > 0) url += "&name=" +_linkName;
			if(_linkCaption.Length > 0) url += "&caption=" +_linkCaption;
			if(_linkDescription.Length > 0) url += "&description=" +_linkDescription;
			if(_picture.Length > 0) url += "&picture=" +_picture;
			if(_mediaSource.Length > 0) url += "&source=" +_mediaSource;
			
			if(_actionName.Length > 0 && _actionLink.Length > 0) { 
				url += string.Format("&actions=[{\"name\":\"{0}\",\"link\":\"{1}\"}]", _actionName, _actionLink);
			}

			if(_reference.Length > 0) url += "&type" +_reference;
			
			url += "&access_token=" + accessToken;
			
			StartCoroutine(GetFBResult(url, callback));
		}
        	

        public override void Pay(
            string product,
            string action = "purchaseitem",
            int quantity = 1,
            int? quantityMin = null,
            int? quantityMax = null,
            string requestId = null,
            string pricepointId = null,
            string testCurrency = null,
            FacebookDelegate callback = null)
        {
            throw new UnityException("There is no Facebook Pay Dialog on Windows Phone 8");
        }


        public override void GetAuthResponse(FacebookDelegate callback = null)
        {
            throw new UnityException("There is no Facebook Pay Dialog on Windows Phone 8");
        }
        
        public override void PublishInstall(string appId, FacebookDelegate callback = null) {}

		private IEnumerator GetFBResult(string url, FacebookDelegate fbDelegate) {
			WWW www = new WWW(url);
			yield return www;
			
			if(fbDelegate != null) fbDelegate(new FBResult(www));	
		}

		#region implemented abstract members of Facebook.AbstractFacebook
		public override void GetDeepLink (FacebookDelegate callback)
		{
			Debug.LogError("GetDeepLink not implemented on WP8");
		}

		public override void AppEventsLogEvent (string logEvent, System.Nullable<float> valueToSum, Dictionary<string, object> parameters)
		{
			Debug.LogError("AppEventsLogEvent not implemented on WP8");
		}

		public override void AppEventsLogPurchase (float logPurchase, string currency, Dictionary<string, object> parameters)
		{
			Debug.LogError("AppEventsLogPurchase not implemented on WP8");
		}

		public override void AppRequest (string message, OGActionType actionType, string objectId, string[] to, string filters, string[] excludeIds, int? maxRecipients, string data, string title, FacebookDelegate callback)
		{
			Debug.LogError("AppEventsLogPurchase not implemented on WP8");
		}

		public override bool LimitEventUsage {
			get {
				Debug.LogError("LimitEventUsage not implemented on WP8");
				return false;
			}
			set {
				Debug.LogError("LimitEventUsage not implemented on WP8");
			}
		}
		#endregion
	}
	
}

#endif