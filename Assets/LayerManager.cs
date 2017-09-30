using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerManager : MonoBehaviour {

	public GameObject quitLayer;
	public GameObject pauseLayer;
	public GameObject winLayer;
	public GameObject endScore;

	public void ShowPauseLayer(bool boolean) {
		Time.timeScale = (boolean) ? 0.0f : 1.0f;
		pauseLayer.SetActive(boolean);
	}

	public void ShowQuitLayer(bool boolean) {
		Time.timeScale = (boolean) ? 0.0f : 1.0f;
		quitLayer.SetActive(boolean);
	}

	public void ShowWinLayer(bool boolean, string score) {
		Time.timeScale = (boolean) ? 0.0f : 1.0f;
		winLayer.SetActive(boolean);
		endScore.GetComponent<Text> ().text = score;
	}

}
