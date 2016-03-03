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
	public int level;
	public int totalCatches;
	public int catchesToNextLevel;
	public GameObject hungryDog;
	public ScrapManager scrapManager;

	void Start() {
		scrapManager = FindObjectOfType<ScrapManager>();
	}

	void FixedUpdate () {
		//FIX
		if (scrapsLost >= 5) {
			Pitpex.SetScore(score);
			Pitpex.rate = .00000000001;
			Pitpex.GameOver();
		}
		if (totalCatches == catchesToNextLevel) {
			LevelUp();
		}
	}

	public void UpdateScore(long scrapValue) {
		score = score + scrapValue;
		scoreDisplay.text = string.Format("SCORE:{0}", score);
	}

	public void UpdateLostScraps() {
		lostScrapDisplay.text = string.Format("LOST SCRAPS:{0}/{1}", scrapsLost, scrapsLostLimit);
	}

	public void UpdateCaughtScraps() {
		totalCatches++;
	}

	public void LevelUp() {
		level++;
		//if (level < 7) {
			scrapManager.spawnRate = scrapManager.spawnRate * 0.9F;
			scrapManager.CancelInvoke("SpawnScrap");
			scrapManager.InvokeRepeating("SpawnScrap", 0, scrapManager.spawnRate);
		//}
		/*if (level == 7) {
			scrapManager.CancelInvoke("SpawnScrap");
		}*/
		levelDisplay.text = string.Format("LEVEL: {0}", level);
		catchesToNextLevel = catchesToNextLevel * 2;
		scrapsLostLimit++;
		scrapsLost = 0;
		UpdateLostScraps();
	}

	public void SendScore() {
		Debug.Log("BWOOP: SEND THE LOG");
	}
}
