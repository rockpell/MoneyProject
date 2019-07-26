using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text profitValue;
    [SerializeField] private UnityEngine.UI.Text disbursmentValue;
    [SerializeField] private UnityEngine.UI.Text enlightmentValue;
    [SerializeField] private UnityEngine.UI.Text dangerValue;
    [SerializeField] private UnityEngine.UI.Text trustValue;

    void Start()
    {
        UpdateValue();
    }

    public void UpdateValue()
    {
        profitValue.text = GameManager.getInstance().Profit.ToString();
        disbursmentValue.text = GameManager.getInstance().Disbursment.ToString();
        enlightmentValue.text = GameManager.getInstance().EnlightenmentLevel.ToString();
        dangerValue.text = GameManager.getInstance().DangerLevel.ToString();
        trustValue.text = GameManager.getInstance().TrustLevel.ToString();
    }
}
