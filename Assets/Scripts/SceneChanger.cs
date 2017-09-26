using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

	public void LoadScene (string sceneName) {
		Time.timeScale = 1.0f;
		StartCoroutine (ChangeLevel (sceneName));
	}

	IEnumerator ChangeLevel(string sceneName) {
		float fadeTime = GameObject.Find ("SystemScript").GetComponent<Fader>().BeginFade (1);
		yield return new WaitForSeconds (fadeTime);
		SceneManager.LoadScene (sceneName);
	}
}
