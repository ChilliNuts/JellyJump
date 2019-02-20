using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlob : MonoBehaviour {

	BoxCollider2D myArea;
	public JellyBlob blobPrefab;
	PlayerManager PM;

	public float spawnEvery = 10f;
	public float t;
	public bool containsPlayer;

	// Use this for initialization
	void Start () {
		myArea = GetComponent<BoxCollider2D>();
		PM = FindObjectOfType<PlayerManager>();
		t = spawnEvery;
	}

	void Update(){
		if (containsPlayer) {
			if (t <= 0) {
				if(transform.childCount >= 1){
					SpawnABlobAtFurthestSpawnPoint ();
				}else SpawnABlobAnywhere();

				t = spawnEvery;
			} else
				t -= Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			containsPlayer = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			containsPlayer = false;
		}
	}
		
	Vector2 GetRandomPosInArea(){
		Vector2 max = myArea.bounds.max;
		Vector2 min = myArea.bounds.min;
		Vector2 RandomPos;

		RandomPos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
		return RandomPos;
	}

	void SpawnABlobAnywhere(){

		Vector2 spawnPos = GetRandomPosInArea();

		JellyBlob blob = Instantiate(blobPrefab, spawnPos, Quaternion.identity) as JellyBlob;

	}

	void SpawnABlobAtFurthestSpawnPoint(){
		Vector2 playerPos = PM.currentPlayer.transform.position;
		Vector2 spawnPoint = transform.GetChild(0).transform.position;

		foreach(Transform child in this.transform){
			if(Vector2.Distance(playerPos, spawnPoint) < Vector2.Distance(playerPos, child.transform.position)){
				spawnPoint = child.transform.position;
			}
		}
		JellyBlob blob = Instantiate(blobPrefab, spawnPoint, Quaternion.identity) as JellyBlob;
	}
}
