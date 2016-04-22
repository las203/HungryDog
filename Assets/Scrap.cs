using UnityEngine;
using System.Collections;

public class Scrap : MonoBehaviour {

	public long value;
	public GameManager gameManager;
	public ScrapManager scrapManager;
	private Rigidbody2D rb;

	void Start() {
		gameManager = FindObjectOfType<GameManager>();
		scrapManager = FindObjectOfType<ScrapManager>();
		rb = this.GetComponent<Rigidbody2D>();
		rb.drag = rb.drag * gameManager.dragMod;
	}

	void FixedUpdate() {
		if (this.transform.position.y < -6) {
			gameManager.scrapsLost++;
			gameManager.UpdateLostScraps();	
			scrapManager.AddToRecycleList(this);
		}
	}
}
