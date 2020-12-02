using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class LocomotionManager : MonoBehaviour
{
    //Locomotion Type
    public enum LocomotionType {
        SmoothHand,
        SmoothHead,
        Drag,
        Teleport,
        WalkSpotHead
    }
    private int numberOfLocomotionTypes  = 4;//n-1
    public LocomotionType currentLocoType = 0;

    // Start is called before the first frame update
    void Start()
    {
        DisableAllLocomotionScripts();
        EnableLocomotionScript(currentLocoType);
    }


    private bool leftBClick;

    // Update is called once per frame
    void Update()
    {
        if (!leftBClick && AssetManager.GetInputManager().GetLeftBDown()) {
            leftBClick = true;
            CycleLocomotionType();
        }

        if (leftBClick && !AssetManager.GetInputManager().GetLeftBDown()) {
            leftBClick = false;
        }

        Turn();

    }

    public void HandleMovement() {
        if (this.currentLocoType == LocomotionType.SmoothHand) {
            GetComponent<SmoothLoco>().HandleMovement();
        } else if (this.currentLocoType == LocomotionType.SmoothHead) {
            GetComponent<SmoothLoco>().HandleMovement();
        } else if (this.currentLocoType == LocomotionType.Drag) {
            GetComponent<DragLoco>().HandleMovement();
        } else if (this.currentLocoType == LocomotionType.Teleport) {
            GetComponent<TeleportLoco>().HandleMovement();
        }else if(this.currentLocoType == LocomotionType.WalkSpotHead) {
            GetComponent<WalkOnSpotLoco>().HandleMovement();
        }
    }

    private void EnableLocomotionScript(LocomotionType type) {
        if(type == LocomotionType.SmoothHand) {
            GetComponent<SmoothLoco>().enabled = true;
            GetComponent<SmoothLoco>().handBased = true;
        }else if(type == LocomotionType.SmoothHead) {
            GetComponent<SmoothLoco>().enabled = true;
            GetComponent<SmoothLoco>().headBased = true;
        } else if (type == LocomotionType.Drag) {
            GetComponent<DragLoco>().enabled = true;
        } else if (type == LocomotionType.Teleport) {
            GetComponent<TeleportLoco>().enabled = true;
        }else if(type == LocomotionType.WalkSpotHead) {
            GetComponent<WalkOnSpotLoco>().enabled = true;
        }
    }

    private void DisableAllLocomotionScripts() {
        GetComponent<SmoothLoco>().enabled = false;
        GetComponent<SmoothLoco>().handBased = false;
        GetComponent<SmoothLoco>().headBased = false;

        GetComponent<DragLoco>().enabled = false;

        GetComponent<TeleportLoco>().enabled = false;

        GetComponent<WalkOnSpotLoco>().enabled = false;
    } 

    private void CycleLocomotionType() {
        DisableAllLocomotionScripts();
        if (currentLocoType >= (LocomotionType)numberOfLocomotionTypes) {
            currentLocoType = 0;
        } else {
            currentLocoType++;
        }
        EnableLocomotionScript(currentLocoType);
    }

    private bool turnTrigger = false;
    private float deadZoneSnapTurn = 0.3f;
    private float turnAngle = 22.5f;
    private void Turn() {
        float rightAnalogX = AssetManager.GetInputManager().GetRightThumbAnalogX();
        if (rightAnalogX > deadZoneSnapTurn) {//turn right
            if (!turnTrigger) {
                turnTrigger = true;
                AssetManager.GetPlayer().transform.Rotate(0, turnAngle, 0);
                Debug.Log("turn right");//TODO REMOVE
            }
        }else if(rightAnalogX < -deadZoneSnapTurn) {//turn left
            if (!turnTrigger) {
                turnTrigger = true;
                AssetManager.GetPlayer().transform.Rotate(0, -turnAngle, 0);
                Debug.Log("turn left");//TODO REMOVE
            }
        } else {
            turnTrigger = false;
        }
    }

    /*
     * TODO CREATE A PROPER REFERENCE SYSTEM!!!
     */
    public InputManager GetInputManager() {
        return AssetManager.GetInputManager();
    }
}
