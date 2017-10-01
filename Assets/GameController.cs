using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

	public GameObject playerObj;
	public GameObject fishObj;
	public GameObject preyObj;

	public GameObject barObj;
	public GameObject soundSystemObj;
	public GameObject reelButtonObj;
	public GameObject textScoreObj;
	public GameObject canvasObj;
	public GameObject mainCameraObj;

	private PlayerManager player;
	private FishManager fish;
	private PreyManager prey;
	private BarManager bar;
	private SoundSystem soundSystem;
	private ReelButtonManager reelButton;
	private Text textScore;
	private LayerManager layerManager;
	private MusicManager musicManager;

	// Use this for initialization
	void Start () {
		player = playerObj.GetComponent<PlayerManager> ();
		fish = fishObj.GetComponent<FishManager> ();
		prey = preyObj.GetComponent<PreyManager> (); 
		bar = barObj.GetComponent<BarManager> ();
		soundSystem = soundSystemObj.GetComponent<SoundSystem> ();
		reelButton = reelButtonObj.GetComponent<ReelButtonManager> ();
		textScore = textScoreObj.GetComponent<Text> ();
		layerManager = canvasObj.GetComponent<LayerManager> ();
		musicManager = mainCameraObj.GetComponent<MusicManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Fish =================================================
	public Vector3 GetPreyPosition() {
		return prey.transform.position;
	}

	public void PreyReelUp() {
		prey.ReelUp ();
	}

	public void SetBar(int fishIndex) {
		bar.SetBar (fishIndex);
	}

	public void PlayCountSpeech (int fishCount) {
		soundSystem.PlayCountSpeech (fishCount);
	}

	public void ReelUpClicked() {
		reelButton.ReelUpClicked ();
	}

	public void SetTextScore(string str) {
		textScore.text = str;
	}

	public void ShowWinLayer() {
		layerManager.ShowWinLayer (true, "" + (int.Parse(textScore.text) + 1));
	}

	// Prey =================================================
	public Vector3 GetPlayerPosition() {
		return player.transform.transform.position;
	}

	public bool IsPlayerFacingRight() {
		return player.GetFacingRight ();
	}

	public void PlayerReel() {
		player.Reel ();
	}

	public void FishReeled() {
		fish.Reeled ();
	}

	public Vector3 GetFishPosition() {
		return fish.transform.position;
	}

	public void SetFishGotHooked(bool boolean) {
		fish.SetGotHooked (boolean);
	}

	public void PlayReelMusic() {
		musicManager.PlayReelMusic ();
	}		

	public void LoadScene(string sceneName) {
		mainCameraObj.GetComponent<SceneChanger> ().LoadScene (sceneName);
	}

	public void SetGlobalScore() {
		ScoreValue.score = fish.GetFishCount ();
//			int.Parse (textScore.text);
	}

}
