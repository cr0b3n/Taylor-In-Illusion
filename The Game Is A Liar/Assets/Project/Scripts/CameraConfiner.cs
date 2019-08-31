using UnityEngine;

[DisallowMultipleComponent]
public class CameraConfiner : MonoBehaviour {

    public float minXPosition;
    public float maxXPosition;

    private Transform playerTransform;

    private void Start() {
        playerTransform = GameManager.instance.player.transform;
    }

    private void LateUpdate() {

        if (playerTransform.position.x < minXPosition || playerTransform.position.x > maxXPosition)
            return;

        transform.position = new Vector3(playerTransform.position.x, transform.position.y);
    }

    public void CheckPlayerTransform() {
        transform.position = new Vector3(playerTransform.position.x, transform.position.y);
    }
}
