using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBlob : MonoBehaviour {

	public float m_MinBounceTime = 0.3f;
	public float m_MaxBounceTime = 1.0f;
	public float m_MinJumpForce = 10.0f;
	public float m_MaxJumpForce = 10.0f;

	public LayerMask m_GroundLayer;

	JellySprite m_JellySprite;
	float m_BounceTimer;

	JellyPlayer player;


	void Start () 
	{
		m_JellySprite = GetComponent<JellySprite>();
		m_BounceTimer = UnityEngine.Random.Range(m_MinBounceTime, m_MaxBounceTime);
		player = FindObjectOfType<JellyPlayer>();	}


	void Update () 
	{
		m_BounceTimer -= Time.deltaTime;

		// Randomly bounce around
		if(m_BounceTimer < 0.0f /*&& m_JellySprite.IsGrounded(m_GroundLayer, 2)*/)
		{
			Jump(false);
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
	public void Jump(bool firstJump){
		Vector2 jumpVector = Vector2.zero;
		if(firstJump){
			int ranDir = Random.Range(0,2);
			if(ranDir == 0){
				jumpVector = new Vector3(-1,1,0);
			}else jumpVector = new Vector3(1,1,0);

		}else jumpVector = CalculateJumpDirection();

		m_JellySprite.AddForce(jumpVector * UnityEngine.Random.Range(m_MinJumpForce, m_MaxJumpForce));
		m_BounceTimer = UnityEngine.Random.Range(m_MinBounceTime, m_MaxBounceTime);
	}

	void OnJellyCollisionEnter2D(JellySprite.JellyCollision2D collision){
		if(collision.Collision2D.gameObject.tag == "Player" && player.canScale){
			
			player.canScale = false;
			player.PickupJelly(1f);
			print("hit");
			Destroy(gameObject);
		}
	}
}