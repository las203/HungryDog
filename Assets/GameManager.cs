using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public long score;
	public Text scoreDisplay;
	public Text lostScrapDisplay;
	public Text levelDisplay;
	public int scrapsLost;
	public int scrapsLostLimit;
	public float dragMod;
	public int level;
	public int totalCatches;
	public int catchesToNextLevel;
	public GameObject background;
	public Camera camera;
	public GameObject hungryDog;
	public ScrapManager scrapManager;

	void Start() {
		scrapManager = FindObjectOfType<ScrapManager>();
		ResizeBackground();
	}

	void FixedUpdate () {
		if (scrapsLost >= scrapsLostLimit) {
			Pitpex.SetScore(score);
			Pitpex.rate = .000001;
			Pitpex.GameOver();
		}
		if (totalCatches == catchesToNextLevel) {
			LevelUp();
		}
	}

	public void ResizeBackground() {
		SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
		background.transform.localScale = new Vector3(1,1,1);
		float width = sr.sprite.bounds.size.x;
		float height = sr.sprite.bounds.size.y;
		float worldScreenHeight = (float)(camera.orthographicSize * 2.0);
		float worldScreenWidth = (float)(worldScreenHeight / Screen.height * Screen.width);
		Vector2 sizeX = new Vector2(worldScreenWidth / width, background.transform.localScale.y);
		background.transform.localScale = sizeX;
		Vector2 sizeY = new Vector2(background.transform.localScale.x, worldScreenHeight / height);
		background.transform.localScale = sizeY;
	}

	public void UpdateScore(long scrapValue) {
		score = score + scrapValue;
		scoreDisplay.text = string.Format("SCORE:{0}", score);
	}

	public void UpdateLostScraps() {
		lostScrapDisplay.text = string.Format("LOST SCRAPS:{0}/{1}", scrapsLost, scrapsLostLimit);
		this.GetComponent<AudioSource>().Play();
	}

	public void UpdateCaughtScraps() {
		totalCatches++;
	}

	public void LevelUp() {
		level++;
		/*if (level < 7) {
			scrapManager.spawnRate = scrapManager.spawnRate * 1.1F;
			scrapManager.CancelInvoke("SpawnScrap");
			scrapManager.InvokeRepeating("SpawnScrap", 0, scrapManager.spawnRate);
		}
		if (level == 7) {
			scrapManager.CancelInvoke("SpawnScrap");
		}*/
		levelDisplay.text = string.Format("LEVEL: {0}", level);
		catchesToNextLevel = catchesToNextLevel * 2;
		dragMod = dragMod * dragMod;
		scrapsLost = 0;
		UpdateLostScraps();
	}

	public void SendScore() {
		Debug.Log("BWOOP: SEND THE LOG");
	}
}
