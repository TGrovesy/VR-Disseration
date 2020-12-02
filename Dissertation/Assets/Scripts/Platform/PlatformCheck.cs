using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformCheck : MonoBehaviour
{

    private bool steamVR = false;
    private bool XR = false;

    // Start is called before the first frame update
    void Awake()
    {
        /*
        if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor) {
            //SteamVR
            steamVR = true;
        }else if(Application.platform == RuntimePlatform.Android) {
            //XR platform
            XR = true;
        }*/

        #if (!UNITY_ANDROID)
            steamVR = true;
        #else
            XR = true;
        #endif

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsSteamVR() {
        return steamVR;
    }

    public bool IsXR() {
        return XR;
    }
}
