using UnityEngine;
using System.Collections;

public class Scrap : MonoBehaviour {

	public long value;
	public GameManager gameManager;
	public ScrapManager scrapManager;

	void Start() {
		gameManager = FindObjectOfType<GameManager>();
		scrapManager = FindObjectOfType<ScrapManager>();
	}

	void FixedUpdate() {
		if (this.transform.position.y < -6) {
			gameManager.scrapsLost++;
			gameManager.UpdateLostScraps();	
			scrapManager.AddToRecycleList(this);
		}
	}
}
