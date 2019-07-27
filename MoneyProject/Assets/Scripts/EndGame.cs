using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    //파산, 의심, 계몽 3가지 엔딩
    [SerializeField] private Sprite[] endingScene;
    [SerializeField] private UnityEngine.UI.Image image;
    [SerializeField] private GameObject endText;
    private static EndGame instance;

    public static EndGame getInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    [ContextMenu("dfdf")]
    public void test()
    {
        selectEnding(ENDING.BANKRUPTCY);
    }

    public void selectEnding(ENDING ending)
    {
        image.gameObject.SetActive(true);
        switch(ending)
        {
            case ENDING.BANKRUPTCY:
                image.sprite = endingScene[0];
                break; 
            case ENDING.DANGER:
                image.sprite = endingScene[1];
                break; 
            case ENDING.ENLIGHTMENT:
                image.sprite = endingScene[2];
                endText.SetActive(true);
                break; 
        }
        image.color = new Color(255, 255, 255, 255);
    }
}
