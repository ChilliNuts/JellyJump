using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour {


	public Vector2 origin = new Vector2(0,-6666);
	[HideInInspector] public CircleCollider2D myCollider;
	public SpriteRenderer myRend;
	[HideInInspector] public bool hitGround;
	public float fadeTime = 0.5f;
	public bool fading;

	void Awake(){
		myRend = GetComponent<SpriteRenderer>();
		myCollider = GetComponent<CircleCollider2D>();
		transform.position = origin;
		Color c = myRend.color;
		c.a = 0;
		myRend.color = c;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
			hitGround = true;
		}
	}
	void OnTriggerExit2D(){
		hitGround = false;
	}

	void FadeDot(){
		Color tempColor = myRend.color;
		if (myRend.color.a > 0) {
			tempColor.a -= Time.deltaTime / fadeTime;
			myRend.color = tempColor;
		}else {
			fading = false;
			transform.position = origin;
		}
	}

	void Update(){
		if(fading){
			FadeDot();
		}
	}
}

//	public void FadeLine (bool fadeOut){
//		Color startColor = myRend.color;
//		Color endColor = Color.clear;
//		if (fadeOut) {
//			if (myRend.color.a > 0) {
//				startColor.a -= Time.deltaTime * fadeTime;
//				myRend.color = startColor;
//			}
//		}else if(!fadeOut){
//			if (myRend.color.a < visibleColorState.a) {
//				startColor.a += Time.deltaTime * fadeTime;
//				myRend.color = startColor;
//			}
//		}
//	}

