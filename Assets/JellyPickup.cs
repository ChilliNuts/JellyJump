using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyPickup : MonoBehaviour {

	public float jumpCooldownTimer = 3f;
	public float jumpForceVariance = 15f;
	public float jumpForce = 50f; 
	bool canJump = true;

	JellyPlayer player;
	Rigidbody2D body;



	void Awake(){
		player = FindObjectOfType<JellyPlayer>();
		body = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (canJump){
			StartCoroutine(JumpCooldown());
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "PlayerBits" && player.canScale){
			player.canScale = false;
			player.PickupJelly(player.scaleIncrement);
			Destroy(gameObject);
		}
	}

	Vector2 CalculateJumpDirection(){
		Vector2 playerPos = player.gameObject.transform.position;
		Vector2 pos = transform.position;
		float disToPlayer = Vector2.Distance(playerPos, pos);
		float originOffset = (2f + Random.Range(-1f, 1f)) * disToPlayer;
		Vector2 originPos = new Vector2 (playerPos.x, playerPos.y - originOffset);
		Vector3 jumpDirection = pos - originPos;

		jumpDirection.Normalize();

		return jumpDirection;
	}
	float CalculateTorque(){
		
		Vector2 playerPos = player.gameObject.transform.position;
		Vector2 pos = transform.position;
		float disToPlayer = Vector2.Distance(playerPos, pos);
		float originOffset = 2f * disToPlayer;
		Vector2 originPos = new Vector2 (playerPos.x, playerPos.y - originOffset);

		float torque = (pos.x - Physics2D.Linecast(originPos, pos).point.x) *-25f;

		return torque;
	}

	public void Jump(){
		Vector2 jumpDir = CalculateJumpDirection();
		float torque = CalculateTorque();
		jumpDir *= jumpForce + Random.Range(-jumpForceVariance, jumpForceVariance);
		body.AddForce(jumpDir);
		body.AddTorque(torque);
		object[] message = SetBroadcast(jumpDir, torque);
		BroadcastMessage("BitJumps", message);
		canJump = true;
	}
	IEnumerator JumpCooldown(){
		canJump = false;
		yield return new WaitForSeconds(jumpCooldownTimer + Random.Range(-1.5f, 1.5f));
		Jump();
	}
	object[] SetBroadcast(Vector2 jumpForce, float torque){
		object[] broadcast = new object[2];
		broadcast[0] = (Vector2)jumpForce;
		broadcast[1] = (float)torque;
		return broadcast;
	}
}
