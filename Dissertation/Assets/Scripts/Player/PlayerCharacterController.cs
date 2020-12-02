using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour {
    private Transform cameraRig = null;
    private Transform head = null;
    bool headSet = false;
    bool rigSet = false;

    private CharacterController characterController = null;

    private float gravity = 9.72f;

    //Movement
    public GameObject movementScript;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (!headSet || !rigSet) {
            cameraRig = AssetManager.GetPlayerRig().transform;
            head = AssetManager.GetHead().transform;

            if (cameraRig != null) rigSet = true;
            if (head != null) headSet = true;
        } else {
            HandleHead();
            HandleHeight();
            //Handle movement
            movementScript.GetComponent<LocomotionManager>().HandleMovement();//TODO Move to a proper handle system
            characterController.Move(new Vector3(0.0f, (-gravity * Time.deltaTime), 0.0f));//Applies gravity to the player
        }
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
}
