using UnityEngine;
using System.Collections;

public class ObjectEnabler : MonoBehaviour {

	public GameObject obj;
	public GameObject egg;

	// Use this for initialization
	void Start () {
		EventManager.RegisterListener ("OnGameStarted", gameObject);
		EventManager.RegisterListener ("OnLevelComplete", gameObject);
	}

	void OnGameStarted() {
		obj.SetActive (true);
	}

	void OnLevelComplete() {
		egg.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
