using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingNetManager : MonoBehaviour {
	public GameObject prey;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DrawLine ();
	}

	void DrawLine()
	{
		LineRenderer lr = GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(Color.black, Color.black);
		lr.SetWidth(0.4f, 0.4f);

		PreyManager preyManager = prey.GetComponent<PreyManager> ();
		Vector2[] points = preyManager.GetPoints ();
		lr.SetPosition(0, points[0]);
		lr.SetPosition(1, points[1]);
//		GameObject.Destroy(myLine, duration);
	}
}
