using UnityEngine;
using System.Collections;

public class MedCollider : MonoBehaviour {
	public battle_master battle_script;
	
	void OnTriggerEnter(Collider other){
		
		battle_script.Med_OnClick ();
		
	}
}
