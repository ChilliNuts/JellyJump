using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class JellyPlayer : MonoBehaviour {

	[HideInInspector] public JellySprite myJelly;
	[HideInInspector] public Vector2 homePos;
	[HideInInspector] public PlayerJump myJump;
	public CinemachineVirtualCamera myVCam;

	PlayerManager playerManager;

	void Awake(){
		homePos = transform.position;
		myJelly = GetComponent<JellySprite>();
		myJump = GetComponent<PlayerJump>();
		playerManager = FindObjectOfType<PlayerManager>();
	}


	public void SetInactive(){
		StartCoroutine(SI());
	}
	IEnumerator SI(){
		yield return new WaitForEndOfFrame();
		gameObject.SetActive(false);
		playerManager.Activate(this);

	}











//	public float minScale = 1f;
//	public float maxScale = 10f;
//	public float scaleIncrement = 1f;
//	public float cameraZoomOnScale = 5f;
//	public float pickupCooldown = 0.5f;
//	[HideInInspector]public bool canScale = true;
//
//	public JellyBlob jellyPrefab;
//
//	CameraZoomOnPlayer cam;
//	PlayerJump playerJump;
//	JellySprite jellySprite;
//
//	void Awake () {
//		cam = FindObjectOfType<CameraZoomOnPlayer>();
//		playerJump = GetComponent<PlayerJump>();
//		jellySprite = GetComponent<JellySprite>();
//	}
//
//
//	public void PickupJelly(float addScale){
//		StartCoroutine("ScaleCooldown", pickupCooldown);
//		if (jellySprite.m_SpriteScale.x < maxScale) {
//
//			Vector2 scale = jellySprite.m_SpriteScale;
//			float scaleRatio = 1f+ (1f/jellySprite.m_SpriteScale.x);
//			jellySprite.m_SpriteScale *= scaleRatio;
//			jellySprite.Scale(scaleRatio, true);
//			jellySprite.m_Mass *= scaleRatio;
//			playerJump.forceMultiplier += 5f;
//		
//			//cam.ZoomCamera(cameraZoomOnScale);
//			//playerJump.MaxForce += cameraZoomOnScale;
//		}
//	}
//	public void LoseJelly(float minusScale){
//		StartCoroutine("ScaleCooldown", pickupCooldown);
//		if (jellySprite.m_SpriteScale.x > minScale) {
//			float scaleRatio = 1f- (1f/jellySprite.m_SpriteScale.x);
//			jellySprite.m_SpriteScale *= scaleRatio;
//			jellySprite.Scale(scaleRatio, true);
//			jellySprite.m_Mass *= scaleRatio;
//			playerJump.forceMultiplier -= 5f;
//			//cam.ZoomCamera(-cameraZoomOnScale);
//			//playerJump.MaxForce -= cameraZoomOnScale;
//			StartCoroutine( EjectJelly());
//		}
//
//	}
//
//	IEnumerator EjectJelly(){
//		Vector2 ejectPos = new Vector2(transform.position.x, transform.position.y + (transform.localScale.y / 2));
//
//		JellyBlob jelly = Instantiate(jellyPrefab, ejectPos, Quaternion.identity) as JellyBlob;
//
//		yield return new WaitForEndOfFrame();
//		jelly.Jump(true);
//	}
//
//	IEnumerator ScaleCooldown(float waitTime){
//		
//		yield return new WaitForSeconds(waitTime);
//		canScale = true;
//	}

}
