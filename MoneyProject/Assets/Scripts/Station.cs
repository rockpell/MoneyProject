using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private Station[] neighborStations;
    [SerializeField] int[] neighborDistance;

    [SerializeField] private GameObject workerSpace;
    [SerializeField] private List<Worker> workers;

    [SerializeField] private Resource sellingResource;
    [SerializeField] private Resource[] purchaseResources;
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
        purchaseResources = new Resource[2];
        workers = new List<Worker>();
        switch (Random.Range(0, 3))
        {
            case 0:
                sellingResource = new Resource(RTYPE.FABRIC);
                sellingResource.price = 230;

                purchaseResources[0] = new Resource(RTYPE.GRAIN);
                purchaseResources[0].price = 200;
                purchaseResources[1] = new Resource(RTYPE.SEAFOOD);
                purchaseResources[1].price = 170;
                break;
            case 1:
                sellingResource = new Resource(RTYPE.GRAIN);
                sellingResource.price = 200;

                purchaseResources[0] = new Resource(RTYPE.FABRIC);
                purchaseResources[0].price = 230;
                purchaseResources[1] = new Resource(RTYPE.SEAFOOD);
                purchaseResources[1].price = 170;
                break;
            case 2:
                sellingResource = new Resource(RTYPE.SEAFOOD);
                sellingResource.price = 170;

                purchaseResources[0] = new Resource(RTYPE.GRAIN);
                purchaseResources[0].price = 200;
                purchaseResources[1] = new Resource(RTYPE.FABRIC);
                purchaseResources[1].price = 230;
                break;
            default:
                sellingResource = new Resource(RTYPE.GRAIN);
                sellingResource.price = 200;

                purchaseResources[0] = new Resource(RTYPE.FABRIC);
                purchaseResources[0].price = 230;
                purchaseResources[1] = new Resource(RTYPE.SEAFOOD);
                purchaseResources[1].price = 170;
                break;
        }
        sellingResource.count = Random.Range(100, 1000);
        purchaseResources[0].count = Random.Range(100, 1000);
        purchaseResources[1].count = Random.Range(100, 1000);
        priceChanger = new PriceChanger();
    }

    // Update is called once per frame
    void Update()
    {
        if(workers.Count == 0)
        {
            GetComponent<SpriteRenderer>().sprite = GameManager.getInstance().getIdleStationImage();
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = GameManager.getInstance().getWorkerStationImage();
        }
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
        get { return sellingResource; }
    }
    public Resource[] GetResources
    {
        get { return purchaseResources; }
    }

    void OnMouseOver()
    {
        InputManager.getInstance().showSellInformation(purchaseResources, Input.mousePosition);
    }

    private void OnMouseExit()
    {
        InputManager.getInstance().disappearSellInformation();
    }
}
