using UnityEngine;
using System.Collections;

public class showMessage : MonoBehaviour {

		public	GameObject UIPanelMessageUI;

	bool isShowing = false;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if ((collision.tag == "Player") && !isShowing) {
			//CharacterController2D player = collision.gameObject.GetComponent<CharacterController2D>();
			//if (player.playerCanMove) {


			isShowing = true;
			showMessageUI ();
			Time.timeScale = 0f;



		}
	}


	void showMessageUI ()
	{

	UIPanelMessageUI.SetActive (true);
	UIPanelMessageUI.GetComponent<Animator> ().SetTrigger ("Show");

	}

	public void hideMessageUI ()
	{
		isShowing = false;
	Time.timeScale = 1f; 
	UIPanelMessageUI.GetComponent<Animator> ().SetTrigger ("Hide");

		//Essa função chama o UIPanelPauseToFalse depois de 2 segundos, tive que fazer isso para só desabilitar o PanelPauseUI depois que a animacao terminar
	Invoke ("DisableUIPanelMessage", 2.0f);


	}

void DisableUIPanelMessage ()
{
	UIPanelMessageUI.SetActive (false);
}
}
