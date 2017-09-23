using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioSource bgMusic;
	public AudioSource fxMusic;
	private bool fadingOut = false;

	void Update() {
		if (fadingOut && bgMusic.volume > 0) {
			Debug.Log ("music");
			bgMusic.volume -= (float) (1.1 * Time.deltaTime);
		}
	}
		
	public void toggleBgMusic() {
		bgMusic.mute = !bgMusic.mute;
	}

	public void playFxMusic() {
		fxMusic.PlayOneShot(fxMusic.clip, 1);
	}

	public void toggleFxMusic() {
		fxMusic.mute = !fxMusic.mute;
	}

	public void FadeOut() {
		fadingOut = true;
	}
}
