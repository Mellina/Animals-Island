using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
 // include UI namespace so can reference UI elements

public class GameManager : MonoBehaviour
{

	// static reference to game manager so can be called from other scripts directly (not just through gameobject component)
	public static GameManager gm;
	public AudioClip buttonSong;
	// levels to move to on victory and lose
	public string levelAfterVictory;
	public string levelAfterGameOver;

	//Adicionais telas
	public string menuLevelSelect;


	// game performance
	public int score = 0;
	public int highscore = 0;
	public int startLives = 3;
	public int lives = 3;

	float _playerHealth = 100;

	//Test
	GameObject componentePlayer;

	// UI elements to control
	public Text UIScore;
	public Text UIHighScore;
	public Text UILevel;
	public Text UIExtraLives;


	public GameObject HealthProgressBar;
	//public GameObject[] UIExtraLives;
	public GameObject UIPanelPause;
	public GameObject buttonPause;
	public GameObject UIPanelWin;
	public GameObject UIPanelLose;

	//FPS
	public Text UI_FPS;
	int m_frameCounter = 0;
	float m_timeCounter = 0.0f;
	float m_lastFramerate = 0.0f;
	public float m_refreshTime = 0.5f;

	//CheckPoints
	public GameObject[] checkPoint;

	//TESTE
	CharacterController2D _playerCharacterController2D;
	RectTransform _healthProgressBarRectTransform;

	// private variables
	GameObject _player;
	Vector3 _spawnInitialLocation;
	Vector3[] _spawnCheckPointLocation = { new Vector3 { x = 0, y = 0, z = 0 }, 
		new Vector3 { x = 1, y = 1, z = 1} };
	// set things up here


	//TESTE
	void Start(){

		System.String appKey = "cfdb3396bbafa2d72738b14d7193f538bfac684ea615e29d";
		Appodeal.disableLocationPermissionCheck();
		Appodeal.initialize(appKey, Appodeal.INTERSTITIAL);
	//	Application.targetFrameRate = 60;

	}


	void Awake ()
	{
		
		PlayerPrefManager.resetCheckPoint();

		// setup reference to game manager
		if (gm == null)
			gm = this.GetComponent<GameManager> ();

		// setup all the variables, the UI, and provide errors if things not setup properly.
		setupDefaults ();
	}

	// game loop
	void Update ()
	{

		showFPS ();
		// if ESC pressed then pause the game
		if (Input.GetKeyDown (KeyCode.Escape)) {

			pauseGame ();
			/*	if (Time.timeScale > 0f) {
				//UIGamePaused.SetActive(true); // this brings up the pause UI
				Time.timeScale = 0f; // this pauses the game action
			} else {
				Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
				//UIGamePaused.SetActive(false); // remove the pause UI
			}*/
		}
	}

	void showFPS(){

		if( m_timeCounter < m_refreshTime )
		{
			m_timeCounter += Time.deltaTime;
			m_frameCounter++;
		}
		else
		{
			//This code will break if you set your m_refreshTime to 0, which makes no sense.
			m_lastFramerate = (float)m_frameCounter/m_timeCounter;
			m_frameCounter = 0;
			m_timeCounter = 0.0f;
		}




		UI_FPS.text = "FPS   "+m_lastFramerate;

	}

	public void pauseGame ()
	{


		PlaySound (buttonSong);
		//Pausa o Game
		if (Time.timeScale > 0f) {
			buttonPause.SetActive (false);
			showPauseUI ();
			Time.timeScale = 0f; // this pauses the game action
			//Unpause o Game
		} else {
			Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
			hidePauseUI ();
			buttonPause.SetActive (true);

		}
	}

	public void ativaCheckPoint(int posicao){

		PlayerPrefManager.setCheckPoint (posicao);


	}

	// setup all the variables, the UI, and provide errors if things not setup properly.
	void setupDefaults ()
	{
		// setup reference to player
		if (_player == null)
			_player = GameObject.FindGameObjectWithTag ("Player");

		//TESTE MEL
		_playerCharacterController2D = _player.GetComponent<CharacterController2D> ();
		_healthProgressBarRectTransform =     HealthProgressBar.GetComponent<RectTransform> ();

		if (_player == null)
			Debug.LogError ("Player not found in Game Manager");
		
		// get initial _spawnLocation based on initial position of player
		_spawnInitialLocation = _player.transform.position;



		//Seta posicao do checkPoint
		for (int i = 0; i < checkPoint.Length; i++) {
			_spawnCheckPointLocation[i] = checkPoint[i].transform.position;

		}

		// if levels not specified, default to current level
		if (levelAfterVictory == "") {
			Debug.LogWarning ("levelAfterVictory not specified, defaulted to current level");
			levelAfterVictory = Application.loadedLevelName;
		}
		
		if (levelAfterGameOver == "") {
			Debug.LogWarning ("levelAfterGameOver not specified, defaulted to current level");
			levelAfterGameOver = Application.loadedLevelName;
		}

		if (menuLevelSelect == "") {
			Debug.LogWarning ("menuLevelSelect not specified, defaulted to current level");
			levelAfterGameOver = Application.loadedLevelName;
		}

		// friendly error messages
		if (UIScore == null)
			Debug.LogError ("Need to set UIScore on Game Manager.");
		
		if (UIHighScore == null)
			Debug.LogError ("Need to set UIHighScore on Game Manager.");
		
		if (UILevel == null)
			Debug.LogError ("Need to set UILevel on Game Manager.");
		
		if (UIPanelPause == null)
			Debug.LogError ("Need to set UIPanelPause on Game Manager.");
		
		// get stored player prefs
		refreshPlayerState ();

		// get the UI ready for the game
		refreshGUI ();
	}

	// get stored Player Prefs if they exist, otherwise go with defaults set on gameObject
	void refreshPlayerState ()
	{
		lives = PlayerPrefManager.GetLives ();

		// special case if lives <= 0 then must be testing in editor, so reset the player prefs
		if (lives <= 0) {
			PlayerPrefManager.ResetPlayerState (startLives, false);
			lives = PlayerPrefManager.GetLives ();
		}
		score = PlayerPrefManager.GetScoreME ();
		highscore = PlayerPrefManager.GetHighscore ();

		// save that this level has been accessed so the MainMenu can enable it
		PlayerPrefManager.UnlockLevel ();

	}

	// refresh all the GUI elements
	void refreshGUI ()
	{
		
		// set the text elements of the UI
		UIScore.text = "" + score.ToString ();
		UIHighScore.text = "Highscore: " + highscore.ToString ();
		UIExtraLives.text = ""+lives;
		atualizaHealthUI ();

		UILevel.text = Application.loadedLevelName;



	/*	// turn on the appropriate number of life indicators in the UI based on the number of lives left
		for (int i = 0; i < UIExtraLives.Length; i++) {
			if (i < (lives - 1)) { // show one less than the number of lives since you only typically show lifes after the current life in UI
				UIExtraLives [i].SetActive (true);
			} else {
				UIExtraLives [i].SetActive (false);
			}
		} */
	}


	public void atualizaHealthUI(){
		

		//Pega o Health inicial do Player
		float HealthInicial = _playerCharacterController2D.playerHealthInicial;
	
		if (HealthInicial != 0) {



			float HealthAtual = _playerCharacterController2D.playerHealth;
		//Calcula percentual pos dano
		float PercentualPosDano = HealthAtual / HealthInicial;

		//posicao X inicial da barra de progresso
		float posInicial = HealthProgressBar.transform.localPosition.x;
		////Tamanho inicial da barra na tela
			float tamanhoInicial = _healthProgressBarRectTransform.localScale.x * 100;
		//Tamanho inicial da barra na tela
			float tamanhoAtual = _healthProgressBarRectTransform.localScale.x * _healthProgressBarRectTransform.rect.width;
		//calcula tamanho final da barra pos dano
		float tamanhoFinal = tamanhoInicial*PercentualPosDano; 
		//ajusta a posicao X para tirar a diferenca do pedaco da barra que foi diminuido
		float diferencaPosicaoInicial = (tamanhoInicial*(1f - PercentualPosDano))/2f; 


/*			Debug.LogWarning ("Health INICIAL  " + _playerCharacterController2D.playerHealthInicial);
			Debug.LogWarning ("Health Atual  " + _playerCharacterController2D.playerHealth);

		Debug.LogWarning ("posInicial  " + posInicial);
		Debug.LogWarning ("tamanhoInicial  " + tamanhoInicial);
		Debug.LogWarning ("tamanhoFinal  " + tamanhoFinal);
		Debug.LogWarning ("diferencaPosicaoInicial  " + diferencaPosicaoInicial);
*/


		//TODO não colocar valores fixos, pegar do codigo
		//Preciso de armazenar a posicao inicial da barra
		HealthProgressBar.transform.localPosition = new Vector3(4.9f - diferencaPosicaoInicial, HealthProgressBar.transform.localPosition.y, 1);
		//Preciso de armazenar o tamanho inicial da barra
			_healthProgressBarRectTransform.sizeDelta = new Vector2(100*PercentualPosDano,_healthProgressBarRectTransform.rect.height);
			//Teste
			_playerHealth = 100 * PercentualPosDano;
		}


	}


	// public function to add points and update the gui and highscore player prefs accordingly
	public void AddPoints (int amount)
	{


		// increase score
		score += amount;

		// update UI
		UIScore.text = "" + score.ToString ();

		// if score>highscore then update the highscore UI too
	/*	if (score > highscore) {
			highscore = score;
			UIHighScore.text = "Highscore: " + score.ToString ();
		}
*/
	}

	// public function to remove player life and reset game accordingly
	public void ResetGame ()
	{
	/*	if (PlayerPrefManager.GetSound () == 1) {
			GameManager.gm.GetComponent<AudioSource> ().mute = false;
		}*/
		// remove life and update GUI
		lives--;
		//refreshGUI ();

		//Game Over
		if (lives <= 0) { // no more lives
			// save the current player prefs before going to GameOver
			PlayerPrefManager.SavePlayerState (score, highscore, lives);

			// load the gameOver screen
			//	Application.LoadLevel (levelAfterGameOver);
			PlayerPrefManager.resetCheckPoint();

			showLoseUI ();
			//CheckPoint
		} else { // tell the player to respawn
			//Acho que nesse _spawnLocation se eu passar a posicao do jogador no meio da fase, ele loga no meio da fase depois que morrer mas ainda tiver vida.
			//Acho que é aqui que devo passar a localização da "bandeira" que salva a posicao no meio do jogo para o personagem não ter que comecar do inicio.

			if (PlayerPrefManager.GetCheckPoint() > -1) {

				//posicao do checkpoint
				_playerCharacterController2D.Respawn (_spawnCheckPointLocation[PlayerPrefManager.GetCheckPoint()]);
			} else {
				//Posicao inicial
				_playerCharacterController2D.Respawn (_spawnInitialLocation);
			}

	
		}
		refreshGUI ();
	}

	// public function for level complete
	public void LevelCompete ()
	{
		// save the current player prefs before moving to the next level
		PlayerPrefManager.SavePlayerState (score, highscore, lives);

		//Aqui faço as contas para a estrela

		Debug.LogError ("score   "+score);
		Debug.LogError ("lives   "+lives);
		Debug.LogError ("_playerHealth   "+_playerHealth);


		int completeCat = 0;
		int completeHeart = 0;
		int completeDiamond = 0;
		
		int star1 = 0;
		int star2 = 0;
		int star3 = 0;
		
		int estrelasTotais = 0;

		if (score > 100) { estrelasTotais++; completeDiamond = 1; }
		if (lives > 3) { estrelasTotais++; completeCat = 1; }
		if (_playerHealth > 90) { estrelasTotais++; completeHeart = 1; }

		if(estrelasTotais > 0 ){  star1 = 1; }
		if(estrelasTotais > 1){  star2 = 1; }
		if(estrelasTotais > 2){  star3 = 1; }


		PlayerPrefManager.SaveLevelStars ("" + Application.loadedLevelName, 1, star1);
		PlayerPrefManager.SaveLevelStars ("" + Application.loadedLevelName, 2, star2);
		PlayerPrefManager.SaveLevelStars ("" + Application.loadedLevelName, 3, star3);

		//Vou por isso em outro método
		// use a coroutine to allow the player to get fanfare before moving to next level
		//StartCoroutine(LoadNextLevel());

		//Mostra os informacoes na Win UI
		Image[] nameComp = UIPanelWin.GetComponentsInChildren<Image> ();
		Text[] scoreComp = UIPanelWin.GetComponentsInChildren<Text> ();

		string stringAccept = "Accept btn 1";
		string stringFailed = "X btn";

		//verifica o nome de todos componentes do UIPanelWin e se tiver o nome de Star, eu seto a Star correspondente
		for (int nc = 0; nc < nameComp.Length; nc++) {

			if (nameComp [nc].name == "star1") {
				if (star1 == 1) {
					nameComp [nc].sprite = Resources.Load<Sprite> ("WinPanel/Star 1");
				} else {
					nameComp [nc].sprite = Resources.Load<Sprite> ("WinPanel/Star_1_des");
				}

			} else if (nameComp [nc].name == "star2") {
				if (star2 == 1) {
					nameComp [nc].sprite = Resources.Load<Sprite> ("WinPanel/Star 2");
				} else {
					nameComp [nc].sprite = Resources.Load<Sprite> ("WinPanel/Star_2_des");
				}


			} else if (nameComp [nc].name == "star3") {
				if (star3 == 1) {
					nameComp [nc].sprite = Resources.Load<Sprite> ("WinPanel/Star 3");
				} else {
					nameComp [nc].sprite = Resources.Load<Sprite> ("WinPanel/Star_3_des");
				}

			} else if (nameComp [nc].name == "completeCat") {
				if (completeCat == 1) { 
					nameComp [nc].sprite = Resources.Load<Sprite> (stringAccept);
				} else {
					nameComp [nc].sprite = Resources.Load<Sprite> (stringFailed);
				}
			} else if (nameComp [nc].name == "completeHeart") {
				if (completeHeart == 1) { 
					nameComp [nc].sprite = Resources.Load<Sprite> (stringAccept);
				} else {
					nameComp [nc].sprite = Resources.Load<Sprite> (stringFailed);
				}
			} else if (nameComp [nc].name == "completeMoney") {
				if (completeDiamond == 1) { 
					nameComp [nc].sprite = Resources.Load<Sprite> (stringAccept);
				} else {
					nameComp [nc].sprite = Resources.Load<Sprite> (stringFailed);
				}
			} 

		
		}



		showWinUI ();
	}


	void showLoseUI ()
	{
		UIPanelLose.SetActive (true);
		UIPanelLose.GetComponent<Animator> ().SetTrigger ("Show");
	}

	void showWinUI ()
	{


		//Mostra propaganda
		Appodeal.show(Appodeal.INTERSTITIAL);


		UIPanelWin.SetActive (true);
		UIPanelWin.GetComponent<Animator> ().SetTrigger ("Show");

	}


	void showPauseUI ()
	{
		GameManager.gm.GetComponent<AudioSource> ().mute = true;
		UIPanelPause.SetActive (true);
		UIPanelPause.GetComponent<Animator> ().SetTrigger ("Show");

	}

	void hidePauseUI ()
	{
		if (PlayerPrefManager.GetSound () == 1) {
			GameManager.gm.GetComponent<AudioSource> ().mute = false;
		}

		UIPanelPause.GetComponent<Animator> ().SetTrigger ("Hide");

		//Essa função chama o UIPanelPauseToFalse depois de 2 segundos, tive que fazer isso para só desabilitar o PanelPauseUI depois que a animacao terminar
		Invoke ("DisableUIPanelPause", 2.0f);
	

	}



	void DisableUIPanelPause ()
	{
		UIPanelPause.SetActive (false);
	}

	public void NextLevel ()
	{
		PlaySound (buttonSong);
		// use a coroutine to allow the player to get fanfare before moving to next level
		StartCoroutine (LoadNextLevel ());
	}

	// load the nextLevel after delay
	IEnumerator LoadNextLevel ()
	{
		yield return new WaitForSeconds (0.1f); 
		Application.LoadLevel (levelAfterVictory);
	}

	//Carrega a mesma fase de novo
	public void LoadLevelSelect ()
	{
		UnpauseGame ();
		DisableUIPanelPause ();
		Application.LoadLevel (menuLevelSelect);

	}

	//Carrega a mesma fase de novo
	public void LoadSameLevel ()
	{
		PlaySound (buttonSong);

		//Se o jogo estiver pausado, volto o timeScale para 1.
		if (Time.timeScale < 0f) {
		
			Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
			hidePauseUI ();
			buttonPause.SetActive (true);
		}

		Application.LoadLevel (Application.loadedLevelName);

	}


	public void ReloadGame ()
	{
		PlaySound (buttonSong);
		Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
		hidePauseUI ();
		buttonPause.SetActive (true);
		DisableUIPanelPause ();
		LoadSameLevel ();
	}

	public void UnpauseGame ()
	{
		PlaySound (buttonSong);
		Time.timeScale = 1f; // this unpauses the game action (ie. back to normal)
		hidePauseUI ();
		buttonPause.SetActive (true);
	
	}

	// play sound through the audiosource on the gameobject
	void PlaySound(AudioClip clip)
	{
		if (PlayerPrefManager.GetSoundEffect () == 1) {
			GetComponent<AudioSource> ().PlayOneShot (clip);
		}
	}


}
