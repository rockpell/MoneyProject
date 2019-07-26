using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerList : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Worker basicWorker;
    [SerializeField] private List<Worker> workers;

    [SerializeField] private GameObject uiWorker;
    [SerializeField] private List<GameObject> uiWorkers;
    private Worker selectWorker;
    private int uiWorkerCount;

    [SerializeField] Station busan;

    [SerializeField] float StationWorkerRange;

    [SerializeField] GameObject popUpSpace;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void addWorkerListPopUp(Worker worker)
    {
        GameObject instWorker = Instantiate(uiWorker, popUpSpace.transform);
        Vector3 uiWorkerLocalPos = instWorker.GetComponent<RectTransform>().localPosition;
        instWorker.GetComponent<RectTransform>().localPosition = new Vector3(uiWorkerLocalPos.x + uiWorkerCount * uiWorker.GetComponent<RectTransform>().rect.width,
            uiWorkerLocalPos.y, uiWorkerLocalPos.z);
        uiWorkerCount++;
        uiWorkers.Add(instWorker);
    }
    public void employWorker()
    {
        int busanWorker = busan.getWorkerCount();
        workers.Add(Instantiate(basicWorker, busan.getWorkerSpace() + (busanWorker * new Vector3(StationWorkerRange,0,0)), Quaternion.identity));
        busan.increaseWorker();
        addWorkerListPopUp(workers[workers.Count - 1]);
        workers[workers.Count - 1].initNowStation();
    }
    public void selectUIWorker(GameObject worker)
    {
        int index = uiWorkers.IndexOf(worker);
        selectWorker = workers[index];
        Debug.Log("selectWorker: " + selectWorker);
        //하이라이트 표시
    }
    public void fireWorker()
    {
        int index = 0;
        if(selectWorker != null)
        {
            index = workers.IndexOf(selectWorker);
            GameObject.Destroy(selectWorker.gameObject);
            workers.Remove(selectWorker);
            GameObject.Destroy(uiWorkers[index]);
            uiWorkers.Remove(uiWorkers[index]);
            busan.decreaseWorker();
        }
    }
    private void UpdateWorkerPosition()
    {
        for(int i = 0; i < workers.Count; i++)
        {
            workers[i].gameObject.transform.position = workers[i].getNowStation().getWorkerSpace() + (i * new Vector3(StationWorkerRange, 0, 0));
        }
    }
    public void updateWorkerList()
    {
        for(int index = 0; index < workers.Count; index++)
        {
            if (workers[index].getSTATUS() == STATUS.SHIP)
                uiWorkers[index].GetComponent<UnityEngine.UI.Image>().sprite = workers[index].getShipImage();
            else if (workers[index].getSTATUS() == STATUS.WAGON)
                uiWorkers[index].GetComponent < UnityEngine.UI.Image>().sprite = workers[index].getWagonImage();
        }
    }
}
