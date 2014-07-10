using UnityEngine;
using System.Collections;
using UnityGoogleAnalytics;

public class UniRateEventHandler : MonoBehaviour {

	void Start () {
        this.GetComponent<UniRate>().OnUserAttemptToRate += UniRateEventHandler_OnUserAttemptToRate;
        this.GetComponent<UniRate>().OnUserWantReminderToRate += UniRateEventHandler_OnUserWantReminderToRate;
        this.GetComponent<UniRate>().OnUserDeclinedToRate += UniRateEventHandler_OnUserDeclinedToRate;
	}

    private void UniRateEventHandler_OnUserAttemptToRate()
    {
        Track.Event("Rate", "Attempt");
    }

    private void UniRateEventHandler_OnUserWantReminderToRate()
    {
        Track.Event("Rate", "Reminder");
    }

    private void UniRateEventHandler_OnUserDeclinedToRate()
    {
        Track.Event("Rate", "Declined");
    }
	
}
