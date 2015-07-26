using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour {

	public GameObject[] Background;
	private float startSpeed;
	public float Speed;
	public float slowdownTime;
	private float slowdownTimer;

	// Use this for initialization
	void Start () {
		startSpeed = Speed;
	}

	void OnSlowDown(float speed) {
		Speed = speed;
		slowdownTimer = slowdownTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var deltaTime = Time.deltaTime;
		foreach (var b in Background) {
				b.transform.Translate (new Vector3 (-Speed * deltaTime, 0, 0));
				if (b.transform.position.x < -640.0f) {
					b.transform.Translate (new Vector3 (2560, 0, 0));
					if(b.tag == "Obstacle")
						EventManager.SendMessage("OnObstaclePassed", b);
				}
		}
		if (slowdownTimer > 0.0f) {
			slowdownTimer -= Time.deltaTime;
			if(slowdownTimer <= 0.0f)
				Speed = startSpeed;
		}
	}
}
