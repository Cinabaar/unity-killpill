using UnityEngine;
using System.Collections;

public class PillPanelControler : MonoBehaviour {

	public GameObject[] pills;
	public GameObject panel;
	private int pillsCount;

	// Use this for initialization
	void Start () {
		EventManager.RegisterListener ("OnDodgeFailure", gameObject);
		EventManager.RegisterListener ("OnPillEventStart", gameObject);
		EventManager.RegisterListener ("OnPillEventEnd", gameObject);
		EventManager.RegisterListener ("OnLevelComplete", gameObject);
		foreach(var pill in pills)
			pill.SetActive(true);
		pillsCount = pills.Length - 1;
	}

	void OnLevelComplete() {
		panel.SetActive (false);
	}

	void OnPillEventStart() {
		panel.SetActive (true);
		foreach (var pill in pills)
			pill.SetActive (true);
		pillsCount = pills.Length - 1;
	}

	void OnPillEventEnd() {
		panel.SetActive (false);
	}

	void OnDodgeFailure() {
		pills [pillsCount].SetActive (false);
		--pillsCount;
		if (pillsCount == -1) {
						EventManager.SendMessage ("OnGameOver");
			Debug.Log("GameOver");
				}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
