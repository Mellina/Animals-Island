using UnityEngine;
using System.Collections;

public class MenuButtonLoadLevel : MonoBehaviour {

	public AudioClip buttonSong;


	public void loadLevel(string leveltoLoad)
	{
		PlaySound (buttonSong);
		Application.LoadLevel (leveltoLoad);
	}

	// play sound through the audiosource on the gameobject
	void PlaySound(AudioClip clip)
	{
		if (PlayerPrefManager.GetSoundEffect () == 1) {
			GetComponent<AudioSource> ().PlayOneShot (clip);
		}
	}
}
