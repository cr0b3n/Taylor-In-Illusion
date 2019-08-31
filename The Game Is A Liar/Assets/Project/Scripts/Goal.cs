using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Goal : MonoBehaviour {

    private int playerLayer;

    private void Start() {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer != playerLayer)
            return;

        collision.GetComponent<Player>()?.MakeInactive();
        GameManager.instance.GoalAchieved();
        //TODO :: Gui disable menus
        EffectManager.instance.ShowGoalEffect(collision.transform.position);
        AudioManager.PlayAudioEffect(EffectAudio.Goal);
        //Debug.Log("Goal Reach!!! Roll Credits!!!");
    }
}
