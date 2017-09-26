using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishManager : MonoBehaviour {

	Animator anim;
	List<Fish> fish;
	Fish currFish;
	Vector2 speed;
	bool gotHooked;

	public GameObject prey;
	public GameObject soundSystem;
	public GameObject buttons;
	public GameObject textScore;
	private Text score;

	private int fishCount;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		fish = InitializeFish();
		currFish = fish [0];
		speed = new Vector2 ();
		gotHooked = false;
		fishCount = 0;
		score = textScore.GetComponent<Text> ();
		score.text = "" + fishCount;
	}
	
	// Update is called once per frame
	void Update () {
		if (gotHooked) {
			BeingReeled ();
			return;
		}

		// 1. If first time, move to initPoint
		if (currFish.GetFirstInit()) {
			RenderFish ();
			return;
		}

		// 2. Move to each checkpoints
		MoveFish();

		// 3. reached target
		if (ReachedTarget()) {
			Survive ();
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

	private void Survive() {
		if (currFish.GetCheckPoints ().Count == 1)
			InitNextFish ();
		else
			currFish.DequeueCheckPoints ();
	}

	private float GetSpeedMultiplier() {
		return (currFish.GetCheckPoints ().Count == currFish.GetCheckPointSize ()) ? 0.42f : 0.7f;
	}

	private Vector3 GetPositionAtLayer(float z) {
		return (new Vector3 (transform.position.x, transform.position.y, z));
	}
		
	private List<Fish> InitializeFish() {
		float[] scale = new float[] {
			4.0f, 3.7f, 4.0f, 4.0f, 2.0f, 3.0f, 3.0f, 3.0f, 4.0f, 4.0f, 
			4.0f, 3.0f, 6.0f, 5.0f, 4.5f, 7.0f, 4.0f, 10.0f, 2.0f, 12.0f 	
		};

		List<Fish> f = new List<Fish> ();
		for (int i = 0;  i < 20;  i++) 
			f.Add (new Fish (i + 1, 6, scale[i]));
		return f;
	}

	public void SetGotHooked(bool boolean) {
		this.gotHooked = boolean;
	}

	public void BeingReeled() {
		// position
		transform.position = prey.transform.position;

		// scale
		float interative = (float) ((currFish.GetScale() - 0.7) * 0.2);
		currFish.SetScale (currFish.GetScale () - interative);
		transform.localScale = new Vector3 (currFish.GetScale(), currFish.GetScale(), 0);

		prey.GetComponent<PreyManager> ().ReelUp ();
	}

	public void Reeled() {
		InitNextFish ();
		RenderFish ();
		buttons.GetComponent<ReelButtonManager>().ReelUpClicked();
		soundSystem.GetComponent<SoundSystem> ().PlayCountSpeech (currFish.GetID());
		score.text = "" + fishCount;
		fishCount++;
	}

	private void InitNextFish() {
		if (fish.Count > 0) {
			fish.RemoveAt (0);
			currFish = fish [0];
			anim.SetInteger ("FishCount", currFish.GetID());
		}
	}

	private void RenderFish() {
		gotHooked = false;
		transform.position = currFish.GetInitPoint ();
		float scale = currFish.GetScale ();
		transform.localScale = new Vector3 (scale, scale, 0);
		currFish.SetFirstInit (false);
	}
}