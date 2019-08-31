using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class BackgroundAdjuster : MonoBehaviour {

    public float size = 22f;
    public float allowedXOffset;

    private Transform playerTransform;

    private void Start() {
        playerTransform = GameManager.instance.player.transform;
    }

    private void LateUpdate() {
        AdjustPosition();
    }

    private void AdjustPosition() {

        if (playerTransform.position.x > transform.position.x + allowedXOffset) {
            //currentXPosition += (size * 2);
            transform.position = new Vector3(transform.position.x + (size * 2), transform.position.y);
        } else if (playerTransform.position.x < transform.position.x - allowedXOffset) {
            //currentXPosition -= (size * 2);
            transform.position = new Vector3(transform.position.x - (size * 2), transform.position.y);
        }
    }
}
