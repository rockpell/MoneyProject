using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] Station[] neighborStations;
    private int workerCount;
    [SerializeField] GameObject workerSpace;

    public void increaseWorker()
    {
        workerCount++;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getWorkerCount() { return workerCount; }
    public Vector3 getWorkerSpace() { return workerSpace.transform.position; }
}
