using System.Collections;
using UnityEngine;
using Random = System.Random;
public class SpawnerController : MonoBehaviour {

	private Random rand = new Random();
	public string[] buttons;
	public Sprite[] buttonSprites;

	public bool isCompleted = true;
	private bool isWallEvent = false;

	public GameObject pillPrefab;
	public Transform pillStart;
	public int maxPills;
	public float pillsXDist;
	public float pillsYClamp;

	public GameObject wallPrefab;
	public Transform wallStart;

	public GameObject[] sequencePrefabs;
	public Transform sequenceStart;
	public int sequenceMaxButtons;

	private bool isGameOver = false;
	public float GameOverTimer;

	public float LevelEndTime;
	public int eventsMinCount;

	public enum EventT {
		PillEvent, WallEvent, SequenceEvent, EventTSize
	}

	// Use this for initialization
	void Start () {
		EventManager.RegisterListener ("OnEventComplete", gameObject);
		EventManager.RegisterListener ("OnObstaclePassed", gameObject);
		EventManager.RegisterListener ("OnGameOver", gameObject);
		gameObject.SetActive (false);
	}

	void OnObstaclePassed(GameObject obstacle) {
		if(rand.Next(0,2) == 1 && !isWallEvent)
			obstacle.SetActive(true);
	}

	void OnGameOver() {
		isGameOver = true;
	}

	void OnEventComplete() {
		isCompleted = true;
		isWallEvent = false;
	}

	void NewPillEvent() {
		int numberOfPills = rand.Next (3, maxPills);
		float currentX = pillStart.position.x;
		GameObject newPill = null;
		for(int i = 0; i < numberOfPills; ++i) {
			float currentY = (Screen.currentResolution.height - 2*pillsYClamp) * (float)(rand.NextDouble() - 0.5);
			newPill = Instantiate(pillPrefab, new Vector3(currentX, currentY, 0), Quaternion.identity) as GameObject;
			currentX += pillsXDist;
			int buttonIndex = rand.Next(0, buttons.Length);
			newPill.SendMessage("SetSprite", new DictionaryEntry(buttons[buttonIndex], buttonSprites[buttonIndex]));
		}
		newPill.SendMessage ("SetLastPill");
		EventManager.SendMessage ("OnPillEventStart");
		isCompleted = false;
	}

	void NewWallEvent() {
		GameObject newWall = Instantiate (wallPrefab, wallStart.position, Quaternion.identity) as GameObject;
		int buttonIndex = rand.Next(0, buttons.Length);
		newWall.SendMessage ("SetSprite", new DictionaryEntry(buttons[buttonIndex], buttonSprites[buttonIndex]));
		isWallEvent = true;
		isCompleted = false;
	}

	void NewSequenceEvent() {
		GameObject prefab = sequencePrefabs [rand.Next(0, sequencePrefabs.Length)];
		GameObject newSequence = Instantiate (prefab, sequenceStart.position, prefab.transform.rotation) as GameObject;
		int numOfButtons = rand.Next (5, sequenceMaxButtons);
		for (int i = 0; i < numOfButtons; ++i) {
			int buttonIndex = rand.Next(0, buttons.Length);
			newSequence.SendMessage ("AddSprite", new DictionaryEntry(buttons[buttonIndex], buttonSprites[buttonIndex]));
		}
		isCompleted = false;
	}
	
	// Update is called once per frame
	void Update () {
		LevelEndTime -= Time.deltaTime;
		Debug.Log (LevelEndTime);
		if(isCompleted) {
			--eventsMinCount;
			EventT newEvent = (EventT)rand.Next (0, (int)EventT.EventTSize);
			switch (newEvent) {
			case EventT.PillEvent:
				NewPillEvent();
				break;
			case EventT.WallEvent:
				NewWallEvent();
				break;
			case EventT.SequenceEvent:
				NewSequenceEvent();
				break;
			default:
				break;
			}
		}
		if (isGameOver) {
			GameOverTimer -= Time.deltaTime;
			if(GameOverTimer <= 0.0f)
				Application.LoadLevel("GameOver");
		}
		if (LevelEndTime <= 0.0f && eventsMinCount <= 0) {
			EventManager.SendMessage("OnLevelComplete");
			gameObject.SetActive(false);
		}
	}
}
