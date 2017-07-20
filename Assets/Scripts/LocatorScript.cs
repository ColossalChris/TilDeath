using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Utils;
using Mapbox.Unity.Map;

public class LocatorScript : MonoBehaviour {

	AbstractMap map;

	[SerializeField]
	Text locationText;

	// Use this for initialization
	IEnumerator Start()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			locationText.text = "Timed out";
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			locationText.text = "Unable to determine device location";
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			locationText.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude
				+ " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
			map.CenterLatitudeLongitude = new Vector2d(Input.location.lastData.latitude, Input.location.lastData.longitude);
		}

		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
	}

	// Update is called once per frame
	void Update () {

	}
}