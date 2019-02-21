using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {


	public JellyPlayer[] sizes;
	int currentSize;
	public JellyPlayer currentPlayer;
	public int startingSize;
	public Transform startingPos;
	public JellyBlob blobPrefab;

	public bool canScale = true;

	PlotTrajectory trajectory;

	// Use this for initialization
	void Start () {
		trajectory = FindObjectOfType<PlotTrajectory>();
		InitPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.I)){
			Scale(1);
		}
		if(Input.GetKeyDown(KeyCode.K)){
			Scale(-1);
		}
	}

	void InitPlayer(){
		currentSize = startingSize;
		currentPlayer = sizes[startingSize];
		currentPlayer.myJelly.SetPosition(startingPos.position, true);
		currentPlayer.myVCam.Priority = 1;
	}

	public void Scale(int scaleBy){
		if(currentSize + scaleBy >= 0 && currentSize + scaleBy <= sizes.Length-1 && canScale){
			canScale = false;
			JellyPlayer scaleTo = sizes[currentSize + scaleBy];
			scaleTo.myJelly.SetPosition(currentPlayer.transform.position, true);
			scaleTo.myJelly.CentralPoint.transform.rotation = currentPlayer.myJelly.CentralPoint.transform.rotation;
			scaleTo.myVCam.Priority = 1;
			currentPlayer.myVCam.Priority = 0;
			for (int i = 0; i < scaleTo.myJelly.ReferencePoints.Count; i++){
				scaleTo.myJelly.ReferencePoints[i].transform.position = currentPlayer.myJelly.ReferencePoints[i].transform.position;
				scaleTo.myJelly.ReferencePoints[i].Body2D.velocity = currentPlayer.myJelly.ReferencePoints[i].Body2D.velocity;
			}
			currentPlayer.SetInactive();
			currentPlayer = scaleTo;
			if(currentSize + scaleBy < currentSize){
				StartCoroutine(EjectBlob());
			}
				
			currentSize += scaleBy;
			trajectory.ScaleDots(scaleBy ,currentSize);
		}else print("?");
	}
		
	public void Activate(JellyPlayer j){
		StartCoroutine("Act",j);
	}
	IEnumerator Act(JellyPlayer j){
		yield return new WaitForSeconds(1f);
		j.myJelly.SetPosition(j.homePos, true);
		yield return new WaitForEndOfFrame();
		j.gameObject.SetActive(true);
		canScale = true;

	}

	IEnumerator EjectBlob(){
		yield return new WaitForEndOfFrame();
		Vector2 ejectPos = new Vector2(currentPlayer.transform.position.x, currentPlayer.transform.position.y + (currentPlayer.myJelly.m_SpriteScale.y));
		
				JellyBlob blob = Instantiate(blobPrefab, ejectPos, Quaternion.identity) as JellyBlob;
		
				yield return new WaitForEndOfFrame();
				blob.Jump(true);
			}
}
