using UnityEngine;
using System.Collections;

public class WallCollision : MonoBehaviour
{

    private Collider2D collider2D;

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "MashingArea")
			collider2D.enabled = false;
	}

	// Use this for initialization
	void Start ()
	{
	    collider2D = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
