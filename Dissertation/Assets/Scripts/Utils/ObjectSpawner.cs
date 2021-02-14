using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    private float xDeviation, zDeviation; //deviation from position
    private float spawnHeight = 0;
    public int numberOfObjs;//Number of objects to spawn
    public List<GameObject> objectsToSelect;


    // Start is called before the first frame update
    void Start()
    {
        xDeviation = gameObject.transform.localScale.x / 2;
        zDeviation = gameObject.transform.localScale.z / 2;
        SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void SpawnObjects() {
        Vector3 position = gameObject.transform.position;
        for(int i = 0; i < numberOfObjs; i++) {
            //generate random position
            float x = Random.Range(position.x - xDeviation, position.x + xDeviation);
            float z = Random.Range(position.z - zDeviation, position.z + zDeviation);
            int objID = Random.Range(0, objectsToSelect.Count);
            GameObject newObj;

            //pick object
            switch (objID) {
                case 0:
                    newObj = objectsToSelect[0];
                    break;
                case 2:
                    newObj = objectsToSelect[1];
                    break;
                default:
                    newObj = objectsToSelect[2];
                    break;
            }
            newObj.transform.SetParent(gameObject.transform);
            //spawn object
            Instantiate(newObj, new Vector3(x, position.y, z ), Quaternion.identity);
        }
    }



}
