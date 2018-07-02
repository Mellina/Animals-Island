using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class audioControl : MonoBehaviour {



	public GameObject blockImage;
	public AudioClip buttonSong;
	public enum ButtonType {Sound, SoundEffect}
	public ButtonType buttontype;
	AudioSource _audio;
	public GameObject audioMain;

	void Awake(){




		if (buttontype == ButtonType.Sound) {
			if (PlayerPrefManager.GetSound () == 1) {
				blockImage.SetActive (false);
			} else {
				blockImage.SetActive (true);
			}

		}else 

			if (buttontype == ButtonType.SoundEffect) {
				if (PlayerPrefManager.GetSoundEffect () == 1) {
					blockImage.SetActive (false);
				} else {
					blockImage.SetActive (true);
				}

			}





		_audio = GetComponent<AudioSource> ();
		if (_audio==null) { // if AudioSource is missing
			//			Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
			// let's just add the AudioSource component dynamically
			_audio = gameObject.AddComponent<AudioSource>();
		}
	}

	public void OnClickedSoundEnable()
	{
		if (audioMain != null) {
			audioMain.GetComponent<AudioSource> ().mute = false;
		}
		blockImage.SetActive (false);
		PlayerPrefManager.setSound (1);
		PlaySound (buttonSong);
		MainMenuManager.MusicMenuManager (true);
	}


	public void OnClickedSoundDisable()
	{
		if (audioMain != null) {
			audioMain.GetComponent<AudioSource> ().mute = true;
		}
		blockImage.SetActive (true);
		PlayerPrefManager.setSound (0);
		PlaySound (buttonSong);
		MainMenuManager.MusicMenuManager (false);

	}

	public void OnClickedSoundEffectEnable()
	{
		blockImage.SetActive (false);
		PlayerPrefManager.setSoundEffect (1);
		PlaySound (buttonSong);

	}


	public void OnClickedSoundEffectDisable()
	{
		blockImage.SetActive (true);
		PlayerPrefManager.setSoundEffect (0);
		PlaySound (buttonSong);

	}


	// play sound through the audiosource on the gameobject
	void PlaySound(AudioClip clip)
	{
		if (PlayerPrefManager.GetSoundEffect () == 1) {
			_audio.PlayOneShot (clip);
		}
	}
}
