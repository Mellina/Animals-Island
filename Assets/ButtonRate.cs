using UnityEngine;
using System.Collections;

public class ButtonRate : MonoBehaviour {

		public string iosRateURL = "" ;
		public string androidRateURL = "";
		public string amazonRateURL = "";

		public bool isAmazon = false;

	public AudioClip buttonSong;
		AudioSource _audio;

	void Awake(){
		_audio = GetComponent<AudioSource> ();
		if (_audio==null) { // if AudioSource is missing
			//			Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
			// let's just add the AudioSource component dynamically
			_audio = gameObject.AddComponent<AudioSource>();
		}
	}
		public void OnClickedRate()
		{
		PlaySound (buttonSong);
			float startTime;
			startTime = Time.timeSinceLevelLoad;

			string URL = "";

			#if UNITY_IOS
			URL = iosRateURL;
			#else
			URL = androidRateURL;
			if(isAmazon)
				URL = amazonRateURL;
			#endif

			Application.OpenURL(URL);


		}


	// play sound through the audiosource on the gameobject
	void PlaySound(AudioClip clip)
	{
		if (PlayerPrefManager.GetSoundEffect () == 1) {
			_audio.PlayOneShot (clip);
		}
	}

}
