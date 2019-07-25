using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private List<Station> nowPath;
    private Worker nowWorker;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log(checkObject());
        }
    }

    private GameObject checkObject()
    {
        GameObject select;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D raycast = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (raycast.collider != null && raycast.collider.tag == "Station")
        {
            select = raycast.collider.gameObject;
            if(nowWorker != null)
            {
                select.GetComponent<Station>().addNowPath();
                Debug.Log("Station Add");
            }
                
        }
        else if(raycast.collider != null && raycast.collider.tag == "Worker")
        {
            if(nowWorker != null)
            {
                savePath();
                initNowPath();
                Debug.Log("unSelect Worker");
            }
            select = raycast.collider.gameObject;
            select.GetComponent<Worker>().selectWorker();
        }
        else
        {
            if (nowWorker != null)
            {
                savePath();
                initNowPath();
                Debug.Log("unSelect Worker");
            }
            select = null;
            nowWorker = null;
        }
            

        return select;
    }
    public void addNowPath(Station station)
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
