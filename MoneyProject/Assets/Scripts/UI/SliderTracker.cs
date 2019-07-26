using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderTracker : MonoBehaviour
{
    [SerializeField] private GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void refreshValue()
    {
        text.GetComponent<Text>().text = GetComponent<Slider>().value.ToString();
    }
}
