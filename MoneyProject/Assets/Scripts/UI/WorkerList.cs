using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerList : MonoBehaviour
{
    // Start is called before the first frame update
    private Worker basicWorker;
    private List<Worker> workers;
    [SerializeField] Station busan;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void employWorker()
    {
        int busanWorker = busan.getWorkerCount();
        workers.Add(Instantiate(basicWorker, busan.getWorkerSpace() + (busanWorker * new Vector3(0.3f,0,0)), Quaternion.identity));
    }
    void fireWorker(Worker worker)
    {
        workers.Remove(worker);
    }
    void updateWorkerList()
    {
        
    }
}
