using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLoco : MonoBehaviour
{


    //Input Manager
    public GameObject inputManagerObj;
    private InputManager inputManager;


    //Player
    public GameObject player;

    //Teleport Values
    public float maxTeleportDistance = 15.0f;

    //Player
    private GameObject leftHand;
    private GameObject head;

    //Pointer
    public GameObject pointer;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = inputManagerObj.GetComponent<InputManager>();
    }


    private bool teleportDown = false;
    private Vector3 pos = Vector3.zero;
    public void HandleMovement() {
        AssignVariables();

        //Set telport pressed
        if (!teleportDown) {
            if(GetComponent<LocomotionManager>().GetInputManager().GetLeftThumbAnalogY() > 0.5) {
                teleportDown = true;
            }
        }

        if (teleportDown) {
            //Cast location
            Ray ray = new Ray(leftHand.transform.position, leftHand.transform.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                pos = hit.point;
                pointer.transform.position = pos;
            }
        }

        //Teleport released
        if(teleportDown && GetComponent<LocomotionManager>().GetInputManager().GetLeftThumbAnalogY() <= 0.5) {
            teleportDown = false;
            // Vector3 playerPos = new Vector3(head.transform.position.x, player.transform.position.y, head.transform.position.z);
            //Vector3 newPos = pointer.transform.position - playerPos;
            //player.GetComponent<PlayerCharacterController>().Teleport(pointer.transform.position);
            /*
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<VignetteApplier>().FadeIn();
            player.transform.position = pos;
            player.GetComponent<VignetteApplier>().FadeOut();
            player.GetComponent<CharacterController>().enabled = true;
            */
            //teleport
            player.GetComponent<CharacterController>().enabled = false;
            StartCoroutine(Teleport(player.transform.position, pos));
            player.GetComponent<CharacterController>().enabled = true;
        }
    }

    private void AssignVariables() {
        if(leftHand == null) {
            leftHand = AssetManager.GetLeftController();
        }

        if(head == null) {
            head = AssetManager.GetHead();
        }
    }

    private IEnumerator Teleport(Vector3 currentPos, Vector3 newPos) {
        float timeElapsed = 0.0f;
        float duration = 0.4f;
        player.GetComponent<VignetteApplier>().FadeIn();
        while (timeElapsed <= duration) {

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        player.transform.position = pos;
        player.GetComponent<VignetteApplier>().FadeOut();
    }
}
