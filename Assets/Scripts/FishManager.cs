using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishManager : MonoBehaviour {

	public GameObject gameControllerObj;
	private GameController gameController;

	private Animator anim;
	private Vector2 speed;
	private Fish currFish;
	private List<Fish> fish;
	private int fishIndex;
	private int fishCount;
	private bool gotHooked;

	// Use this for initialization
	void Start () {
		gameController = gameControllerObj.GetComponent<GameController> ();
		anim = GetComponent<Animator> ();

		fish = InitializeFish();
		speed = new Vector2 ();
		gotHooked = false;
		fishCount = 0;
		fishIndex = 0;
		currFish = fish [fishIndex];

		gameController.SetTextScore ("" + fishCount);
		gameController.SetBar (fishIndex);
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
		if (ReachedTarget())
			Survive ();
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
		Vector3 newPosY = (yDistance > 0) ? Vector3.up : Vector3.down;
		transform.position += newPosY * speed.y * Time.deltaTime;
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
			4.0f, 3.0f, 4.5f, 5.0f, 4.5f, 4.5f, 4.0f, 4.5f, 2.0f, 4.5f 	
		};

		List<Fish> f = new List<Fish> ();
		for (int i = 0;  i < 20;  i++) 
			f.Add (new Fish (i + 1, 1, scale[i]));
		return f;
	}

	public void SetGotHooked(bool boolean) {
		this.gotHooked = boolean;
	}

	public void BeingReeled() {
		// position
		transform.position = gameController.GetPreyPosition();

		// scale
		float interative = (float) ((currFish.GetScale() - 0.7) * 0.2);
		currFish.SetScale (currFish.GetScale () - interative);
		transform.localScale = new Vector3 (currFish.GetScale(), currFish.GetScale(), 0);

		gameController.PreyReelUp ();
	}

	public void Reeled() {
		InitNextFish ();
		RenderFish ();
		gameController.ReelUpClicked();
		gameController.PlayCountSpeech (fishCount);
		fishCount++;
		gameController.SetTextScore ("" + fishCount);
		if (fishIndex == 20) {
			gameController.SetGlobalScore ();
			gameController.LoadScene ("EndScene");
		}
	}

	private void InitNextFish() {
		if (fishIndex < 19) {
			currFish = fish [++fishIndex];
			anim.SetInteger ("FishCount", currFish.GetID ());
			gameController.SetBar (fishIndex);
		} else {
			gameController.SetBar (fishIndex++);
			gameController.SetGlobalScore ();
			gameController.LoadScene ("EndScene");
		}
	}

	private void RenderFish() {
		gotHooked = false;
		transform.position = currFish.GetInitPoint ();
		float scale = currFish.GetScale ();
		transform.localScale = new Vector3 (scale, scale, 0);
		currFish.SetFirstInit (false);
	}

	public int GetFishCount() {
		return fishCount;
	}
}