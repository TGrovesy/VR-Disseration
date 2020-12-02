using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    //Input Manager
    public GameObject inputMangerObj;
    private static InputManager inputManager;

    //Player
    private static GameObject player;
    private static GameObject playerRig;
    private static GameObject leftController;
    private static GameObject rightController;
    private static GameObject head;

    //Locomotion Manager
    public GameObject locomotionManagerObj;
    private static LocomotionManager locomotionManger;

    //Post Process Effects
    public GameObject volumeObj;

    //Timer
    private static float playTime = 0.0f;

    //Data Collection
    public static GameObject dataCollector;
    

    // Start is called before the first frame update
    void Start()
    {
        //Set input manager
        inputManager = inputMangerObj.GetComponent<InputManager>();

        //Locomotion Manager
        locomotionManger = locomotionManagerObj.GetComponent<LocomotionManager>();
    }

    private bool allAssigned = false;
    
    // Update is called once per frame
    void Update()
    {
        if (!allAssigned) {
            FirstAssignment();
        } else {
            //Debug.Log("X: " + leftController.transform.position.x);
        }

        playTime += Time.deltaTime;//Play time counter
    }

    private void FirstAssignment() {
        if(leftController == null) {
            leftController = GameObject.Find("Controller (left)");
        }

        if (rightController == null) {
            rightController = GameObject.Find("Controller (right)");
        }

        if (head == null) {
            head = GameObject.Find("Head");
        }

        if(playerRig == null) {
            playerRig = GameObject.Find("Player(Clone)");
        }

        if(player == null) {
            player = GameObject.Find("Player");
        }

        if(head != null && leftController !=null && rightController != null && playerRig!=null && player !=null) {
            allAssigned = true;
            Debug.Log("all assigned");
        }

        if(dataCollector == null) {
            dataCollector = GameObject.Find("DataCollecter");
        }
    }

    public static GameObject GetPlayer() {
        return player;
    }

    public static GameObject GetLeftController() {
        return leftController;
    }

    public static GameObject GetRightController() {
        return rightController;
    }
    
    public static GameObject GetHead() {
        return head;
    }

    public static GameObject GetPlayerRig() {
        return playerRig;
    }

    public static InputManager GetInputManager() {
        return inputManager;
    }

    public static float GetPlayTime() {
        return playTime;
    }

    public static GameObject GetDataCollecter() {
        return dataCollector;
    }
}
