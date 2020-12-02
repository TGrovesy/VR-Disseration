using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    public GameObject steamVRPlayer;
    public GameObject XRPlayer;

    //Platform checker
    public GameObject platformCheckerObj;
    private PlatformCheck platformChecker;

    // Start is called before the first frame update
    void Start()
    {
        platformChecker = platformCheckerObj.GetComponent<PlatformCheck>();

        if (platformChecker.IsSteamVR()) {
            SpawnSteamVR();
        } else if (platformChecker.IsXR()) {
            SpawnXR();
        } else {
            Debug.Log("Unknown Platform");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnXR() {

        GameObject player = Instantiate(XRPlayer, transform.position, transform.rotation);
        player.transform.parent = GameObject.Find("Player").transform;
    }

    private void SpawnSteamVR() {

        GameObject player = Instantiate(steamVRPlayer, transform.position, transform.rotation);
        player.transform.parent = GameObject.Find("Player").transform;

    }
}
