using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class GameOverScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GamePad.SetVibration (PlayerIndex.One, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown)
						Application.LoadLevel ("MainMenu");
	}
}
