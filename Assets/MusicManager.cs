using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioSource bgMusic;
	public AudioSource fxMusic;

	public void toggleBgMusic() {
		bgMusic.mute = !bgMusic.mute;
	}

	public void playFxMusic() {
		fxMusic.PlayOneShot(fxMusic.clip, 1);
	}

	public void toggleFxMusic() {
		fxMusic.mute = !fxMusic.mute;
	}
}
