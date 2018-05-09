using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScaling : MonoBehaviour {

	JellySprite j;

	public float scaleBy = 1f;


	// Use this for initialization
	void Start () {
		j = GetComponent<JellySprite>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.O)){
			Vector2 scale = j.m_SpriteScale;
			float scaleRatio = 1f+ (1f/j.m_SpriteScale.x);
			j.m_SpriteScale *= scaleRatio;
			j.Scale(scaleRatio, true);
			//j.UpdateJoints();
		}
		if(Input.GetKeyDown(KeyCode.L)){
			Vector2 scale = j.m_SpriteScale;
			float scaleRatio = 1f- (1f/j.m_SpriteScale.x);
			j.m_SpriteScale *= scaleRatio;
			j.Scale(scaleRatio, true);
			//j.UpdateJoints();
		}
		if(Input.GetKeyDown(KeyCode.I)){
			
			j.Scale(10f, true);
			//j.UpdateJoints();
		}
		if(Input.GetKeyDown(KeyCode.K)){

			j.Scale(0.2f, true);
			//j.UpdateJoints();
		}
	}
}
