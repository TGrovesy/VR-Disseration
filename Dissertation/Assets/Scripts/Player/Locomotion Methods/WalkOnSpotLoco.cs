using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkOnSpotLoco : MonoBehaviour {


    //Input Manager
    public GameObject inputManagerObj;
    private InputManager inputManager;


    //Player
    public GameObject player;

    //Player
    private GameObject leftHand;
    private GameObject head;

    //Walk parameters
    public float playerHeight = 1.85f;
    public float sensitivity = 0.05f;//deviation needed to move forward
    public float speed = 1.5f;

    // Start is called before the first frame update
    void Start() {
        inputManager = AssetManager.GetInputManager();
    }

    //Should move
    private bool triggered = false;
    private bool move = false;
    private float timeElapsedSinceTrigger = 0.0f;
    private float returnTimer = 0.0f;

    //height average
    private float heightCalcTimer = 0;
    private float averageHeight = 0;
    private int samples = 0;
    private bool calcAverage = true;
    public void HandleMovement() {
        AssignVariables();

        //DEBUG HEIGHT SET REMOVE!!!!
        if (AssetManager.GetInputManager().GetLeftTriggerDown()) {
            playerHeight = AssetManager.GetHead().transform.localPosition.y;
        }

        //calc height average 
        //TODO MOVE THIS TO THE PLAYER!!!! SHOULD NOT DO THIS HERE!
        if (true) {//only calculate when not moveing
            heightCalcTimer += Time.deltaTime;
            averageHeight += AssetManager.GetHead().transform.localPosition.y;
            samples++;
            if(heightCalcTimer >= 3) {
                calcAverage = false;
                heightCalcTimer = 0.0f;
                playerHeight = averageHeight / samples;
                averageHeight = 0.0f;
                samples = 0;
            }
        }


        if (move) {
            timeElapsedSinceTrigger += Time.deltaTime;//since last trigger increment
            returnTimer += Time.deltaTime;
        }
        
        //if barrier is triggered
        if (Mathf.Abs(playerHeight - AssetManager.GetHead().transform.localPosition.y) >= sensitivity){
            if(!triggered) player.GetComponent<VignetteApplier>().FadeIn();//comfort enable
            move = true;
            triggered = true;
            timeElapsedSinceTrigger = 0.0f; //trigger happend reset
        } else {
            returnTimer = 0.0f;
        }

        Vector3 moveDirection = Quaternion.AngleAxis(AssetManager.GetHead().transform.rotation.eulerAngles.y, Vector3.up) * Vector3.forward;//get the angle user is facing
        if (move && returnTimer < 0.25f) {//must have returned within 0.25 Secs
            player.GetComponent<CharacterController>().Move(moveDirection * speed * Time.deltaTime);
            if(timeElapsedSinceTrigger >= 0.75f) {//frequency for trigger to activate
                move = false;
                triggered = false;
                player.GetComponent<VignetteApplier>().FadeOut();//comfort disable
            }
        }
    }

    private void AssignVariables() {
        if (leftHand == null) {
            leftHand = AssetManager.GetLeftController();
        }

        if (head == null) {
            head = AssetManager.GetHead();
        }
    }

    public static float Angle(Vector2 p_vector2) {
        if (p_vector2.x < 0) {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        } else {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }

}
