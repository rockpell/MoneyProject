using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceChanger : MonoBehaviour, Observer
{
    private int originalPrice;
    [SerializeField] float changeValue;
    [SerializeField] Station station;

    public void OnNotify()
    {
        station.Price = Random.Range((int)(originalPrice * (1 - changeValue)), (int)(originalPrice * (1 + changeValue)));
        station.GetResource.price = station.Price;
    }
    void Start()
    {
        GameManager.getInstance().AddObserver(this);
        originalPrice = station.GetResource.price;
    }
}
