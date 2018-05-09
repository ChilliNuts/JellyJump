using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : MonoBehaviour {

	JellyPlayer player;

	void Awake () {
		player = FindObjectOfType<JellyPlayer>();
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject == player.gameObject){

			//TODO: Simple reset for prototyping... Make something better.
			Application.LoadLevel(0);
		}
		if(other.gameObject.tag == "JellyBits"){

			Destroy(other.gameObject);
		}
	}
}
