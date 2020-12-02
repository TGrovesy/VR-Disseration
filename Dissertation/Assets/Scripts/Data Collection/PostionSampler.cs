using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostionSampler : MonoBehaviour
{
    //TODO Collect Locomotion type!
    //Data Collection
    struct Sample {
        public Sample(Transform tran, float ti) {
            t = tran;
            position = tran.position;
            time = ti;
        }

        public Transform t { get; }

        public Vector3 position { get; }
        public float time { get; }

        public override string ToString() {
            string value = "";
            value += "X: " + position.x + ", Y: " + position.y + ", Z: " + position.z + ", Time: " + time + "S";
            return value;
        }
    }

    //List of samples
    private List<Sample> samples = new List<Sample>();


    //Object of interest
    public GameObject targetObj;

    public float sampleRate = 3.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= sampleRate) {
            //reset timer
            timer = 0.0f;
            //Log positon
            Transform tran = targetObj.transform;
            //time
            float currentTime = AssetManager.GetPlayTime();
            samples.Add(new Sample(tran, currentTime));
        }
    }

    public void PrintLog() {
        foreach(Sample sample in samples) {
            Debug.Log(sample.ToString());
        }
    }

}
