using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReelButtonManager : MonoBehaviour {

	public GameObject reelButton;
	public GameObject reelUpButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ReelClicked() {
		reelButton.SetActive (false);
		reelUpButton.SetActive (true);
	}

	public void ReelUpClicked() {
		reelButton.SetActive (true);
		reelUpButton.SetActive (false);
	}
}
