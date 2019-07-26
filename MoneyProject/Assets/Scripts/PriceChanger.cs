using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceChanger : MonoBehaviour, Observer
{
    private int[] originalPrices;
    [SerializeField] float changeValue;
    [SerializeField] Station station;

    public void OnNotify()
    {
        station.Price = Random.Range((int)(originalPrices[0] * (1 - changeValue)), (int)(originalPrices[0] * (1 + changeValue)));
        station.GetResource.price = station.Price;

        station.GetResources[0].price = Random.Range((int)(originalPrices[1] * (1 - changeValue)), (int)(originalPrices[1] * (1 + changeValue)));
        station.GetResources[1].price = Random.Range((int)(originalPrices[2] * (1 - changeValue)), (int)(originalPrices[2] * (1 + changeValue)));
    }
    void Start()
    {
        originalPrices = new int[3];
        GameManager.getInstance().AddObserver(this);
        originalPrices[0] = station.GetResource.price;
        originalPrices[1] = station.GetResources[0].price;
        originalPrices[2] = station.GetResources[1].price;
    }
}
