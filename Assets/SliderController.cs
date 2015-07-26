using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderController : MonoBehaviour {

	public GameObject sliderObject;
	public float Speed;
	public float clickPenalty;

	private Slider slider;
	
	// Use this for initialization
	void Start () {
		EventManager.RegisterListener ("OnSeqClickFailure", gameObject);
		EventManager.RegisterListener ("OnSequenceEventStart", gameObject);
		EventManager.RegisterListener ("OnSequenceEventEnd", gameObject);
		EventManager.RegisterListener ("OnLevelComplete", gameObject);
		slider = sliderObject.GetComponent<Slider> ();
	}

	void OnLevelComplete() {
		sliderObject.SetActive (false);
	}
	
	void OnSequenceEventStart() {
		sliderObject.SetActive (true);
		slider.normalizedValue = 1.0f;
	}
	
	void OnSequenceEventEnd() {
		sliderObject.SetActive (false);
	}
	
	void OnSeqClickFailure() {
		slider.normalizedValue -= clickPenalty;
	}
	
	// Update is called once per frame
	void Update () {
		if(sliderObject.activeSelf) {
			slider.normalizedValue -= Speed * Time.deltaTime;
			if (slider.normalizedValue <= 0.0f) {
				EventManager.SendMessage ("OnGameOver");
				Debug.Log("GameOver slider");
			}
		}
	}
}
