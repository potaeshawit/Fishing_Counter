using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject playerObj;
	public GameObject fishObj;
	public GameObject preyObj;
	public GameObject barObj;

	private PlayerManager player;
	private FishManager fish;
	private PreyManager prey;
	private BarManager bar;

	// Use this for initialization
	void Start () {
		player = playerObj.GetComponent<PlayerManager> ();
		fish = fishObj.GetComponent<FishManager> ();
		prey = preyObj.GetComponent<PreyManager> (); 
		bar = barObj.GetComponent<BarManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Vector3 GetPreyPosition() {
		return prey.transform.position;
	}

	public void PreyReelUp() {
		prey.ReelUp ();
	}

	public void SetBar(int fishIndex) {
		bar.SetBar (fishIndex);
	}
}
