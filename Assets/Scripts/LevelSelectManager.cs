using UnityEngine;
using System.Collections;
using UnityEngine.UI; // include UI namespace since references UI Buttons directly

public class LevelSelectManager : MonoBehaviour {


	// list the level names
	public Button[] LevelButtons;

	public Sprite []spriteButtonsUnlock;
	public Sprite spriteButtonlock;
	GameObject []spriteStar;

	public Sprite starYellow;
	public Sprite starBrown;
	public Sprite starDisable;

	public AudioClip buttonSong;
	void Awake()
	{

		if (PlayerPrefManager.GetSound () == 1) {
			GetComponent<AudioSource> ().mute = false;
		} else {
			GetComponent<AudioSource> ().mute = true;
		}

		// disable/enable Level buttons based on player progress
		setLevelSelect();

		/*Debug.LogError ("1_1  " + PlayerPrefManager.GetLevelStars ("Level 1", 1));
		Debug.LogError ("1_2  " + PlayerPrefManager.GetLevelStars ("Level 1", 2));
		Debug.LogError ("1_3  " + PlayerPrefManager.GetLevelStars ("Level 1", 3));

		Debug.LogError ("2_1  " + PlayerPrefManager.GetLevelStars ("Level 2", 1));
		Debug.LogError ("2_2  " + PlayerPrefManager.GetLevelStars ("Level 2", 2));
		Debug.LogError ("2_3  " + PlayerPrefManager.GetLevelStars ("Level 2", 3));
		*/
	}

	// loop through all the LevelButtons and set them to interactable 
	// based on if PlayerPref key is set for the level.
	void setLevelSelect() {


		// loop through each levelName defined in the editor
		for (int i = 0; i < LevelButtons.Length; i++) {


			string levelname = LevelButtons [i].GetComponent<Button> ().name;

			//Pega o componente das estrelas de cada botao
			Image[] stars = LevelButtons [i].GetComponentsInChildren<Image> ();

			// determine if the button should be interactable based on if the level is unlocked
			if (PlayerPrefManager.LevelIsUnlocked (levelname)) {
				//Habilita o botão
				LevelButtons [i].interactable = true;
				//Altera a imagem para o numero da fase
				LevelButtons [i].GetComponent<Image> ().sprite = spriteButtonsUnlock [i];

			


				//Se existir informacao para a star 1, existe para as outras
				//	if (PlayerPrefManager.GetLevelStars (levelname, 1) != -1) {

				//Adiciona as imagens de estrela
				for (int s = 1; s < 4; s++) {
					
					if (PlayerPrefManager.GetLevelStars (levelname, s) == 1) {
						stars [s].sprite = starYellow;

					} else {
						stars [s].sprite = starBrown;
					}
						

				}

			} else {

				for (int s = 1; s < 4; s++) {

					stars [s].sprite = starDisable;
				}


				LevelButtons[i].interactable = false;
			

				LevelButtons [i].GetComponent<Image> ().sprite = spriteButtonlock;

			}
		}
	}


}
