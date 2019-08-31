using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class DeathChecker : MonoBehaviour {

    private int playerLayer;
    private Transform playerTransform;

    private void Start() {
        playerLayer = LayerMask.NameToLayer("Player");
        playerTransform = GameManager.instance.player.transform;
    }


    private void LateUpdate() {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer != playerLayer)
            return;

        //Debug.Log("Player Detected! PlayerDied!");

        collision.GetComponent<Player>()?.SetDeath();
    }
}
