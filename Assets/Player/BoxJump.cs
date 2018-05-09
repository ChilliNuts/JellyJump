using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxJump : MonoBehaviour {

	[HideInInspector]public bool startDrag = false;
	Rigidbody2D rb2d;
	BoxCollider2D box2d;
	public LayerMask mask;
	[HideInInspector] public Vector3 jumpForce;
	public GameObject DragCircle;
	Ray2D dragRay;
	Vector3 mPos;
	public float MaxForce = 50f;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		box2d = GetComponent<BoxCollider2D>();
		dragRay = new Ray2D(transform.position, DragCircle.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if(startDrag){
			Dragging();
		}else if (Input.GetMouseButtonDown(0) && !box2d.IsTouchingLayers() && Camera.main.ScreenToWorldPoint(Input.mousePosition).y < transform.position.y){
			
			mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 slamForce = transform.position - mPos;
			rb2d.AddForce(-slamForce * 3f, ForceMode2D.Impulse);
		}
	}

	void OnMouseDown(){
		
		startDrag = true;
	}

	void Dragging(){
		
		DragCircle.transform.position = transform.position;
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
		jumpForce *= 200f;

		Debug.DrawLine(mPos, transform.position, Color.red);
		//Debug.Log(Physics2D.Linecast(mPos, transform.position, mask).point);


	}


	void OnMouseUp(){
		startDrag = false;
		float torque = (transform.position.x - Physics2D.Linecast(mPos, transform.position).point.x) *-25f;
		rb2d.AddForce(jumpForce);
		rb2d.AddTorque(torque);
		object[] message = SetBroadcast(jumpForce, torque);
		BroadcastMessage("BitJumps", message);
	}
	object[] SetBroadcast(Vector2 jumpForce, float torque){
		object[] broadcast = new object[2];
		broadcast[0] = (Vector2)jumpForce;
		broadcast[1] = (float)torque;
		return broadcast;
	}
}
