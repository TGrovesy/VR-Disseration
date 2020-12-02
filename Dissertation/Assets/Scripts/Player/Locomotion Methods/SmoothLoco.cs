using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SmoothLoco : MonoBehaviour
{

    //Input Manager
    public GameObject inputManagerObj;
    private InputManager inputManager;


    //Player
    public GameObject player;
    private GameObject directionObj;
    private const float speed = 1.5f;
    private const float deadZone = 0.2f;
    float currentSpeed = 0.0f;

    //Direction Type
    public bool handBased = false;
    public bool headBased = false;

    //Are moving
    private bool isMoving = false;


    // Start is called before the first frame update
    private void Start()
    {
        inputManager = inputManagerObj.GetComponent<InputManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        //HandleMovement();
    }

    private bool onTrigger = false;
    public void HandleMovement() {

        //TODO stop string comparison as this is very inefficent todo every frame!
        if (handBased) {
            directionObj = AssetManager.GetLeftController();
        }else if (headBased) {
            directionObj = AssetManager.GetHead();
        }

        float thumbX = inputManager.GetLeftThumbAnalogX(), thumbY = inputManager.GetLeftThumbAnalogY(); ;
        Vector3 moveDirection = Quaternion.AngleAxis(Angle(new Vector2(thumbX, thumbY)) + directionObj.transform.rotation.eulerAngles.y, Vector3.up) * Vector3.forward;//get the angle of the touch and correct it for the rotation of the controller
        
        


        if(Mathf.Abs(thumbX) > 0.3 || Mathf.Abs(thumbY) > 0.3) {
            //Apply VR comfort 
            //TODO MOVE TO A VELOCITY HANDLER!!
            if (!onTrigger) {
                player.GetComponent<VignetteApplier>().FadeIn();
                onTrigger = true;
            }
            //Debug.Log("X: " + thumbX + ", Y: " + thumbY);
            player.GetComponent<CharacterController>().Move(moveDirection * speed * Time.deltaTime);
        }

        //Apply VR comfort for stop 
        //TODO MOVE TO A VELOCITY HANDLER!!
        if (Mathf.Abs(thumbX) < 0.2 && Mathf.Abs(thumbY) < 0.2) {
            if (onTrigger) {
                player.GetComponent<VignetteApplier>().FadeOut();
                onTrigger = false;
            }
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
