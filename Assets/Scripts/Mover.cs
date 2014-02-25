using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public float speed;
	private GameController gameController;

	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		else {
			Debug.Log("Can not find gameControllerObject");
		}
		rigidbody.velocity = transform.forward * speed * (1 + gameController.GetLevel()/10);
	}

}
