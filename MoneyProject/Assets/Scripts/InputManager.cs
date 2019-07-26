using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Transform[] boundarys; // right, left, upper, bottom
    [SerializeField] private List<Station> nowPath;
    [SerializeField] private float zoomScale = 0.5f;
    [SerializeField] private float dragScale = 0.05f;

    private Worker nowWorker;

    private Vector3 dragPivot;
    private float xMin, xMax;
    private float yMin, yMax;
    

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
    }

    private GameObject checkObject()
    {
        GameObject select;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D raycast = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (raycast.collider != null && raycast.collider.tag == "Station")
        {
            select = raycast.collider.gameObject;
            Station _station = select.GetComponent<Station>();
            if (nowWorker != null)
            {
                if(nowPath.Count == 0)
                {
                    if (_station.IsConnectStation(nowWorker.getNowStation()))
                    {
                        addNowPath(_station);
                        Debug.Log("first Station Add");
                    }
                }
                else
                {
                    if (_station.IsConnectStation(nowPath[nowPath.Count - 1]))
                    {
                        addNowPath(_station);
                        Debug.Log("Station Add");
                    }
                }
            }
        }
        else if(raycast.collider != null && raycast.collider.tag == "Worker")
        {
            if(nowWorker != null)
            {
                if(nowPath.Count != 0)
                {
                    savePath();
                    initNowPath();
                }
                Debug.Log("unSelect Worker");
            }
            select = raycast.collider.gameObject;
            select.GetComponent<Worker>().selectWorker();
        }
        else
        {
            if (nowWorker != null)
            {
                if(nowPath.Count != 0)
                {
                    savePath();
                    initNowPath();
                }
                Debug.Log("unSelect Worker");
            }
            select = null;
            nowWorker = null;
        }

        return select;
    }
    public void addNowPath(Station station) // 연결 가능한 거점인지 확인한 후에 add 해주는 작업 필요
    {
        nowPath.Add(station);
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

}
