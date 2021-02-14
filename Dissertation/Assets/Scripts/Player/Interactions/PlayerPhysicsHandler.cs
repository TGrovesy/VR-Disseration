using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToHandLayer(GameObject obj) {
        obj.layer = 8;//hand layer
    }

    public void ReturnToGlobalLayer(GameObject obj) {

        obj.layer = 0;//hand layer
    }
}
