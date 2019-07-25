﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station : MonoBehaviour
{
    [SerializeField] private Station[] neighborStations;
    [SerializeField] private GameObject workerSpace;

    private int price;
    private int workerCount;
    private bool isSeaprot;

    public void increaseWorker()
    {
        ++workerCount;
    }

    public void decreaseWorker()
    {
        --workerCount;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNowPath() // 클릭시 InputManager의 현재 경로에 this 추가하는 함수
    {
        InputManager.getInstance().addNowPath(this);
    }

    public int getWorkerCount() { return workerCount; }
    public Vector3 getWorkerSpace() { return workerSpace.transform.position; }
}