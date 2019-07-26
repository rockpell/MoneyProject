using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEnd : MonoBehaviour
{
    private List<Worker> workers;
    public void nextTurnMove()
    {
        workers = GameObject.Find("WorkerListBtn").GetComponent<WorkerList>().getWorkers();

        foreach(Worker worker in workers)
        {
            worker.progressTurn();
        }
    }
}
