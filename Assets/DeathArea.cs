using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour {

	PlayerManager player;

	void Awake () {
		player = FindObjectOfType<PlayerManager>();
	}


	void OnTriggerEnter2D(Collider2D other){
		
		if(other.gameObject == player.currentPlayer.myJelly.CentralPoint.GameObject){

			//TODO: Simple reset for prototyping... Make something better.
			Application.LoadLevel(0);
		}
		if(other.gameObject.tag == "JellyBits"){

			Destroy(other.gameObject);
		}
	}
}
