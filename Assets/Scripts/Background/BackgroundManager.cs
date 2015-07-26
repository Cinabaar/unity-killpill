using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {
	
	public SplineFunction ShakeSpline;

	public bool Debug;
	public GameObject[] Background;
	public BackgroundShaking _backgroundShaking;
	public BackgroundParallax _backgroundParallax;
	public Animator Intro;
	public GameObject DerpPrefab;
	public GameObject DerpGroup;
	public GameObject ActorGroup;

    private Animation introAnimation;
	private bool triggerSet = false;
	private bool messageSent = false;
	//private BackgroundIntro _backgroundIntro;
	// Use this for initialization
	void Start ()
	{
	    introAnimation = Intro.GetComponent<Animation>();
		ShakeSpline = new SplineFunction ()
			.AddSpline (new LinearFunction (new Vector2 (0.0f, 0.0f), new Vector2 (1.0f, 0.0f)))
			.AddSpline (new LinearFunction (new Vector2 (1.0f, 0.0f), new Vector2 (1.25f, 0.5f)))
			.AddSpline (new LinearFunction (new Vector2 (1.25f, 0.5f), new Vector2 (3.5f, -0.2f)))
			.AddSpline (new LinearFunction (new Vector2 (3.5f, -0.2f), new Vector2 (3.75f, 0.3f)))
			.AddSpline (new LinearFunction (new Vector2 (3.75f, 0.3f), new Vector2 (5.0f, 0.0f)))
			.AddSpline (new LinearFunction (new Vector2 (5.0f, 0.0f), new Vector2 (5.25f, 0.5f)))
			.AddSpline (new LinearFunction (new Vector2 (5.25f, 0.5f), new Vector2 (7.5f, 0.0f)));
	}


	void EnableComponent<T>()
	{
		if (_backgroundShaking is T) 
		{
			_backgroundShaking.enabled = true;
			_backgroundParallax.enabled = false;
		}
		else if (_backgroundParallax is T) 
		{
			_backgroundParallax.enabled = true;
			_backgroundShaking.enabled = false;
		}
		else
		{
			_backgroundParallax.enabled = false;
			_backgroundShaking.enabled = false;
		}
	}


	// Update is called once per frame
	void Update () {
		if (Debug) {
			DebugUpdate ();
			return;
		}
		var time = Time.timeSinceLevelLoad;

		if (time < ShakeSpline.Start ()) 
		{
			EnableComponent<BackgroundManager>();
		}
		else if (time < ShakeSpline.End ()) 
		{
			_backgroundShaking.Background = Background;
			_backgroundShaking.ShakeSpline = ShakeSpline;
			EnableComponent<BackgroundShaking>();

		}
		else if ( time < ShakeSpline.End() + introAnimation.clip.length)
		{
			if(triggerSet == false)
			{
				for(int i=0;i<50;i++)
				{
					var instance = Instantiate(DerpPrefab, DerpGroup.transform.position, Quaternion.identity);
					var goInstance = (instance as GameObject);
					goInstance.transform.SetParent(DerpGroup.transform);
					Camera cam = Camera.main;
					float height = 2f * cam.orthographicSize;
					goInstance.transform.Translate((Random.value-0.5f) * new Vector3(0, height, 0.0f));
				}

			}
			Intro.SetTrigger ("PlayCutscene");
			triggerSet = true;
		}
		else
		{
			_backgroundParallax.Background = Background;
			EnableComponent<BackgroundParallax>();
			if(!messageSent)
			{
				EventManager.SendMessage("OnGameStarted");
				messageSent = true;
			}
		}

	}
	void DebugUpdate()
	{
		var time = Time.timeSinceLevelLoad;
		if ( time < introAnimation.clip.length)
		{
			if(triggerSet == false)
			{
				for(int i=0;i<50;i++)
				{	
					var instance = Instantiate(DerpPrefab, DerpGroup.transform.position, Quaternion.identity);
					var goInstance = (instance as GameObject);
					goInstance.transform.SetParent(DerpGroup.transform);
					Camera cam = Camera.main;
					float height = 2f * cam.orthographicSize;
					goInstance.transform.Translate((Random.value-0.5f) * new Vector3(0, height - 200, 0.0f));
				}
				
			}
			Intro.SetTrigger ("PlayCutscene");
			triggerSet = true;
		}
		else
		{
			_backgroundParallax.Background = Background;
			EnableComponent<BackgroundParallax>();
			if(!messageSent)
			{
				EventManager.SendMessage("OnGameStarted");
				messageSent = true;
			}
		}
	}
}
