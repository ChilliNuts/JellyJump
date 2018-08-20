using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {


	PlayerManager playerManager;

	void Awake () {
		playerManager = FindObjectOfType<PlayerManager>();
	}
	
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player" && playerManager.canScale){
			playerManager.Scale(-1);
//			player.canScale = false;
//			player.LoseJelly(player.scaleIncrement);

		}
	}
}
