using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraProxy : MonoBehaviour {

	PlayerManager player;
	[HideInInspector] public bool skipFrame = false;


	// Use this for initialization
	void Awake () {
		player = FindObjectOfType<PlayerManager>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(!skipFrame){
			transform.position = player.currentPlayer.transform.position;
		}else if(skipFrame){
			skipFrame = false;
		}

	}
}
