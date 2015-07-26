using UnityEngine;
using System.Collections;

public class BackgroundShaking : MonoBehaviour {

	public SplineFunction ShakeSpline;
	public GameObject[] Background;
	private Vector3[] _startPos;
	// Use this for initialization
	void Start () {
		_startPos = new Vector3[Background.Length];
		for (int i=0; i < Background.Length; i++) {
			_startPos [i] = Background [i].transform.position;
		}
	}
	
	// Update is called once per frame
	void Update () {
		var time = Time.timeSinceLevelLoad;
		for(int i=0; i < Background.Length; i++)
		{
			Background[i].transform.position = _startPos[i] + new Vector3(ShakeSpline.GetValue(time)*640, 0, 0);
		}

	}
}
