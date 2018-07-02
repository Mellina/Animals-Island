using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelNavigation : MonoBehaviour {


	public AudioClip buttonSong;

	public GameObject gameObjectAnimation;
	public Button buttonLeftNavigation;
	public Button buttonRightNavigation;


	//Testar isso despois de ligar/desligar tela do celular/tablet
	void Awake(){
		buttonLeftNavigation.GetComponent<Button> ().interactable = false;
		//Se estiver na segunda parte e desligar/ligar a tela? O que acontece?
	}

	public void buttonLeftAction()
	{
		PlaySound (buttonSong);
		buttonLeftNavigation.GetComponent<Button> ().interactable = false;
		buttonRightNavigation.GetComponent<Button> ().interactable = true;
		gameObjectAnimation.GetComponent<Animator> ().SetTrigger ("Level1");

	}

	public void buttonRightAction()
	{
		PlaySound (buttonSong);
		buttonLeftNavigation.GetComponent<Button> ().interactable = true;
		buttonRightNavigation.GetComponent<Button> ().interactable = false;
		gameObjectAnimation.GetComponent<Animator> ().SetTrigger ("Level2");

	}

	// play sound through the audiosource on the gameobject
	void PlaySound(AudioClip clip)
	{
		if (PlayerPrefManager.GetSoundEffect () == 1) {
			GetComponent<AudioSource> ().PlayOneShot (clip);
		}
	}
}
