using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

	[HideInInspector]public bool startDrag = false;

	[HideInInspector]public JellySprite jelly;
	public LayerMask groundLayer;
	public bool isGrounded = false;
	public float dropDelay = 0.15f;
	[HideInInspector] public Vector3 jumpForce;
	PlotTrajectory trajectory;
	Ray2D dragRay;
	Vector3 mPos;
	float tempDelay;
	public float MaxForce = 50f;
	public float forceMultiplier = 2000f;


	// Use this for initialization
	void Start () {
		
		jelly = GetComponent<JellySprite>();
		trajectory = FindObjectOfType<PlotTrajectory>();
		dragRay = new Ray2D(transform.position, trajectory.transform.position);

	}

	// Update is called once per frame
	void Update () {

		Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y -20f), Color.green);

		if(jelly.IsGrounded(groundLayer,1)){
			if(tempDelay >= 0f) {
				tempDelay -= Time.deltaTime;
			}else{
				isGrounded = true;
				tempDelay = dropDelay;
			}
		}else if(isGrounded) {
			if (!Physics2D.Raycast(transform.position, Vector2.down, 20f, groundLayer)) {
				if (tempDelay >= 0f) {
					tempDelay -= Time.deltaTime;
				} else {
					isGrounded = false;
					StopDrag ();
					tempDelay = dropDelay;
				}
			}

		}

		CheckForMouseDown();

		if(startDrag){
			print("dragging = " + startDrag);

			Dragging();
			CheckForMouseUp();
		}
//		else if (Input.GetMouseButtonDown(0) && !jelly.IsGrounded(groundLayer, 2) && Camera.main.ScreenToWorldPoint(Input.mousePosition).y < transform.position.y){
//
//			mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//			Vector2 slamForce = transform.position - mPos;
//			jelly.AddForce(-slamForce * 3f);
//		}
	}


	void CheckForMouseDown(){
		if(Input.GetMouseButtonDown(0)){
			float selectRadius = (jelly.m_SpriteScale.x * 2f) + 1f;

			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0;

			if(Vector2.Distance(transform.position, mousePos) <= selectRadius){
				
				if (isGrounded && !startDrag) {
					startDrag = true;

					trajectory.transform.position = transform.position;
					trajectory.showArc = true;
					trajectory.StartFade (false);
				}
			}
		}
	}

	void CheckForMouseUp(){
		if(Input.GetMouseButtonUp(0)){
			StopDrag();
			isGrounded = false;
			jelly.AddForce (jumpForce);

			jelly.CentralPoint.Body2D.AddTorque ((mPos.x - transform.position.x)  * (jelly.m_Mass * jelly.m_Mass));
		}
	}

//	void OnMouseDown(){
//
//		if (isGrounded) {
//			startDrag = true;
//			
//			trajectory.transform.position = transform.position;
//			trajectory.showArc = true;
//			trajectory.StartFade (false);
//		}
//	}

	void Dragging(){

		if (isGrounded) {
			
			trajectory.showArc = true;
			trajectory.StartFade (false);
		}


		mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mPos.z = 0;
		Vector3 dragDir = mPos - transform.position;
		jumpForce = transform.position - mPos;

		//Vector2 jumpDir = mPos - transform.position;
		if(Vector2.Distance(transform.position, mPos) > MaxForce){
			dragRay.origin = transform.position;
			dragRay.direction = dragDir;
			mPos = dragRay.GetPoint(MaxForce);
		}
		jumpForce = Vector3.ClampMagnitude(jumpForce, MaxForce);

		//jumpForce = transform.position - mPos;
		//jumpForce = jumpForce.normalized * Mathf.Clamp(jumpForce.sqrMagnitude, 0, MaxForce);
		jumpForce *= forceMultiplier;

		Debug.DrawLine(mPos, transform.position, Color.red);
		//Debug.Log(Physics2D.Linecast(mPos, transform.position, mask).point);

	}


//	void OnMouseUp(){
//		if (startDrag) {
//			StopDrag();
//			isGrounded = false;
//			jelly.AddForce (jumpForce);
//			
//			jelly.CentralPoint.Body2D.AddTorque ((mPos.x - transform.position.x)  * (jelly.m_Mass * jelly.m_Mass));
//		}
//	}
	void StopDrag(){
		startDrag = false;

		trajectory.showArc = false;
		trajectory.StartFade (true);
	}

}
