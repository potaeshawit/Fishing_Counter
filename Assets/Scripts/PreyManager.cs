using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyManager : MonoBehaviour {

	public GameObject player;
	public GameObject fish;
	public GameObject mainCamera;
	private float ySpeed;
	private float timeDelay;
	private bool isReeling;
	private bool isReelingUp;
	private bool isFirstTriggered;
	private Vector2 pointStart;
	private Vector2 pointEnd;
	private Vector3 target;
	private bool fishAttached;
	private LineRenderer lr;

	// Use this for initialization
	void Start () {
		ySpeed = 0f;
		timeDelay = 1.0f;
		GetComponent<Renderer> ().enabled = false;
		GetComponent<LineRenderer> ().enabled = false;
		isReeling = false;
		isFirstTriggered = true;
		isReelingUp = false;
		target = new Vector3 ();
		fishAttached = false;
	}
	
	// Update is called once per frame
	void Update () {
		// wait for animation "player-cast"
		if (IsDelayed () || !isReeling) {
			return;
		}
			

		// set active
		GetComponent<Renderer> ().enabled = true;
		GetComponent<LineRenderer> ().enabled = true;
		GetComponent<LineRenderer> ().SetColors (Color.black, Color.black);


		target = transform.position;
		target.x = player.transform.position.x;
		target.x = GetXDistFromPlayer (target.x);

		// start moving
		MovePrey ();

		if (!fishAttached)
			CheckCollideFish ();

		DrawLine ();
	}

	private void DrawLine() {
		Vector3 target = player.transform.position;

		target.x += (float)(player.GetComponent<PlayerManager>().GetFacingRight() ? 1.2f : -1.2f);
		target.y -= 0.2f;
		GetComponent<LineRenderer> ().SetWidth(0.01f, 0.01f);
		GetComponent<LineRenderer> ().SetPosition(0, transform.position);
		GetComponent<LineRenderer> ().SetPosition(1, target);
	}

	private void MovePrey() {
		MoveX ();
		MoveY ();

		transform.position = GetPositionAtLayer (-8.0f);
	}

	private void MoveX() {
		// travel-x 
		if (isFirstTriggered) {
			transform.position = target;
			isFirstTriggered = false;
		}

		float xDistance = target.x - transform.position.x;
		float xSpeed = Mathf.Abs (xDistance) * 0.5f;
		if (xDistance > 0.2)
			transform.position += Vector3.right * xSpeed * Time.deltaTime;
		else if (xDistance < -0.8)
			transform.position += Vector3.left * xSpeed * Time.deltaTime;

		pointStart.x = target.x;
		pointEnd.x = transform.position.x;
	}

	private void MoveY() {
		// travel-y
		if (isReelingUp) {
			float yDistance = player.transform.position.y - transform.position.y;
			float ySpeed = Mathf.Abs (yDistance) * 5f;
			transform.position += Vector3.up * ySpeed * Time.deltaTime;
			if (transform.position.y > 2.8) {
				// finishing up fish[i]
				if (fishAttached) {
					fish.GetComponent<FishManager> ().Reeled ();
				}
				Start ();
			}
		} else if (transform.position.y > -4.5) {
			transform.position += Vector3.down * ySpeed * 2f * Time.deltaTime;
		}

		pointStart.y = player.transform.position.y + 1;
		pointEnd.y = transform.position.y + 0.5f;
	}

	public void Reel() {
		timeDelay = 1.0f;
		ySpeed = 1.0f;
		isReeling = true;
	}

	public void ReelUp() { 
		isReelingUp = true; 
		player.GetComponent<PlayerManager> ().Reel ();
		mainCamera.GetComponent<MusicManager> ().PlayReelMusic ();
	}
	public void StopReel() { ySpeed = 0f; }

	private float GetXDistFromPlayer(float x) {
		PlayerManager playerManager = player.GetComponent<PlayerManager> ();
		bool facingRight = playerManager.GetFacingRight ();
		return (float) (facingRight ? (x + 1.2f) : (x - 1.2f));
	}

	private bool IsDelayed() {
		timeDelay -= Time.deltaTime;
		return (timeDelay > 0);
	}

	public Vector2[] GetPoints() {
		Vector2[] points = { pointStart, pointEnd };
		return points;
	}

	private void CheckCollideFish() {
		float r = 1.0f;
		float x = transform.position.x;
		float y = transform.position.y;
		float fX = fish.transform.position.x;
		float fY = fish.transform.position.y;
		if ((x > fX - r && x < fX + r) && (y > fY - r && y < fY + r)) {
			fish.GetComponent<FishManager> ().SetGotHooked(true);
			fishAttached = true;
		}
	}

	private Vector3 GetPositionAtLayer(float z) {
		return (new Vector3 (transform.position.x, transform.position.y, z));
	}
}
