using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : MonoBehaviour {

	public float targetFollowDistance;
	public float smoothTime = 3f;
	public float maxSpeed = 5f;
	Vector2 yVelocity;
	PlayerManager player;
	Transform target;
	public float maxHeightReached;


	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerManager>();
		target = player.camProxy.transform;
		maxHeightReached = target.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(target.position.y > maxHeightReached){
			maxHeightReached = target.position.y;
		}

		Rise();
	}

	//TODO: Create a maxHeightReached var and set Rise to target that.

	void Rise(){
		float distanceToMaxHeight = maxHeightReached - transform.position.y;
		Vector2 moveToTarget = new Vector2(transform.position.x, maxHeightReached - targetFollowDistance);
		if(distanceToMaxHeight >= targetFollowDistance){
			print("following");
			//float newPosition = Mathf.SmoothDamp(transform.position.y, target.position.y, ref yVelocity, smoothTime);
			//transform.position = new Vector3(transform.position.x, newPosition, transform.position.z);
			transform.position = Vector2.SmoothDamp(transform.position, moveToTarget, ref yVelocity, smoothTime, maxSpeed, Time.deltaTime);
		}
	}
}
