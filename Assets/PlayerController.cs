using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int speed;
	private Rigidbody2D rb;
	public GameManager gameManager;
	public ScrapManager scrapManager;

	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		scrapManager = FindObjectOfType<ScrapManager>();
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate () {
		rb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * speed);
		if (rb.velocity.x < 0)
			this.transform.localScale = new Vector3(-5, 5, 1);
		else
			this.transform.localScale = new Vector3(5, 5, 1);
		//transform.Translate(Input.GetAxis("Horizontal") * speed, 0, 0);
		/*if (this.transform.position.x <= -6 && rb.velocity.magnitude < 0) {
			this.transform.position.Set(-6, this.transform.position.y, 0);
			rb.MovePosition(new Vector2(-6, 0));
		}*/
	}

	void OnTriggerEnter2D(Collider2D other) {
		gameManager.UpdateScore(other.gameObject.GetComponent<Scrap>().value);
		gameManager.UpdateCaughtScraps();
		scrapManager.AddToRecycleList(other.gameObject.GetComponent<Scrap>());
	}
}
