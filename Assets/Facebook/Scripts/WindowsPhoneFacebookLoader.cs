using UnityEngine;
using System.Collections;
#if UNITY_WP8
namespace Facebook
{

	public class WindowsPhoneFacebookLoader : FB.CompiledFacebookLoader
	{
	
	    protected override IFacebook fb
	    {
	        get
	        {
	            return FBComponentFactory.GetComponent<WindowsPhoneFacebook>();
	        }
	    }
	}

}

#endif