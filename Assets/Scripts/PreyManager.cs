using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyManager : MonoBehaviour {

	public GameObject player;
	private float ySpeed;
	private float timeDelay;
	private bool isReeling;
	private bool isFirstTriggered;
	private Vector2 pointStart;
	private Vector2 pointEnd;

	// Use this for initialization
	void Start () {
		ySpeed = 0f;
		timeDelay = 1.0f;
		GetComponent<Renderer> ().enabled = false;
		isReeling = false;
		isFirstTriggered = true;
//		pointStart = Vector2 (0, 0);
//		pointEnd = Vector2 (0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		// wait for animation "player-cast"
		if (IsDelayed() || !isReeling)
			return;

		// set active
		GetComponent<Renderer> ().enabled = true;

		// start moving
		MovePrey ();
	}

	private void MovePrey() {
		MoveX ();
		MoveY ();
	}

	private void MoveX() {
		// travel-x
		Vector3 target = transform.position;
		target.x = player.transform.position.x;
		target.x = GetXDistFromPlayer (target.x);

		if (isFirstTriggered) {
			transform.position = target;
			isFirstTriggered = false;
		}

		float xDistance = target.x - transform.position.x;
		float xSpeed = Mathf.Abs (xDistance) * 0.7f;
		if (xDistance > 0.2)
			transform.position += Vector3.right * xSpeed * Time.deltaTime;
		else if (xDistance < -0.8)
			transform.position += Vector3.left * xSpeed * Time.deltaTime;

		pointStart.x = target.x;
		pointEnd.x = transform.position.x;
	}

	private void MoveY() {
		// travel-y
		if (transform.position.y > -4.5) {
			transform.position += Vector3.down * ySpeed * Time.deltaTime;
		}
		pointStart.y = player.transform.position.y + 1;
		pointEnd.y = transform.position.y + 0.5f;
	}

	public void Reel() {
		timeDelay = 1.0f;
		ySpeed = 1.0f;
		isReeling = true;
	}

	public void StopReel() {
		ySpeed = 0f;
	}

	private float GetXDistFromPlayer(float x) {
		PlayerManager playerManager = player.GetComponent<PlayerManager> ();
		bool facingRight = playerManager.GetFacingRight ();
		return (float) (facingRight ? (x + 1.5f) : (x - 1.8f));
	}

	private bool IsDelayed() {
		timeDelay -= Time.deltaTime;
		if (timeDelay > 0)
			return true;
		return false;
	}

	public Vector2[] GetPoints() {
		Vector2[] points = { pointStart, pointEnd };
		return points;
	}
}
