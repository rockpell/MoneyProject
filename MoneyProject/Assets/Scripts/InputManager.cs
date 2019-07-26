using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Transform[] boundarys; // right, left, upper, bottom
    [SerializeField] private GameObject selectMenu;
    [SerializeField] private GameObject messageUI;
    [SerializeField] private List<Station> nowPath;

    [SerializeField] private Text buyButton;
    [SerializeField] private Text sellButton;

    [SerializeField] private GameObject buyChoiceMenu;
    [SerializeField] private GameObject sellChoiceMenu;

    [SerializeField] private GameObject sellInformation;

    [SerializeField] private float zoomScale = 0.5f;
    [SerializeField] private float dragScale = 0.05f;

    private Worker nowWorker;
    private Trading trading;

    private RTYPE selectResourceType;

    private Vector3 dragPivot;
    private float xMin, xMax;
    private float yMin, yMax;

    private bool isMoveMode = false;
    private bool isNowBusan = false;

    private int choiceAmount = 1;

    private static InputManager instance;
    public static InputManager getInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        nowPath = new List<Station>();
        xMax = boundarys[0].position.x;
        xMin = boundarys[1].position.x;
        yMax = boundarys[2].position.y;
        yMin = boundarys[3].position.y;

        trading = new Trading();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(GameManager.getInstance().IsTouchable)
                Debug.Log(checkObject());
        }
        if(Input.GetMouseButtonDown(1))
        {
            dragPivot = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 _drag = (Input.mousePosition - dragPivot) * dragScale * 0.3f;
            Camera.main.transform.Translate(_drag);
            dragPivot = Input.mousePosition;
        }

        float x = Mathf.Clamp(Camera.main.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(Camera.main.transform.position.y, yMin, yMax);
        Camera.main.transform.position = new Vector3(x, y, Camera.main.transform.position.z);

        Camera.main.orthographicSize -= Input.mouseScrollDelta.y * zoomScale;
        if (Camera.main.orthographicSize > 5) Camera.main.orthographicSize = 5;
        else if (Camera.main.orthographicSize < 2) Camera.main.orthographicSize = 2;
    }

    private GameObject checkObject()
    {
        GameObject select = null;
        Vector3 _checkPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(_checkPosition);
        RaycastHit2D raycast = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (isMoveMode)
        {
            if(raycast.collider != null)
            {
                if (raycast.collider.tag == "Station")
                {
                    select = raycast.collider.gameObject;
                    Station _station = select.GetComponent<Station>();
                    if (nowWorker != null && isMoveMode)
                    {
                        if (nowPath.Count == 0)
                        {
                            if (nowWorker.getNowStation().IsConnectStation(_station))
                            {
                                addNowPath(_station);
                                Debug.Log("first Station Add");
                            }
                        }
                        else
                        {
                            if (nowPath[nowPath.Count - 1].IsConnectStation(_station))
                            {
                                addNowPath(_station);
                                Debug.Log("Station Add");
                            }
                        }
                    }
                }
                else if (raycast.collider.tag == "Worker")
                {
                    if (nowWorker != null)
                    {
                        if (nowPath.Count != 0)
                        {
                            UpdatePath();
                            savePath();
                            initNowPath();
                        }
                        Debug.Log("reSelect Worker");
                    }
                    select = raycast.collider.gameObject;
                    select.GetComponent<Worker>().selectWorker();
                }
            }
            else
            {
                if (nowWorker != null)
                {
                    if (nowPath.Count != 0)
                    {
                        UpdatePath();
                        savePath();
                        initNowPath();
                    }
                    Debug.Log("unSelect Worker");
                }
                select = null;
                nowWorker = null;
                isMoveMode = false;
                selectMenu.SetActive(false);

                initChangeButtonText();
                isNowBusan = false;
            }
        }

        if (raycast.collider != null && raycast.collider.tag == "Worker")
        {
            select = raycast.collider.gameObject;
            select.GetComponent<Worker>().selectWorker();

            if (nowWorker.getNowStation().transform.name == "Busan")
            {
                changeButtonText();
                isNowBusan = true;
            }

            selectMenu.transform.position = _checkPosition + new Vector3(40, 70, 0);
            selectMenu.SetActive(true);
        }

        return select;
    }
    private void UpdatePath()
    {
        if (nowPath.Count == 0)
        {
            foreach (Station neighbor in nowWorker.getNowStation().getNeighbor())
                nowWorker.getNowStation().GetComponent<HighlightPath>().TurnOffPath(nowWorker.getNowStation().name, neighbor.name);
        }
        else
        {
            foreach (Station neighbor in nowPath[nowPath.Count - 1].getNeighbor())
            {
                nowPath[nowPath.Count - 1].GetComponent<HighlightPath>().TurnOffPath(nowPath[nowPath.Count - 1].name, neighbor.name);
            }
        }
    }
    public void addNowPath(Station station) // 연결 가능한 거점인지 확인한 후에 add 해주는 작업 필요
    {
        UpdatePath();
        nowPath.Add(station);
        foreach (Station neighbor in station.getNeighbor())
        {
            station.GetComponent<HighlightPath>().TurnOnPath(station.name, neighbor.name);
            Debug.Log("하이라이트 추가 중");
        }
    }

    public void initNowPath()
    {
        nowPath.Clear();
    }

    public void savePath()  //거점이 아닌 화면 선택 시 호출됨
    {
        List<Station> stations = new List<Station>(nowPath);
        nowWorker.setPath(stations);
    }
    public void selectWorker(Worker worker)
    {
        nowWorker = worker;
    }

    public void SetMoveMode()
    {
        isMoveMode = true;
        selectMenu.SetActive(false);
    }

    public void buyResource()
    {
        // Worker에게 Resource 사도록 만들어야함
        int _amount = (int)buyChoiceMenu.transform.GetChild(1).GetComponent<Slider>().value; // 입력 개수

        if (isNowBusan) // 부산일 경우 물품 출고
        {
            showMessage("출고 할 물품이 없습니다.");
        }
        else
        {
            if (nowWorker.getNowStation().Price > GameManager.getInstance().Money)
            {
                showMessage("돈이 부족합니다.");
            }
            else
            {
                Resource[] resources = nowWorker.GetResources;
                foreach (Resource _resource in resources)
                {
                    if (_resource.rType == nowWorker.getNowStation().GetResource.rType)
                    {
                        trading.purchaseResource(_resource, nowWorker.getNowStation().GetResource, _amount);
                        disappearBuyChoiceMenu();
                        showMessage(_resource.rType + "을 구매하였습니다." + _amount);
                    }
                }
            }
        }
    }

    public void sellResource()
    {
        //if (nowWorker.) hasResource 확인해서 있을 경우 판매
        //{

        //}
        int _amount = (int)sellChoiceMenu.transform.GetChild(2).GetComponent<Slider>().value; // 입력 개수

        if (isNowBusan)
        {
            showMessage("물품을 적재하였습니다.");
        }
        else
        {
            Resource[] resources = nowWorker.GetResources;
            Resource _workerResource = null, _stationResource = null;
            foreach (Resource _resource in resources)
            {
                if (_resource.rType == selectResourceType)
                {
                    _workerResource = _resource;
                }
            }
            foreach(Resource _resource in nowWorker.getNowStation().GetResources)
            {
                if (_resource.rType == selectResourceType)
                {
                    _stationResource = _resource;
                }
            }

            if (_workerResource != null && _stationResource != null)
            {
                trading.sellingResource(_workerResource, _stationResource, _amount);
                disappearSellChoiceMenu();
                showMessage(_workerResource.rType + "물품을 판매하였습니다." + _amount);
            }
        }
    }
    
    public void showBuyChoiceMenu() // 물품 구매 or 물품 출고 버튼 눌렀을때
    {
        buyChoiceMenu.SetActive(true);
        selectMenu.SetActive(false);
    }

    public void disappearBuyChoiceMenu() // 수량 입력하는 화면에서 취소버튼
    {
        buyChoiceMenu.SetActive(false);
    }

    public void showSellChoiceMenu() // 물품 판매 or 물품 적재 버튼 눌렀을때
    {
        sellChoiceMenu.SetActive(true);
        selectMenu.SetActive(false);

        sellChoiceMenu.GetComponent<SellChoiceUI>().sycnHasResource(nowWorker);
    }

    public void disappearSellChoiceMenu() // 수량 입력하는 화면에서 취소버튼
    {
        sellChoiceMenu.SetActive(false);
    }

    public void showMessage(string text)
    {
        messageUI.SetActive(true);
        messageUI.transform.GetChild(0).GetComponent<Text>().text = text;

        Invoke("disappearMessage", 1.5f);
    }

    private void disappearMessage()
    {
        messageUI.SetActive(false);
    }

    private void changeButtonText()
    {
        buyButton.text = "물품 출고";
        sellButton.text = "물품 적재";

        buyChoiceMenu.transform.GetChild(0).GetComponent<Text>().text = "출고하시겠습니까?";
        sellChoiceMenu.transform.GetChild(0).GetComponent<Text>().text = "적재하시겠습니까?";
    }

    private void initChangeButtonText()
    {
        buyButton.text = "물품 구매";
        sellButton.text = "물품 판매";

        buyChoiceMenu.transform.GetChild(0).GetComponent<Text>().text = "구매하시겠습니까?";
        sellChoiceMenu.transform.GetChild(0).GetComponent<Text>().text = "판매하시겠습니까?";
    }

    public void showSellInformation(Resource[] resource, Vector3 position)
    {
        sellInformation.SetActive(true);
        sellInformation.GetComponent<SellInformation>().sycnResouceInformation(resource);
        sellInformation.transform.position = position;
    }

    public void disappearSellInformation()
    {
        sellInformation.SetActive(false);
    }

    public void setSelectResourceType(RTYPE rTYPE)
    {
        selectResourceType = rTYPE;
    }

    //public RTYPE getSelectResourceType()
    //{
    //    return selectResourceType;
    //}
}