using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioSource bgMusic;
	public AudioSource fxMusic;
	public AudioSource swooshMusic;
	public AudioSource boatFx;
	public AudioSource reelFx;
	private bool fadingOut = false;

	void Start() {
		// bgMusic
		bool bg = SoundSettings.bgMusic;
		bgMusic.mute = !bg;
		boatFx.mute = !bg;

		bool fx = SoundSettings.fxMusic; 
		fxMusic.mute = !fx;
		swooshMusic.mute = !fx;
		reelFx.mute = !fx;
	}

	void Update() {
		if (fadingOut && bgMusic.volume > 0) {
			Debug.Log ("music");
			bgMusic.volume -= (float) (1.1 * Time.deltaTime);
			if (boatFx != null) {
				boatFx.volume -= (float) (1.1 * Time.deltaTime);
			}
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

	public void PlayReelMusic() {
		reelFx.PlayOneShot(reelFx.clip, 1);
	}

	public void ToggleFxMusic() {
		fxMusic.mute = !fxMusic.mute;
	}

	public void FadeOut() {
		fadingOut = true;
	}
}
