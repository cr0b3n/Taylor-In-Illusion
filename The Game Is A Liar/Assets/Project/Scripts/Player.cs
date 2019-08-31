using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Player : MonoBehaviour {


    public Animator animator;
    public PlayerInput input;
    public Rigidbody2D myRB;
    public PlayerMovement movement;
    public float deathSpawnDelay = 2f;
    public SpriteRenderer faceRenderer;
    public Sprite mainFace;
    public Sprite altFace;

    private int speedAnimID;
    private int isJumpingAnimID;
    private int onGroundAnimID;
    private int velocityAnimID;
    private int isActiveAnimID;
    private int inactiveAnimID;

    private bool isActive;

    private void Start() {

        isActive = true;
        input.isActive = isActive;
        EffectManager.instance.ShowPlayerSpawnEffect(new Vector3(transform.position.x, 1.09f));

        //Set up animation int ID cause they are faster than strings
        speedAnimID = Animator.StringToHash("speed");
        isJumpingAnimID = Animator.StringToHash("isJumping");
        onGroundAnimID = Animator.StringToHash("onGround");
        velocityAnimID = Animator.StringToHash("velocity");
        isActiveAnimID = Animator.StringToHash("isActive");
        inactiveAnimID = Animator.StringToHash("inactive");
    }

    private void Update() {

        animator.SetBool(isJumpingAnimID, input.jumpPressed);
        animator.SetFloat(velocityAnimID, myRB.velocity.y);
        animator.SetBool(onGroundAnimID, movement.isOnGround);
        
        animator.SetFloat(speedAnimID, Mathf.Abs(input.horizontal));
    }
    
    public void SetDeath() {
        input.isActive = false;
        myRB.isKinematic = true;
        myRB.velocity = Vector2.zero;
        EffectManager.instance.ShowDeathEffect(new Vector3(transform.position.x, transform.position.y + 2f));
        AudioManager.PlayPlayerAudio(PlayerAudio.Death);
        Invoke("Respawn", deathSpawnDelay);
    }

    public void MakeInactive(float inactiveTimer) {
        isActive = false;
        input.isActive = isActive;
        faceRenderer.sprite = altFace;
        animator.SetBool(isActiveAnimID, isActive);
        animator.SetTrigger(inactiveAnimID);
        
        Invoke("MakeActive", inactiveTimer);
    }

    public void MakeInactive() {
        isActive = false;
        input.isActive = isActive;
    }

    private void MakeActive() {
        isActive = true;
        input.isActive = isActive;
        faceRenderer.sprite = mainFace;
        animator.SetBool(isActiveAnimID, isActive);
    }

    private void Respawn() {          
        GameManager.instance.Respawn();
    }

    public void RespawnRequirement() {
        CancelInvoke();
        myRB.isKinematic = true;
        myRB.velocity = Vector2.zero;

        MakeActive();
        myRB.isKinematic = false;
    }
}
