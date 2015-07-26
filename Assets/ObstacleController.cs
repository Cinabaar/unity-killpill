using UnityEngine;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnBecameInvisible() {
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
