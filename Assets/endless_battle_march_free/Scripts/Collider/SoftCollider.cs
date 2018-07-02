using UnityEngine;
using System.Collections;

public class SoftCollider : MonoBehaviour {

	public battle_master battle_script;
	
	void OnTriggerEnter(Collider other){
		
		battle_script.Soft_OnClick ();
		
	}
}
