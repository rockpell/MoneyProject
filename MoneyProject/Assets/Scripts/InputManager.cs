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

    [SerializeField] private float zoomScale = 0.5f;
    [SerializeField] private float dragScale = 0.05f;

    private Worker nowWorker;

    private Vector3 dragPivot;
    private float xMin, xMax;
    private float yMin, yMax;

    private bool isMoveMode = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(checkObject());
        }
        if(Input.GetMouseButtonDown(1))
        {
            dragPivot = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 _drag = (Input.mousePosition - dragPivot) * dragScale * 0.01f;

            Camera.main.transform.position += _drag;
        }

        float x = Mathf.Clamp(Camera.main.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(Camera.main.transform.position.y, yMin, yMax);
        Camera.main.transform.position = new Vector3(x, y, Camera.main.transform.position.z);

        Camera.main.orthographicSize -= Input.mouseScrollDelta.y * zoomScale;
        if (Camera.main.orthographicSize > 5) Camera.main.orthographicSize = 5;
        else if (Camera.main.orthographicSize < 1) Camera.main.orthographicSize = 1;

        //Debug.Log("mode: " + isMoveMode + "   nowWorker: " + nowWorker);
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

                    //selectMenu.transform.position = _checkPosition;
                    //selectMenu.SetActive(true);
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
            }
        }

        if (raycast.collider != null && raycast.collider.tag == "Worker")
        {
            select = raycast.collider.gameObject;
            select.GetComponent<Worker>().selectWorker();

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

        //nowWorker.getNowStation.   ,,, nowstatcion의 품목의 가격을 알아와서 비교하여 돈이 부족할 경우 showmessage
    }

    public void sellResource()
    {
        //if (nowWorker.) hasResource 확인해서 있을 경우 판매
        //{

        //}
        showMessage("물품을 판매하였습니다.");
    }

    public void cancelChioce()
    {

    }

    private void showMessage(string text)
    {
        messageUI.SetActive(true);
        messageUI.transform.GetChild(0).GetComponent<Text>().text = text;
    }

    private void disappearMessage()
    {
        messageUI.SetActive(false);
    }

    private void changeButtonText()
    {
        buyButton.text = "물품 출고";
        sellButton.text = "물품 적재";
    }

    private void initChangeButtonText()
    {
        buyButton.text = "물품 구매";
        sellButton.text = "물품 판매";
    }
}