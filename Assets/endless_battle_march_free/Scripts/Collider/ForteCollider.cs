using UnityEngine;
using System.Collections;

public class ForteCollider : MonoBehaviour {

	public battle_master battle_script;
	
	void OnTriggerEnter(Collider other){
		
		battle_script.Forte_OnClick ();
		
	}
}
