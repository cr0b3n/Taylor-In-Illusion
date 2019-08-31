using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SavePoint : MonoBehaviour {

    private int playerLayer;

    private void Start() {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer != playerLayer)
            return;

        //Debug.Log("Player Detected! Setting New Save Point");
        GameManager.instance.currentSavePoint = transform;
    }
}
