using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour {


	public Vector2 origin = new Vector2(0,-6666);
	[HideInInspector] public CircleCollider2D myCollider;
	public SpriteRenderer myRend;
	[HideInInspector] public bool hitGround;

	void Awake(){
		myRend = GetComponent<SpriteRenderer>();
		myCollider = GetComponent<CircleCollider2D>();
		transform.position = origin;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
			hitGround = true;
		}
	}
	void OnTriggerExit2D(){
		hitGround = false;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
