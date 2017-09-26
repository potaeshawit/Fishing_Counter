using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettings : MonoBehaviour {

	static public bool bgMusic = true;
	static public bool fxMusic = true;

	public void toggleBgMusic() {
		bgMusic = !bgMusic;
	}

	public void toggleFxMusic() {
		fxMusic = !fxMusic;
	}
}
