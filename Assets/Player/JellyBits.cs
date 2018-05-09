using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBits : MonoBehaviour {


	Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	public void BitJumps(object[] message){
		Vector2 force = (Vector2) message[0];
		float torque = (float) message[1];
		rb2d.AddForce(force);
		rb2d.AddTorque(torque);
	}
}
