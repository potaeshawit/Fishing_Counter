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

		facingRight = false;

		speed = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		MovePlayer (speed);
		Idle ();
	}

	void MovePlayer(float playerSpeed) {
		if (facingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
	}

	public void SailLeft() {
		speed = 1;
	}

	public void SailRight() {
		speed = -1;
	}

	private void Idle() {
		speed = 0f;
	}
}
