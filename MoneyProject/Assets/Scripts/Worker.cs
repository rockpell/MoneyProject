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
    private Resource hasResource;

    private int arriveLeftTime; // 남은 턴수

    void Start()
    {
        
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
            moveStation();
        }
    }

    public void selectWorker() // 마우스로 클릭시 이 함수 호출, 연결된 경로 하이라이트 효과도 여기서 호출
    {
        InputManager.getInstance().selectWorker(this);
    }

    private void moveStation()
    {
        changeSprite();

        nowStation = leftPath[0];
        leftPath.RemoveAt(0);
    }
    public void initNowStation()
    {
        nowStation = GameObject.Find("Busan").GetComponent<Station>();
    }
    public Station getNowStation() { return nowStation; }
    public STATUS getSTATUS() { return status; }
    public Sprite getWagonImage() { return wagon; }
    public Sprite getShipImage() { return ship; }
}
