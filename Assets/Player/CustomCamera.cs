using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CustomCamera : MonoBehaviour {

	CinemachineVirtualCamera cam;
	public float duration = 1f;
	float lastTargetSize;


	void Awake () {
		cam = GetComponent<CinemachineVirtualCamera>();
		lastTargetSize = cam.m_Lens.OrthographicSize;
	}
	public void ZoomCamera(float amount){
		StopCoroutine("Zoom");
		StartCoroutine("Zoom",(amount));
	}
	IEnumerator Zoom(float amount){
		float start = cam.m_Lens.OrthographicSize;
		float end = lastTargetSize + amount;
		lastTargetSize = end;
		float t = 0f;
		print("start Zooming");
		while(cam.m_Lens.OrthographicSize != end){
			
			cam.m_Lens.OrthographicSize = Mathf.SmoothStep(start, end, t);
			t += Time.deltaTime / duration;

			yield return null;
		}
		print("finished Zooming");
	}

	public void SetFollowTarget(Transform newTarget){
		StartCoroutine("SetTarget", newTarget);
	}

	IEnumerator SetTarget(Transform newTarget){
		cam.Follow = null;
		yield return new WaitForFixedUpdate();
		cam.Follow = newTarget;
	}
}
