using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private bool facingRight;
	private float speed;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		facingRight = true;
		speed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		MovePlayer (speed);

		if (this.anim.GetCurrentAnimatorStateInfo (0).IsName("player-cast") && anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.75) {
			anim.SetInteger ("State", 0);
		} else if (this.anim.GetCurrentAnimatorStateInfo (0).IsName("player-reel") && anim.GetCurrentAnimatorStateInfo (0).normalizedTime > 0.75) {
			anim.SetInteger ("State", 0);
		}
	}

	// Sprite Transformer ==================================
	void MovePlayer(float playerSpeed) {
		if (facingRight && transform.position.x < 7.2) {
			transform.position += Vector3.right * speed * Time.deltaTime;
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		} else if (!facingRight && transform.position.x > -7.2) {
			transform.position += Vector3.left * speed * Time.deltaTime;
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}

	public void SailLeft() {
		facingRight = false;
		speed = 1.8f;
	}

	public void SailRight() {
		facingRight = true;
		speed = 1.8f;
	}

	public void Idle() {
		speed = 0f;
	}

	public bool GetFacingRight() {
		return facingRight;
	}

	// Animation Handler ==================================
	public void Reel() {
		anim.SetInteger ("State", 1);
	}

	public void Cast() {
		anim.SetInteger ("State", 2);
	}
}
