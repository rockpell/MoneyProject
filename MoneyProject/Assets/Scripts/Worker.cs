using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private STATUS status;
    [SerializeField] private Sprite ship;
    [SerializeField] private Sprite wagon;

    private Station nowStation; // 초기값 부산으로 설정 필요
    [SerializeField] private List<Station> leftPath;
    private Resource[] hasResources;   //count -> 물건 산 개수

    private int arriveLeftTime; // 남은 턴수

    void Start()
    {
        hasResources = new Resource[3];
        hasResources[0] = new Resource(RTYPE.FABRIC);
        hasResources[1] = new Resource(RTYPE.GRAIN);
        hasResources[2] = new Resource(RTYPE.SEAFOOD);
    }

    void Update()
    {
        changeSprite();
    }

    private void changeSprite()
    {
        if (status == STATUS.WAGON)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = wagon;
        else if (status == STATUS.SHIP)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ship;
    }

    public void setPath(List<Station> stations)
    {
        if((leftPath.Count == 0) || (leftPath[0] != stations[0])) // 새로운 경로일 경우
        {
            arriveLeftTime = nowStation.calNeighborDistance(stations[0]);
        }
        
        leftPath = stations;
    }

    public void initLeftPath()
    {
        leftPath = null;
    }

    public void progressTurn() // 턴 진행시 호출
    {
        --arriveLeftTime;
        
        if(arriveLeftTime < 0)
        {
            if (leftPath.Count != 0)
            {
                foreach (Station neighbor in leftPath[leftPath.Count - 1].getNeighbor())
                {
                    leftPath[leftPath.Count - 1].GetComponent<HighlightPath>().TurnOffPath(leftPath[leftPath.Count - 1].name, neighbor.name);
                }
                moveStation();
                
            }
                
        }
        nowStation.updatePosition();
    }

    public void selectWorker() // 마우스로 클릭시 이 함수 호출, 연결된 경로 하이라이트 효과도 여기서 호출
    {
        InputManager.getInstance().selectWorker(this);
        GameObject.Find("WorkerListBtn").GetComponent<WorkerList>().selectUIWorker(this);
        foreach(Station station in nowStation.getNeighbor())
        {
            Debug.Log("now Station: "+station.name);
            nowStation.GetComponent<HighlightPath>().TurnOnPath(nowStation.name, station.name);
        }
    }

    private void moveStation()
    {
        changeSprite();
        nowStation.deleteWorker(this);
        nowStation = leftPath[0];
        leftPath.RemoveAt(0);
        nowStation.addWorker(this);
        moveWorker();
    }
    private void moveWorker()
    {
        this.gameObject.transform.position = nowStation.getWorkerSpace() + (nowStation.getWorkerCount()*new Vector3(nowStation.getWorkerSpaceRange(), 0, 0));
    }
    public void initNowStation()
    {
        nowStation = GameObject.Find("Busan").GetComponent<Station>();
    }
    public Station getNowStation() { return nowStation; }
    public STATUS getSTATUS() { return status; }
    public Sprite getWagonImage() { return wagon; }
    public Sprite getShipImage() { return ship; }
    public void setShipImage(Sprite value) { ship = value; }
    public int GetResource(RTYPE rTYPE)
    {
        for(int i = 0; i < hasResources.Length; i++)
        {
            if (hasResources[i].rType == rTYPE)
                return hasResources[i].count;

        }
        return 0;
    }
    public Resource[] GetResources
    {
        get { return hasResources; }
    }

}
