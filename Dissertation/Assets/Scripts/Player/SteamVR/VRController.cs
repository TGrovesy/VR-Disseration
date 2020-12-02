using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{

    public float sensitivity = 0.1f;
    public float maxSpeed = 1.0f;


    private float speed = 0.0f;

    //Gameobjects
    private CharacterController characterController = null;
    private Transform cameraRig = null;
    private Transform head = null;

    //Input
    public GameObject inputManagerObj;
    private InputManager inputManager;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
        inputManager = inputManagerObj.GetComponent<InputManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraRig = SteamVR_Render.Top().origin;
        head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    void Update()
    {
        HandleHead();
        HandleHeight();
        CalculateMovment();
    }

    private void HandleHead() {
        //store current
        Vector3 oldPosition = cameraRig.position;
        Quaternion oldRotation = cameraRig.rotation;

        //rotation
        transform.eulerAngles = new Vector3(0.0f, head.rotation.eulerAngles.y, 0.0f);

        //restore
        cameraRig.position = oldPosition;
        cameraRig.rotation = oldRotation;
    }

    private void HandleHeight() {
        //Get head in local space
        float headHeight = Mathf.Clamp(head.localPosition.y, 1, 2);
        characterController.height = headHeight;

        //cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;

        //Move capsule in local space
        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;

        //rotate
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        //apply
        characterController.center = newCenter;
    }

    private void CalculateMovment() {
        //Orientation
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        //If not moving
        if(Mathf.Abs(inputManager.GetLeftThumbAnalogX()) < 0.1f && Mathf.Abs(inputManager.GetLeftThumbAnalogY()) < 0.1f) {
            speed = 0;
        } else {
            Debug.Log("move");
            speed = inputManager.GetLeftThumbAnalogY() * sensitivity;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

            movement += orientation * (speed * Vector3.zero) * Time.deltaTime;
        }

        characterController.Move(movement);

    }
}
