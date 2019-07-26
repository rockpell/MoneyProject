using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private Station[] neighborStations;
    [SerializeField] int[] neighborDistance;

    [SerializeField] private GameObject workerSpace;
    [SerializeField] private List<Worker> workers;

    [SerializeField] private Resource hasResource;
    [SerializeField] private int price;
    [SerializeField] private float workerSpaceRange;

    private PriceChanger priceChanger;

    private bool isSeaprot;
    public void addWorker(Worker worker)
    {
        workers.Add(worker);
    }

    public void deleteWorker(Worker worker)
    {
        workers.Remove(worker);
    }

    void Awake()
    {
        workers = new List<Worker>();
        switch (Random.Range(0, 3))
        {
            case 0:
                hasResource = new Resource(RTYPE.FABRIC);
                hasResource.price = 230;
                break;
            case 1:
                hasResource = new Resource(RTYPE.GRAIN);
                hasResource.price = 200;
                break;
            case 2:
                hasResource = new Resource(RTYPE.SEAFOOD);
                hasResource.price = 170;
                break;
            default:
                hasResource = new Resource(RTYPE.GRAIN);
                hasResource.price = 200;
                break;
        }
        hasResource.count = Random.Range(100, 1000);
        priceChanger = new PriceChanger();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatePosition()
    {
        for(int i = 0; i < workers.Count; i++)
        {
            workers[i].transform.position = getWorkerSpace() + (i * new Vector3(getWorkerSpaceRange(), 0, 0));
        }
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

    public bool IsConnectStation(Station target)
    {
        for (int i = 0; i < neighborStations.Length; i++)
        {
            if (neighborStations[i] == target)
                return true;
        }
        return false;
    }
    public Station[] getNeighbor() { return neighborStations; }
    public int Price
    {
        get { return price; }
        set { price = value; }
    }
    public Resource GetResource
    {
        get { return hasResource; }
    }
}
