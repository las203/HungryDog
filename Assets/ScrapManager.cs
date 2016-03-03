using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrapManager : MonoBehaviour {

	public GameObject scrapTypes;
	public float spawnRate;
	public List<Scrap> scrapList = new List<Scrap>();
	GameManager gameManager;

	void Start() {
		gameManager = FindObjectOfType<GameManager>();
		InvokeRepeating("SpawnScrap", 0, spawnRate);
	}

	void FixedUpdate() {
		/*if (gameManager.level >= 7) {
			if (Random.value < 0.01) {
				SpawnScrap();
				Debug.Log("Spawn random");
			}
		}*/
	}

	private int ChooseScrap() {
		float random = Random.value;
		if (random < 0.01)
			return 4;
		else if (random < 0.05)
			return 3;
		else if (random < 0.3)
			return 2;
		else if (random < 0.5)
			return 1;
		else
			return 0;
	}

	public void SpawnScrap() {
		Scrap scrap;
		if (scrapList.Count > 0) {
			scrap = RecycleScrap(scrapTypes.GetComponentsInChildren<Scrap>()[ChooseScrap()]);
		}
		else {
			scrap = (Scrap)Instantiate (scrapTypes.GetComponentsInChildren<Scrap>()[ChooseScrap()], new Vector3(Random.Range(-6F, 6F), 6, 0), new Quaternion(0, 0, 0, 0));
		}
		scrap.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
	}

	public void AddToRecycleList(Scrap scrap) {
		scrapList.Add(scrap);
		scrap.transform.position = new Vector3(0, 10, 0);
		scrap.GetComponent<Rigidbody2D>().isKinematic = true;
	}

	public Scrap RecycleScrap(Scrap scrapType) {
		Scrap scrap = scrapList[0];
		scrapList.RemoveAt(0);
		scrap.value = scrapType.value;
		scrap.GetComponent<Rigidbody2D>().drag = scrapType.GetComponent<Rigidbody2D>().drag;
		scrap.transform.position = new Vector3(Random.Range(-6F, 6F), 6, 0);
		scrap.GetComponent<Rigidbody2D>().isKinematic = false;
		return scrap;
	}
}
