using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioSource bgMusic;
	public AudioSource fxMusic;
	public AudioSource swooshMusic;
	private bool fadingOut = false;

	void Update() {
		if (fadingOut && bgMusic.volume > 0) {
			Debug.Log ("music");
			bgMusic.volume -= (float) (1.1 * Time.deltaTime);
		}
	}
		
	public void ToggleBgMusic() {
		bgMusic.mute = !bgMusic.mute;
	}

	public void PlayFxMusic() {
		fxMusic.PlayOneShot(fxMusic.clip, 1);
	}

	public void PlaySwooshMusic() {
		swooshMusic.PlayOneShot(swooshMusic.clip, 1);
	}

	public void ToggleFxMusic() {
		fxMusic.mute = !fxMusic.mute;
	}

	public void FadeOut() {
		fadingOut = true;
	}
}
