using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellInformation : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Transform[] resource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sycnResouceInformation(Resource[] sellingResource)
    {
        for(int i = 0; i < sellingResource.Length; i++)
        {
            switch (sellingResource[i].rType)
            {
                case RTYPE.FABRIC:
                    resource[i].GetComponent<Image>().sprite = sprites[0];
                    break;
                case RTYPE.SEAFOOD:
                    resource[i].GetComponent<Image>().sprite = sprites[1];
                    break;
                case RTYPE.GRAIN:
                    resource[i].GetComponent<Image>().sprite = sprites[2];
                    break;
            }
            resource[i].GetChild(0).GetComponent<Text>().text = sellingResource[i].price.ToString();
        }
    }
}
