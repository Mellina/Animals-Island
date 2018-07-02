using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public bool state = false;
	public int posicao;
	public GameObject hairFlower;

	// if the player touches the coin, it has not already been taken, and the player can move (not dead or victory)
	// then take the coin
	void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.tag == "Player" ) && (!state) && (other.gameObject.GetComponent<CharacterController2D>().playerCanMove))
		{
			// mark as taken so doesn't get taken multiple times
			state = true;



			// do the player checkpoint thing
		
			other.gameObject.GetComponent<CharacterController2D>().passaCheckPoint(posicao);
			hairFlower.GetComponent<Animator> ().SetTrigger ("checkpoint");
		
		}
	}
}
