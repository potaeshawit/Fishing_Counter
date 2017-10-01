using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyManager : MonoBehaviour {

	public GameObject gameControllerObj;
	private GameController gameController;

	private Vector2 pointStart;
	private Vector2 pointEnd;
	private Vector3 target;
	private LineRenderer lr;
	private Renderer renderer;

	private float ySpeed;
	private float timeDelay;
	private bool isReeling;
	private bool isReelingUp;
	private bool isFirstTriggered;
	private bool fishAttached;

	// Use this for initialization
	void Start () {
		gameController = gameControllerObj.GetComponent<GameController> ();
		renderer = GetComponent<Renderer> ();
		lr = GetComponent<LineRenderer> ();

		ySpeed = 0f;
		timeDelay = 1.0f;
		renderer.enabled = false;
		lr.enabled = false;
		isReeling = false;
		isReelingUp = false;
		fishAttached = false;

		isFirstTriggered = true;
		target = new Vector3 ();
	}
	
	// Update is called once per frame
	void Update () {
		// wait for animation "player-cast"
		if (IsDelayed () || !isReeling) {
			return;
		}
			
		// set active
		renderer.enabled = true;
		lr.enabled = true;
		lr.SetColors (Color.black, Color.black);


		target = transform.position;
		target.x = gameController.GetPlayerPosition().x;
		target.x += GetPixelDiffByDir();

		// start moving
		MovePrey ();

		if (!fishAttached)
			CheckCollideFish ();

		DrawLine ();
	}

	private void DrawLine() {
		Vector3 target = gameController.GetPlayerPosition();

		target.x += GetPixelDiffByDir();
		target.y -= 0.2f;
		lr.SetWidth(0.01f, 0.01f);
		lr.SetPosition(0, transform.position);
		lr.SetPosition(1, target);
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
		float playerY = gameController.GetPlayerPosition().y;
		if (isReelingUp) {
			float yDistance = playerY - transform.position.y;
			float ySpeed = Mathf.Abs (yDistance) * 5f;
			transform.position += Vector3.up * ySpeed * Time.deltaTime;
			if (transform.position.y > 2.8) {
				// finishing up fish[i]
				if (fishAttached) {
					gameController.FishReeled ();
				}
				Start ();
			}
		} else if (transform.position.y > -4.5) {
			transform.position += Vector3.down * ySpeed * 2f * Time.deltaTime;
		}

		pointStart.y = playerY + 1;
		pointEnd.y = transform.position.y + 0.5f;
	}

	public void Reel() {
		timeDelay = 1.0f;
		ySpeed = 1.0f;
		isReeling = true;
	}

	public void ReelUp() { 
		isReelingUp = true; 
		gameController.PlayerReel ();
		gameController.PlayReelMusic ();
	}
	public void StopReel() { ySpeed = 0f; }

	private float GetPixelDiffByDir() {
		return gameController.IsPlayerFacingRight () ? 1.2f : -1.2f;
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
		float fX = gameController.GetFishPosition().x;
		float fY = gameController.GetFishPosition().y;
		if ((x > fX - r && x < fX + r) && (y > fY - r && y < fY + r)) {
			gameController.SetFishGotHooked(true);
			fishAttached = true;
		}
	}

	private Vector3 GetPositionAtLayer(float z) {
		return (new Vector3 (transform.position.x, transform.position.y, z));
	}
}
