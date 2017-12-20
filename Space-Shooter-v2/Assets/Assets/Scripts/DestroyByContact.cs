using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
    //vFx
    public GameObject explosion, playerExplosion;

    //Asteroids score
    public int scoreValue;
    private GameController gameController;

    void Start(){
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
            gameController = gameControllerObject.GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider other){
        //if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy"));
        if (other.tag == "Boundary" || other.tag == "Enemy")
            return;
        if (explosion != null)
            Instantiate(explosion, transform.position, transform.rotation);
        if (other.tag == "Player") { 
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
