using UnityEngine;
using System.Collections;

public class WallController : MonoBehaviour {

	private BackgroundParallax background;
	public float Speed;
	public SpriteRenderer spriteRenderer;
	private string button;
	private bool isInMashingArea;
	private int mashCount;
	private Animator animator;
	public Collider2D wallCollider0;
	public Collider2D wallCollider1;
	public Collider2D wallCollider2;
	public float slowdownX;
	private bool messageSent = false;
	// Use this for initialization
	void Start () {
		background = GameObject.FindGameObjectWithTag ("Background").GetComponent<BackgroundParallax> ();
		EventManager.RegisterListener ("OnLevelComplete", gameObject);
		animator = GetComponent<Animator> ();
		mashCount = 0;
	}

	void OnLevelComplete() {
		Destroy (gameObject);
	}

	void SetSprite(DictionaryEntry entry) {
		button = (string)entry.Key;
		spriteRenderer.sprite = (Sprite)entry.Value;
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}

	void InMashingArea() {
		isInMashingArea = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate (new Vector3(Time.fixedDeltaTime * (-background.Speed), 0, 0));
		if (Camera.main.WorldToScreenPoint (transform.position).x < slowdownX && !messageSent) {
			messageSent = true;
			background.SendMessage ("OnSlowDown", Speed);
		}
	}

	void Update() {
		if (isInMashingArea) {
			if(Input.GetButtonDown(button)) {
				++mashCount;
				animator.SetInteger("ClickCount", mashCount);
			}
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Wall_1")) {
				wallCollider0.enabled = false;
				wallCollider1.enabled = true;
			}
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Wall_2")) {
				wallCollider1.enabled = false;
				wallCollider2.enabled = true;
			}
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Wall_3") && wallCollider2.enabled) {
				EventManager.SendMessage("OnEventComplete");
				wallCollider2.enabled = false;
			}
		}
	}
}
