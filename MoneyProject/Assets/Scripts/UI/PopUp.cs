﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{

    [SerializeField] GameObject PopUpPanel;
    void Start()
    {
        
    }

    public void clickUI()
    {
        if (PopUpPanel.activeInHierarchy)
            PopUpPanel.SetActive(false);
        else
            PopUpPanel.SetActive(true);
    }
}
