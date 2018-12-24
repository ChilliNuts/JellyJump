using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

	[HideInInspector]public bool startDrag = false;

	[HideInInspector]public JellySprite jelly;
	public LayerMask groundLayer;
	[HideInInspector] public Vector3 jumpForce;
	public GameObject trajectory;
	Ray2D dragRay;
	Vector3 mPos;
	public float MaxForce = 50f;
	public float forceMultiplier = 2000f;

	// Use this for initialization
	void Start () {
		jelly = GetComponent<JellySprite>();
		dragRay = new Ray2D(transform.position, trajectory.transform.position);
	}

	// Update is called once per frame
	void Update () {
		if(startDrag){
			Dragging();
		}else if (Input.GetMouseButtonDown(0) && !jelly.IsGrounded(groundLayer, 2) && Camera.main.ScreenToWorldPoint(Input.mousePosition).y < transform.position.y){

			mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 slamForce = transform.position - mPos;
			jelly.AddForce(-slamForce * 3f);
		}
	}

	void OnMouseDown(){

		startDrag = true;
	}

	void Dragging(){
		//test code... update!!!
		FindObjectOfType<PlotTrajectory>().transform.position = this.transform.position;

		trajectory.transform.position = transform.position;
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


	void OnMouseUp(){
		startDrag = false;
		jelly.AddForce(jumpForce);

		jelly.CentralPoint.Body2D.AddTorque((mPos.x - transform.position.x) * (forceMultiplier));
	}

}
