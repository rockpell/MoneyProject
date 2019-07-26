using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMonitor : MonoBehaviour
{
    [SerializeField] private Station station;
    [SerializeField] private Resource resource;
    [SerializeField] private Sprite[] resourceIcon;

    [SerializeField] private UnityEngine.UI.Text text;
    private int resourceValue;

    void Start()
    {
        resource = station.GetResource;
        switch(resource.rType)
        {
            case RTYPE.FABRIC:
                this.GetComponent<SpriteRenderer>().sprite = resourceIcon[0];
                break;
            case RTYPE.GRAIN:
                this.GetComponent<SpriteRenderer>().sprite = resourceIcon[1];
                break;
            case RTYPE.SEAFOOD:
                this.GetComponent<SpriteRenderer>().sprite = resourceIcon[2];
                break;
            default:
                break;
        }
        text.text = resource.price.ToString();
    }

    public void UpdateValue()
    {
        resourceValue = station.Price;
    }
    void Update()
    {
        
    }
}
