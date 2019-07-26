using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private Station[] neighborStations;
    [SerializeField] int[] neighborDistance;
    [SerializeField] private GameObject workerSpace;
    private Resource hasResource;
    private List<Worker> workers;

    private int price;
    private bool isSeaprot;
    [SerializeField] private float workerSpaceRange;

    public void addWorker(Worker worker)
    {
        workers.Add(worker);
    }

    public void deleteWorker(Worker worker)
    {
        workers.Remove(worker);
    }

    void Start()
    {
        workers = new List<Worker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNowPath() // 클릭시 InputManager의 현재 경로에 this 추가하는 함수
    {
        InputManager.getInstance().addNowPath(this);
    }

    public int getWorkerCount() { return workers.Count; }
    public Vector3 getWorkerSpace() { return workerSpace.transform.position; }
    public float getWorkerSpaceRange() { return workerSpaceRange; }

    public int calNeighborDistance(Station station)
    {
        int _result = -1;

        for(int i = 0; i < neighborStations.Length; i++)
        {
            if (neighborStations[i] == station)
            {
                _result = neighborDistance[i];
            }
                
        }

        return _result;
    }
}
