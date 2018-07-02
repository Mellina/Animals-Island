using UnityEngine;
using System.Collections;

public class StopCollider : MonoBehaviour {
	public battle_master battle_script;

	void OnTriggerEnter(Collider other){

		battle_script.Stop_OnClick ();

	}
}
