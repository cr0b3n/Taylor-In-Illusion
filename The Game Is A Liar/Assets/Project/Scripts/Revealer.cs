using UnityEngine;

[DisallowMultipleComponent]
public class Revealer : MonoBehaviour {

    public float speed;
    public float playerXOffset = -19f;
    public float playerYOffset = 2f;
    public float minYPosition = -6.65f;
    public Transform playerTransform;

    private bool isActive;
    private bool isBonusTime;
    private float bonusTimer;

    private void OnEnable() {
        isActive = true;

        if(playerTransform == null)
            playerTransform = GameManager.instance.player.transform;

        ResetRevealer();
    }

    private void LateUpdate() {

        if (!isActive) return;

        if (isBonusTime)
            MoveBonusTime();
        else
            MoveDefault();
    }

    private void MoveDefault() {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0));

        if (playerTransform == null)
            return;

        //Adjust the player Y position so that it can't go beyond a certain point
        if (playerTransform.position.y + playerYOffset < minYPosition)
            transform.position = new Vector3(transform.position.x, minYPosition + playerYOffset);
        else
            transform.position = new Vector3(transform.position.x, playerTransform.position.y + playerYOffset);

        if (transform.position.x + playerXOffset > playerTransform.position.x)
            gameObject.SetActive(false);
    }

    private void MoveBonusTime() {
        bonusTimer -= Time.deltaTime;
        ResetPosition();
        isBonusTime = bonusTimer > 0;
    }

    public void ResetRevealer() {
        ResetPosition();
        isBonusTime = false;
        bonusTimer = 0f;
    }

    public void ActivateBonusTime(float bonusTime) {
        ResetPosition();
        isBonusTime = true;
        bonusTimer += bonusTime;
        gameObject.SetActive(true);
    }

    private void ResetPosition() {

        float y;

        if (playerTransform.position.y + playerYOffset < minYPosition)
            y = minYPosition + playerYOffset;
        else
            y = playerTransform.position.y + playerYOffset;

        transform.position = new Vector3(playerTransform.position.x, y);
    }

    public void StopActivity() {

        bonusTimer = 10f;
        isBonusTime = true;
        gameObject.SetActive(true);
        Invoke("SetInactivity", 2f);
    }

    private void SetInactivity() {
        isActive = false;
        ResetPosition();
    }

    //private bool CheckIfCountDownElapsed(float duration) {

    //    stateTimeElapse += Time.deltaTime;
    //    return stateTimeElapse >= duration;
    //}
}
