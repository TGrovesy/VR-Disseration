#if (!UNITY_ANDROID)
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVRInputManager : MonoBehaviour
{

    //Input Manager
    public GameObject inputManagerObj;
    private InputManager inputManager;

    //Hands
    public GameObject leftHand;
    public GameObject rightHand;

    //SteamVR
    //Trigger
    public SteamVR_Action_Boolean rightTrigger;
    private bool rightTriggerValue;
    public SteamVR_Action_Boolean leftTrigger;
    private bool leftTriggerValue;

    //Controller Face
    public SteamVR_Action_Boolean leftA;
    private bool leftAValue;
    public SteamVR_Action_Boolean leftB;
    private bool leftBValue;
    public SteamVR_Action_Boolean rightA;
    private bool rightAValue;
    public SteamVR_Action_Boolean rightB;
    private bool rightBValue;

    //Grips
    public SteamVR_Action_Boolean leftGripClk;
    private bool leftGripClkValue;
    public SteamVR_Action_Boolean rightGripClk;
    private bool rightGripClkValue;

    //Thumbstick
    public SteamVR_Action_Vector2 rightThumbAnalog;
    private Vector2 rightThumbAnalogValue;
    public SteamVR_Action_Vector2 leftThumbAnalog;
    private Vector2 leftThumbAnalogValue;



    // Start is called before the first frame update
    void Start()
    {

        //Add input listeners
        AddInputListeners();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager == null) inputManager = AssetManager.GetInputManager();
    }

    private void AddInputListeners() {
        //right trigger
        rightTrigger.AddOnStateDownListener(RightTriggerDown, SteamVR_Input_Sources.RightHand);
        rightTrigger.AddOnStateUpListener(RightTriggerUp, SteamVR_Input_Sources.RightHand);

        //left trigger
        leftTrigger.AddOnStateDownListener(LeftTriggerDown, SteamVR_Input_Sources.LeftHand);
        leftTrigger.AddOnStateUpListener(LeftTriggerUp, SteamVR_Input_Sources.LeftHand);

        //controller face
        leftA.AddOnStateDownListener(LeftADown, SteamVR_Input_Sources.LeftHand);
        leftA.AddOnStateUpListener(LeftAUp, SteamVR_Input_Sources.LeftHand);
        leftB.AddOnStateDownListener(LeftBDown, SteamVR_Input_Sources.LeftHand);
        leftB.AddOnStateUpListener(LeftBUp, SteamVR_Input_Sources.LeftHand);


        rightA.AddOnStateDownListener(RightADown, SteamVR_Input_Sources.RightHand);
        rightA.AddOnStateUpListener(RightAUp, SteamVR_Input_Sources.RightHand);
        rightB.AddOnStateDownListener(RightBDown, SteamVR_Input_Sources.RightHand);
        rightB.AddOnStateUpListener(RightBUp, SteamVR_Input_Sources.RightHand);

        //Grip
        leftGripClk.AddOnStateDownListener(LeftGripDown, SteamVR_Input_Sources.LeftHand);
        leftGripClk.AddOnStateUpListener(LeftGripUp, SteamVR_Input_Sources.LeftHand);

        rightGripClk.AddOnStateDownListener(RightGripDown, SteamVR_Input_Sources.RightHand);
        rightGripClk.AddOnStateUpListener(RightGripUp, SteamVR_Input_Sources.RightHand);

        //Thumb Analog
        rightThumbAnalog.AddOnChangeListener(RightThumbAnalogChange, SteamVR_Input_Sources.RightHand);
        leftThumbAnalog.AddOnChangeListener(LeftThumbAnalogChange, SteamVR_Input_Sources.LeftHand);
    }


    private void RightTriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetRightTriggerDown();
    }

    private void RightTriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetRightTriggerUp();
    }

    private void LeftTriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetLeftTriggerDown();
    }

    private void LeftTriggerUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetLeftTriggerUp();
    }

    private void LeftADown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetLeftAButtonDown();
    }

    private void LeftAUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetLeftAButtonUp();
    }

    private void LeftBDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetLeftBButtonDown();
    }

    private void LeftBUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetLeftBButtonUp();
    }

    private void RightADown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetRightAButtonDown();
    }

    private void RightAUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetRightAButtonUp();
    }

    private void RightBDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetRightBButtonDown();
    }

    private void RightBUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetRightBButtonUp();
    }

    private void RightGripDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetRightGripDown();
    }

    private void RightGripUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetRightGripUp();
    }

    private void LeftGripDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetLeftGripDown();
    }

    private void LeftGripUp(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource) {
        inputManager.SetLeftGripUp();
    }

    private void RightThumbAnalogChange(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta) {
        inputManager.SetRightThumbAnalogX(rightThumbAnalog.axis.x);
        inputManager.SetRightThumbAnalogY(rightThumbAnalog.axis.y);

        //TODO Debug Remove
        //Debug.LogError("X: " + inputManager.GetRightThumbAnalogX() + ", Y: " + inputManager.GetRightThumbAnalogY());
    }

    private void LeftThumbAnalogChange(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta) {
        inputManager.SetLeftThumbAnalogX(leftThumbAnalog.axis.x);
        inputManager.SetLeftThumbAnalogY(leftThumbAnalog.axis.y);

        //TODO Debug Remove
        //Debug.LogError("X: " + inputManager.GetRightThumbAnalogX() + ", Y: " + inputManager.GetRightThumbAnalogY());
    }
}

#endif