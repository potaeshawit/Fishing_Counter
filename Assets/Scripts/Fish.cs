using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish {

	static int[] RANGE_X = {-800, 800};
	static int[] RANGE_Y = {50, -400};

	private int ID;
	private Vector2 initPoint;
	private int checkPointSize;
	private List<Vector2> checkPoints;
	private bool facingRight;
	private bool firstInit;
	private float scale;

	public Fish(int ID, int checkPointSize, float scale) {
		this.ID = ID;
		this.checkPointSize = checkPointSize;
		this.scale = scale;

		int randForX = Random.Range (0, 2);
		float randX = (randForX == 0) ? -13.5f : 13.5f;
		float randY = Random.Range (RANGE_Y[0], RANGE_Y[1]) / 100.0f;
		initPoint = new Vector2 (randX, randY);

		this.GenCheckPoints ();
		checkPoints.Add (initPoint);

		facingRight = (initPoint.x < 0);
		firstInit = true;
	}

	private void GenCheckPoints() {
		checkPoints = new List<Vector2> ();
		for (int i = 0; i < checkPointSize; i++) {
			float x = Random.Range (RANGE_X[0], RANGE_X[1]) / 100.0f;
			float y = Random.Range (RANGE_Y[0], RANGE_Y[1]) / 100.0f;
			Vector2 point = new Vector2 (x, y);
			checkPoints.Add (point);
		}
	}

	public int GetID() {
		return this.ID;
	}

	public float GetScale() {
		return this.scale;
	}
	public void SetScale(float scale) {
		this.scale = scale;
	}
		
	public Vector2 GetInitPoint() {
		return this.initPoint;
	}

	public List<Vector2> GetCheckPoints() { return this.checkPoints; }
	public void DequeueCheckPoints() {
		this.checkPoints.RemoveAt (0);
	}

	public int GetCheckPointSize() { return this.checkPointSize; }

	public bool GetFacingRight() {
		return this.facingRight;
	}

	public bool GetFirstInit() { return this.firstInit; }
	public void SetFirstInit(bool boolean) { this.firstInit = boolean; }
}
