using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellChoiceUI : MonoBehaviour
{
    [SerializeField] Button fabricButton;
    [SerializeField] Button seafoodButton;
    [SerializeField] Button grainButton;

    [SerializeField] Slider slider;

    int fabricCount;
    int seafoodCount;
    int grainCount;

    // Start is called before the first frame update
    void Start()
    {
        fabricButton.interactable = false;
        seafoodButton.interactable = false;
        grainButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sycnHasResource(Worker worker)
    {
        fabricCount = worker.GetResource(RTYPE.FABRIC);
        seafoodCount = worker.GetResource(RTYPE.SEAFOOD);
        grainCount = worker.GetResource(RTYPE.GRAIN);

        if(fabricCount > 0)
        {
            fabricButton.interactable = true;
        }
        if(seafoodCount > 0)
        {
            seafoodButton.interactable = true;
        }
        if(grainCount > 0)
        {
            grainButton.interactable = true;
        }
    }

    public void setSliderFabricMaxValue()
    {
        slider.maxValue = fabricCount;
    }
    public void setSliderSeafoodMaxValue()
    {
        slider.maxValue = seafoodCount;
    }
    public void setSliderGrainMaxValue()
    {
        slider.maxValue = grainCount;
    }
}
