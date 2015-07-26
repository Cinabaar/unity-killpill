using UnityEngine;
using System.Collections;

public class PillControler : MonoBehaviour {

	public float Speed;
	private GameObject Target;
	public SpriteRenderer spriteRenderer;

	private bool isDodged;
	private Vector3 toTarget;
	private string button;
	private bool isLastPill = false;

	public Collider2D collider1;
	public Collider2D collider2;

	public float Amp;

	// Use this for initialization
	void Start () {
		Target = GameObject.FindGameObjectWithTag ("Player");
		isDodged = false;
		EventManager.RegisterListener ("OnLevelComplete", gameObject);
	}

	void OnLevelComplete() {
		Destroy (gameObject);
	}

	void OnDodgeSuccess(string button) {
		if(this.button == button) {
			isDodged = true;
			toTarget = (Target.transform.position - transform.position).normalized;
			collider1.enabled = false;
			collider2.enabled = false;
		}
	}

	void SetLastPill() {
		isLastPill = true;
	}

	void OnDodgeFailure() {
		EventManager.SendMessage("OnDodgeFailure");
		Destroy (gameObject);
	}

	void SetTarget(GameObject newTarget) {
		Target = newTarget;
	}

	void SetSprite(DictionaryEntry entry) {
		button = (string)entry.Key;
		spriteRenderer.sprite = (Sprite)entry.Value;
	}

	void OnBecameInvisible() {
		if (isLastPill) {
			EventManager.SendMessage ("OnEventComplete");
			EventManager.SendMessage ("OnPillEventEnd");
		}
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if(!isDodged)
			toTarget = (Target.transform.position - transform.position).normalized;
		transform.Translate(
			Speed * Time.deltaTime * toTarget
		);
		transform.Translate (new Vector3 (0, Amp * Mathf.Sin(Time.time), 0));
	}
}
