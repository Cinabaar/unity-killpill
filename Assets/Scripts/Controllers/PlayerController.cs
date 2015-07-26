using UnityEngine;
using XInputDotNetPure;

public class PlayerController : MonoBehaviour {

	public float Speed;
	private string[] dodgeButtons = {"Triangle", "Square", "Cross", "Circle"};
	private Animator animator;

	public float notReadyDodgeTime;
	private bool isInDodgeArea = false;
	private Collider2D dodgeObject;
	private bool isGameOver = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		EventManager.RegisterListener ("OnGameOver", gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "VibrationTrigger") {
			GamePad.SetVibration(PlayerIndex.One, 1.0f, 1.0f);
		}
		else if(other.tag == "DodgeObjectSuccess") {
			isInDodgeArea = true;
			dodgeObject = other;
		}
		else if(other.tag == "DodgeObjectFailure") {
			other.SendMessageUpwards("OnDodgeFailure");
		}
		else if(other.tag == "MashingArea") {
			other.SendMessage("InMashingArea");
		}
		else if(other.tag == "GameOver") {
			EventManager.SendMessage("OnGameOver");
		}
		else if(other.tag == "Egg") {
			Application.LoadLevel("LevelComplete");
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.tag == "VibrationTrigger") {
			GamePad.SetVibration(PlayerIndex.One, .0f, .0f);
		}
	}

	void OnGameOver() {
		isGameOver = true;
	}

	void FixedUpdate() {
		if(!isGameOver) {
			transform.Translate(
				new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * Speed,
				            Input.GetAxis("Vertical") * Time.deltaTime * Speed,
				            0
			    )
			);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!isGameOver) {
			if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Dodge")) {
				foreach (var button in dodgeButtons) {
					if(Input.GetButtonDown(button)) {
						animator.SetTrigger("Dodge");
						if(isInDodgeArea)
							dodgeObject.SendMessageUpwards("OnDodgeSuccess", button);
					}
				}
			}
		}
	}
}
