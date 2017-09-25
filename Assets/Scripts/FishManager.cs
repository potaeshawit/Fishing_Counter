using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour {

	Animator anim;

	List<Fish> fish;
	Fish currFish;

	Vector2 speed;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		fish = InitializeFish();
		currFish = fish [0];
		speed = new Vector2 ();
	}
	
	// Update is called once per frame
	void Update () {
		// 1. If first time, move to initPoint
		if (currFish.GetFirstInit()) {
			transform.position = currFish.GetInitPoint ();
			currFish.SetFirstInit (false);
			return;
		}

		// 2. Move to each checkpoints
		MoveFish();

		// 3. reached target
		if (ReachedTarget()) {
			RegenNextFish ();
		}
	}

	private void MoveFish() {
		// travel-x
		Vector2 targetPoint = currFish.GetCheckPoints()[0];
		float multiplier = GetSpeedMultiplier();

		MoveX (targetPoint.x, multiplier);
		MoveY (targetPoint.y, multiplier);

		transform.position = GetPositionAtLayer (-8.0f);
	}

	private void MoveX(float x, float multiplier) {
		float xDistance = x - transform.position.x;
		speed.x = Mathf.Abs (xDistance) * multiplier;
		if (xDistance > 0) {
			// facing right toward target
			transform.position += Vector3.right * speed.x * Time.deltaTime;
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		} else {
			transform.position += Vector3.left * speed.x * Time.deltaTime;
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}
	}

	private void MoveY(float y, float multiplier) {
		float yDistance = y - transform.position.y;
		speed.y = Mathf.Abs (yDistance) * multiplier;
		if (yDistance > 0) {
			// facing down toward target
			transform.position += Vector3.up * speed.y * Time.deltaTime;
		} else {
			transform.position += Vector3.down * speed.y * Time.deltaTime;
		}
	}

	private bool ReachedTarget() {
		return (speed.x < 0.5 && speed.y < 0.5);
	}

	private void RegenNextFish() {
		if (currFish.GetCheckPoints ().Count == 1) {
			if (fish.Count > 0) {
				fish.RemoveAt (0);
				currFish = fish [0];
				anim.SetInteger ("FishCount", currFish.GetID());
			}
		} else {
			currFish.DequeueCheckPoints ();
		}
	}

	private float GetSpeedMultiplier() {
		return (currFish.GetCheckPoints ().Count == currFish.GetCheckPointSize ()) ? 0.2f : 0.7f;
	}

	private Vector3 GetPositionAtLayer(float z) {
		return (new Vector3 (transform.position.x, transform.position.y, z));
	}
		
	private List<Fish> InitializeFish() {
		List<Fish> f = new List<Fish> ();
		for (int i = 1;  i <= 4;  i++) f.Add (new Fish (i, 8, 6));
		for (int i = 5;  i <= 8;  i++) f.Add (new Fish (i, 8, 6));
		for (int i = 9;  i <= 12; i++) f.Add (new Fish (i, 7, 6));
		for (int i = 13; i <= 16; i++) f.Add (new Fish (i, 6, 6));
		for (int i = 17; i <= 20; i++) f.Add (new Fish (i, 6, 6));
		return f;
	}
}
