using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : MonoBehaviour {

	public AudioSource source;
	public AudioClip c1;
	public AudioClip c2;
	public AudioClip c3;
	public AudioClip c4;
	public AudioClip c5;
	public AudioClip c6;
	public AudioClip c7;
	public AudioClip c8;
	public AudioClip c9;
	public AudioClip c10;
	public AudioClip c11;
	public AudioClip c12;
	public AudioClip c13;
	public AudioClip c14;
	public AudioClip c15;
	public AudioClip c16;
	public AudioClip c17;
	public AudioClip c18;
	public AudioClip c19;
	public AudioClip c20;

	private AudioClip[] clips;

	void Start() {
		clips = new AudioClip[]{
			c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12, c13, c14, c15, c16, c17, c18, c19, c20
		};
	}

	public void PlayCountSpeech(int number) {
		source.PlayOneShot(clips[number], 4);
	}
}
