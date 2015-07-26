using UnityEngine;
using System.Collections;

public class DerpController : MonoBehaviour {


	private Vector2 offset;
	private Vector2 times;
	private float allTime;
	private float camHeight;
	// Use this for initialization
	void Start () {
		offset.x = Random.value * Mathf.PI * 2;
		offset.y = Random.value * Mathf.PI * 2;
		times.x = Random.value * 6 + 8;
		times.y = Random.value * 10 + 6;
		allTime = 0;
		Camera cam = Camera.main;
		camHeight = cam.orthographicSize;
	}

	// Update is called once per frame

	float PosY(float time)
	{
		var posY = (camHeight/20.0f) * Mathf.Sin (2 * Mathf.PI / times.y * time + offset.y);
		return posY;
	}

	float PosX(float time)
	{
		var height = 50.0f;
		var posX = height * Mathf.Sin (2 * Mathf.PI / times.x * time + offset.x);
		return posX;
	}

	void Update () {
		transform.Translate(new Vector3 (PosX(allTime+Time.deltaTime) - PosX(allTime), 
		                                   PosY(allTime+Time.deltaTime) - PosY(allTime), 0.0f));	
		
		allTime += Time.deltaTime;


	}

}
