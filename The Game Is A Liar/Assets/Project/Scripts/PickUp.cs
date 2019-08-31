using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class PickUp : MonoBehaviour {

    public float timer = 2f;
    public bool isGood = true;
    public bool showAltGraphic;

    private int playerLayer;
    private bool isActive;

    private void Start() {
        playerLayer = LayerMask.NameToLayer("Player");
        isActive = true;

        if(showAltGraphic)
            GameManager.instance.GetAltGraphic(isGood, transform.GetChild(0));
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.layer != playerLayer)
            return;

        if(!isActive) {
            CancelInvoke();
            return;
        }

        EffectManager.instance.ShowPickUpEffect(isGood, transform.position);

        if (isGood) {            
            GameManager.instance.ActivatePickUpBonus(timer);
            AudioManager.PlayAudioEffect(EffectAudio.Bonus);
            Destroy(gameObject);
        } else {

            collision.GetComponent<Player>()?.MakeInactive(timer);
            isActive = false;
            AudioManager.PlayAudioEffect(EffectAudio.Penalty);
            transform.GetChild(0)?.gameObject.SetActive(false);           
        }
            
        //Debug.Log("Player has pick up and item!!");       
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.layer != playerLayer || isActive)
            return;

        Invoke("Reactivate", timer);
    }

    private void Reactivate() {
        isActive = true;

        transform.GetChild(0)?.gameObject.SetActive(true);
    }
}
