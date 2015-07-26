using UnityEngine;
using System.Collections;

public class IntroController : MonoBehaviour {

	public AudioSource audioSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioSource.isPlaying || Input.anyKeyDown)
						Application.LoadLevel ("Game");

	}
}
