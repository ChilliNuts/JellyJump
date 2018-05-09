using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {


	JellyPlayer player;

	void Awake () {
		player = FindObjectOfType<JellyPlayer>();
	}
	
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player" && player.canScale){
			player.canScale = false;
			player.LoseJelly(player.scaleIncrement);

		}
	}
}
