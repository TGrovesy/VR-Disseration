using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragLoco : MonoBehaviour
{

    //Input Manager
    public GameObject inputManagerObj;
    private InputManager inputManager;

    public float speed = 150.0f;

    //Player
    public GameObject player;
    private GameObject leftHand;
    private GameObject rightHand;

    //Comfort
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 originClk;
    private bool triggerTransform = false;
    private bool leftHandTrack = false;
    private bool inputDown = false;
    private GameObject transformObj;

    //Comfort Vignette
    private bool firstClick = false;
    public void HandleMovement() {
        AssignVariables();
        if(firstClick) timer += Time.deltaTime;

        //Obtain Controller exclusivitey to ensure not getting input from multiple controllers
        if (!triggerTransform) {
            if (inputManager.GetLeftADown()) {
                transformObj = leftHand;
                leftHandTrack = true;
                inputDown = true;
            } else if (inputManager.GetRightADown()) {
                transformObj = rightHand;
                leftHandTrack = false;
                inputDown = true;
            }
        }

        if (inputDown && !triggerTransform) {
            originClk = transformObj.transform.position;
            triggerTransform = true;
            if(!firstClick)player.GetComponent<VignetteApplier>().FadeIn();
            firstClick = true;
            timer = 0.0f;
        }else if (triggerTransform && inputDown) {//drag and apply movement
            Vector3 movement = new Vector3(originClk.x - transformObj.transform.position.x, 0, originClk.z - transformObj.transform.position.z);
            player.GetComponent<CharacterController>().Move(movement * speed * Time.deltaTime);
            originClk = transformObj.transform.position;
        }

        if(triggerTransform) {
            if (leftHandTrack) {
                if (!inputManager.GetLeftADown()) {
                    triggerTransform = false;
                    inputDown = false;
                }
            } else {
                if (!inputManager.GetRightADown()) {
                    triggerTransform = false;
                    inputDown = false;
                }
            }
        } else {
            if (timer > 0.75f) {
                firstClick = false;
                player.GetComponent<VignetteApplier>().FadeOut();
            }
        }

        
    }

    private void AssignVariables() {
        if (inputManager == null) {
            inputManager = AssetManager.GetInputManager();
        }

        if(leftHand == null) {
            leftHand = AssetManager.GetLeftController();
        }

        if (rightHand == null) {
            rightHand = AssetManager.GetRightController();
        }
    }
}
