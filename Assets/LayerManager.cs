using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour {

	public GameObject quitLayer;
	public GameObject pauseLayer;
	public GameObject winLayer;

	public void ShowPauseLayer(bool boolean) {
		Time.timeScale = (boolean) ? 0.0f : 1.0f;
		pauseLayer.SetActive(boolean);
	}

	public void ShowQuitLayer(bool boolean) {
		Time.timeScale = (boolean) ? 0.0f : 1.0f;
		quitLayer.SetActive(boolean);
	}

	public void ShowWinLayer(bool boolean) {
		Time.timeScale = (boolean) ? 0.0f : 1.0f;
		winLayer.SetActive(boolean);
	}

}
