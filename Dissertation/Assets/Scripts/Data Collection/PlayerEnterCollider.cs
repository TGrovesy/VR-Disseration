using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterCollider : MonoBehaviour
{

    private BoxCollider boxCollider;

    public bool isStart = false;

    //Timer stuff
    private static bool isTiming = false;
    private static float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTiming && isStart) {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("Player")) {
            if (isStart) {
                isTiming = true;
                timer = 0.0f;//ensure timer is 0
            } else {
                isTiming = false;//stop updating timer
                //print log
                AssetManager.GetDataCollecter().GetComponent<PostionSampler>().PrintLog();
                AssetManager.GetDataCollecter().GetComponent<PostionSampler>().CreatePNG();
            }
            Debug.Log("ENTERED!");
            Debug.Log(timer + "S");
        }
    }
}
