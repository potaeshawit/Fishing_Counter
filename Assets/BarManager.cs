using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour {

	public Sprite bar0;
	public Sprite bar1;
	public Sprite bar2;
	public Sprite bar3;
	public Sprite bar4;
	public Sprite bar5;
	public Sprite bar6;
	public Sprite bar7;
	public Sprite bar8;
	public Sprite bar9;
	public Sprite bar10;
	public Sprite bar11;
	public Sprite bar12;
	public Sprite bar13;
	public Sprite bar14;
	public Sprite bar15;
	public Sprite bar16;
	public Sprite bar17;
	public Sprite bar18;
	public Sprite bar19;
	public Sprite bar20;

	private Sprite[] bars;

	// Use this for initialization
	void Start () {
		bars = new Sprite[] {
			bar0, bar1, bar2, bar3, bar4, bar5, bar6, bar7,  
			bar8, bar9, bar10, bar11, bar12, bar13, bar14,  
			bar15, bar16, bar17, bar18, bar19, bar20,
		};

		GetComponent<Image> ().sprite = bars [0];
	}
	
	public void SetBar(int index) {
		if (index > 19) {
			return;
		}
		GetComponent<Image> ().sprite = bars [index];
	}
}
