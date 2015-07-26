using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SequenceController : MonoBehaviour {

	private string[] buttons = {"Square", "Triangle", "Circle", "Cross"};
	private List<string> buttonSequence = new List<string>();
	private List<SpriteRenderer> renderers = new List<SpriteRenderer> ();
	public Transform[] buttonSpots;
	public Transform playerSpot;
	public float Speed;
	private int buttonsCount = 0;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		EventManager.RegisterListener ("OnLevelComplete", gameObject);
	}

	void OnLevelComplete() {
		buttonSequence.Clear ();
	}

	void AddSprite(DictionaryEntry entry) {
		if (buttonsCount <= buttonSpots.Length) {
			buttonSequence.Add ((string)entry.Key);
			var spriteRenderer = buttonSpots[buttonsCount++].GetComponent<SpriteRenderer>();
			spriteRenderer.sprite = (Sprite)entry.Value;
			renderers.Add(spriteRenderer);
		}
	}

	void OnBecameInvisible() {
		EventManager.SendMessage ("OnEventComplete");
		Destroy (gameObject);
	}

	void OnBecameVisible() {
		EventManager.SendMessage ("OnSequenceEventStart");
	}
	
	// Update is called once per frame
	void Update () {
		if(buttonSequence.Count > 0) {
			string nextButton = buttonSequence [0];
			if (Input.GetButtonDown (nextButton)) {
				renderers[0].enabled = false;
				renderers.RemoveAt(0);
				buttonSequence.RemoveAt(0);
			}
			else {
				foreach(var button in buttons)
					if(Input.GetButtonDown(button) && button != nextButton)
						EventManager.SendMessage("OnSeqClickFailure");
			}
			Vector3 toPlayer = (player.transform.position - playerSpot.position).normalized;
			transform.Translate(Speed * Time.deltaTime * toPlayer);
		}
		else {
			EventManager.SendMessage ("OnSequenceEventEnd");
			transform.Translate(Speed * Time.deltaTime * (-transform.right));
		}
	}
}
