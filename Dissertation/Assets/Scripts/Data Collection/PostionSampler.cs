using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    public void CreatePNG() {
        /*
        // Create a texture the size of the screen, RGB24 format
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();
        Object.Destroy(tex);

        // For testing purposes, also write to a file in the project folder
        File.WriteAllBytes("D:/Documents/Uni work/Dissertation/Data/Locomotion/SavedScreen.png", bytes);*/
        /*Bitmap bmp = new Bitmap(50, 50);
        System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);

        //paint the bitmap here with g. …
        //I just fill a recctangle here
        g.FillRectangle(Brushes.Green, 0, 0, 50, 50);

        //dispose and save the file
        g.Dispose();
        bmp.Save("D:/Documents/Uni work/Dissertation/Data/Locomotion/output.PNG", System.Drawing.Imaging.ImageFormat.Png);
        bmp.Dispose();
        */
    }

}
