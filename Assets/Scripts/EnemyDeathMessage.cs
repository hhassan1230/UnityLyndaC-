using UnityEngine;
using System.Collections;

public class EnemyDeathMessage : MonoBehaviour {

	void OnTriggerEnter(Collider collider){
		if(collider.gameObject.tag == "Player"){
			gameObject.SendMessageUpwards ("OnDeath");
		}
	}
}
