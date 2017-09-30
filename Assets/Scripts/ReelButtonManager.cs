using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReelButtonManager : MonoBehaviour {

	public GameObject reelButton;
	public GameObject reelUpButton;

	public void ReelClicked() {
		reelButton.SetActive (false);
		reelUpButton.SetActive (true);
	}

	public void ReelUpClicked() {
		reelButton.SetActive (true);
		reelUpButton.SetActive (false);
	}
}
