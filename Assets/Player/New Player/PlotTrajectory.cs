using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotTrajectory : MonoBehaviour {

	PlayerManager player;
	public Dot dotPrefab;
	public float iteration;
	public int steps;
	Dot[] allDots;
	List<Dot> visibleDots = new List<Dot>();
	int numVisibleDots = 0;
	public Vector2 testForce;
	public LayerMask ground;
	public Color startColor;
	public Color endColor;
	bool hitGround;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerManager>();
		allDots = new Dot[steps];
		for(int i = 0; i < steps; i++){
			allDots[i] = Instantiate(dotPrefab,new Vector2(0,-6666), Quaternion.identity, this.transform) as Dot;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		CreateTrajectory();
		//Plot(this.transform.position, player.currentPlayer.myJump.jumpForce);
	}

	Vector2[] Plot(Vector2 pos, Vector2 force){

		Vector2[] results = new Vector2[steps];

		float timeStep = Time.fixedDeltaTime/iteration;

		Vector2 gravityAccel = (Physics2D.gravity * player.currentPlayer.myJelly.CentralPoint.Body2D.gravityScale * timeStep * timeStep);
		float drag = 1f - player.currentPlayer.myJelly.m_Drag;
		Vector2 moveStep = (force/(player.currentPlayer.myJelly.m_Mass*5.5f)) * timeStep;

		for(int i = 0; i < steps; i++){
			moveStep += gravityAccel;
			moveStep *= drag;
			pos += moveStep;
			results[i] = pos;

			//allDots[i].transform.position = pos;
		}
		return results;
	}

	void CreateTrajectory(){
		bool hitGround = false;
		Vector2[] plotPoints = Plot(this.transform.position, player.currentPlayer.myJump.jumpForce);

		for(int i = 0; i < plotPoints.Length - 1; i++){


			float d = Vector2.Distance(plotPoints[i], plotPoints[i+1]);
			d *= 1.2f;
			RaycastHit2D hit = Physics2D.Raycast(plotPoints[i], plotPoints[i] - plotPoints[i+1], d, ground);
			if(hit.collider != null){
				hitGround = true;
				visibleDots.Add(allDots[i]);
				if(visibleDots.Count != numVisibleDots){
					GradientDots();
				}
//				foreach(Dot dot in allDots){
//					if(!visibleDots.Contains(dot)){
//						dot.transform.position = dot.origin;
//					}
//				}
			}

			if(hit.collider == null){

				allDots[i].transform.position = plotPoints[i];
				visibleDots.Add(allDots[i]);
			}

			if (hitGround) {
				allDots[i].transform.position = allDots[i].origin;
				visibleDots.Remove(allDots[i]);
			}

		}
		hitGround = false;
		visibleDots.Clear();
	}



	void GradientDots(){
		float lerpAmount;
		Color colorState = startColor;
		for(int i = 1; i < visibleDots.Count; i++){

			lerpAmount = (float)i/(float)visibleDots.Count;

			colorState = Color.Lerp(startColor, endColor, lerpAmount);
			visibleDots[i].myRend.color = colorState;
		}
		numVisibleDots = visibleDots.Count;

	}
}
