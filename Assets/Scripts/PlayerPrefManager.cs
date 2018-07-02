using UnityEngine;
using System.Collections;

public static class PlayerPrefManager {

	public static int GetLives() {
		if (PlayerPrefs.HasKey("Lives")) {
			return PlayerPrefs.GetInt("Lives");
		} else {
			return 0;
		}
	}

	public static void SetLives(int lives) {
		PlayerPrefs.SetInt("Lives",lives);
	}
	/*
	public static int GetScore() {
		if (PlayerPrefs.HasKey("ScoreCat")) {
			return PlayerPrefs.GetInt("ScoreCat");
		} else {
			return 0;
		}
	}

	public static void SetScore(int score) {
		PlayerPrefs.SetInt("ScoreCat",score);
	}
	*/
	public static int GetScoreME() {
		if (PlayerPrefs.HasKey("Sc")) {
			return PlayerPrefs.GetInt("Sc");
		} else {
			return 0;
		}
	}

	public static void SetScoreME(int score) {
		PlayerPrefs.SetInt("Sc",score);
	}

	public static int GetHighscore() {
	/*	if (PlayerPrefs.HasKey("Highscore")) {
			return PlayerPrefs.GetInt("Highscore");
		} else {
			return 0;
		}

*/ 
		//Teste
		return 0;
	}

	public static void SetHighscore(int highscore) {
		//PlayerPrefs.SetInt("Highscore",highscore);
	}


	//1 - ON, 0 - OFF
	public static void setSound(int on_off){
		PlayerPrefs.SetInt("Sound",on_off);
	}

	public static int GetSound() {
		if (PlayerPrefs.HasKey("Sound")) {
			return PlayerPrefs.GetInt("Sound");
		} else {
			return 1;
		}
	}

	//1 - ON, 0 - OFF
	public static void setSoundEffect(int on_off){
		PlayerPrefs.SetInt("SoundEffect",on_off);
	}

	public static int GetSoundEffect() {
		if (PlayerPrefs.HasKey("SoundEffect")) {
			return PlayerPrefs.GetInt("SoundEffect");
		} else {
			return 1;
		}
	}

	public static void setCheckPoint(int posicao){
		PlayerPrefs.SetInt("Checkpoint",posicao);
	}

	public static void resetCheckPoint(){
		PlayerPrefs.SetInt("Checkpoint",-1);
	}

	public static int GetCheckPoint() {
		if (PlayerPrefs.HasKey("Checkpoint")) {
			return PlayerPrefs.GetInt("Checkpoint");
		} else {
			return -1;
		}
	}

	// story the current player state info into PlayerPrefs
	public static void SavePlayerState(int score, int highScore, int lives) {
		// save currentscore and lives to PlayerPrefs for moving to next level
		PlayerPrefs.SetInt("ScoreCat",score);
		PlayerPrefs.SetInt("Lives",lives);
		PlayerPrefs.SetInt("Highscore",highScore);
	}
	
	// reset stored player state and variables back to defaults
	public static void ResetPlayerState(int startLives, bool resetHighscore) {
		
		PlayerPrefs.SetInt("Lives",startLives);
		PlayerPrefs.SetInt("ScoreCat", 0);

		if (resetHighscore)
			PlayerPrefs.SetInt("Highscore", 0);
	}

	// story the current player state info into PlayerPrefs
	public static void SaveLevelStars(string levelName, int star, int starState) {
		// save currentscore and lives to PlayerPrefs for moving to next level
		PlayerPrefs.SetInt("LevelStar_"+levelName+"_"+star,starState);
	

	}

	public static int GetLevelStars(string levelName, int star) {
		
		if (PlayerPrefs.HasKey("LevelStar_"+levelName+"_"+star)) {
			return PlayerPrefs.GetInt("LevelStar_"+levelName+"_"+star);
		} else {
			return -1;
		}
	}

	// store a key for the name of the current level to indicate it is unlocked
	public static void UnlockLevel() {	
		PlayerPrefs.SetInt(Application.loadedLevelName,1);
	}

	// determine if a levelname is currently unlocked (i.e., it has a key set)
	public static bool LevelIsUnlocked(string levelName) {
		return (PlayerPrefs.HasKey(levelName));
	}

	// output the defined Player Prefs to the console
	public static void ShowPlayerPrefs() {
		// store the PlayerPref keys to output to the console
		string[] values = {"ScoreCat","Highscore","Lives"};

		// loop over the values and output to the console
		foreach(string value in values) {
			if (PlayerPrefs.HasKey(value)) {
				Debug.Log (value+" = "+PlayerPrefs.GetInt(value));
			} else {
				Debug.Log (value+" is not set.");
			}
		}
	}

	// output the defined Player Prefs to the console
	public static void ShowCheckpoint() {
		// store the PlayerPref keys to output to the console
		string[] values = {"Checkpoint"};

		// loop over the values and output to the console
		foreach(string value in values) {
			if (PlayerPrefs.HasKey(value)) {
				Debug.Log (value+" = "+PlayerPrefs.GetInt(value));
			} else {
				Debug.Log (value+" is not set.");
			}
		}
	}
}
