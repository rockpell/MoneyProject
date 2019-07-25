using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerList : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Worker basicWorker;
    [SerializeField] private List<Worker> workers;
    [SerializeField] Station busan;
    [SerializeField] float StationWorkerRange;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void employWorker()
    {
        int busanWorker = busan.getWorkerCount();
        workers.Add(Instantiate(basicWorker, busan.getWorkerSpace() + (busanWorker * new Vector3(StationWorkerRange,0,0)), Quaternion.identity));
        busan.increaseWorker();
    }
    public void fireWorker(Worker worker)
    {
        workers.Remove(worker);
    }
    public void updateWorkerList()
    {
        
    }
}
