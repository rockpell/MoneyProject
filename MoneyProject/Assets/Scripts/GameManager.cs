using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //[SerializeField] Text moenyText;
    //[SerializeField] Text fabricText;
    //[SerializeField] Text seafoodText;
    //[SerializeField] Text grainText;
    [SerializeField] Text[] resourceStatus;
    [SerializeField] Slider[] statusSlider;

    private int enlightenmentLevel;
    private int dangerLevel;
    private int trustLevel;
    private int turnCount;
    private int money;
    private Resource[] resource; // 분리하는게 나을지도?

    private static GameManager instance;

    public static GameManager getInstance()
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

    void Start()
    {
        resource = new Resource[3];
        resource[0] = new Resource(RTYPE.FABRIC);
        resource[1] = new Resource(RTYPE.GRAIN);
        resource[2] = new Resource(RTYPE.SEAFOOD);
    }

    // Update is called once per frame
    void Update()
    {
        refreshUI();
    }

    public void nextTurn()
    {

    }

    private void refreshUI()
    {
        //moenyText.text = money.ToString();
        //fabricText.text = money.ToString();
        //seafoodText.text = money.ToString();
        //grainText.text = money.ToString();

        resourceStatus[0].text = money.ToString();
        resourceStatus[1].text = money.ToString();
        resourceStatus[2].text = money.ToString();
        resourceStatus[3].text = money.ToString();

        statusSlider[0].value = enlightenmentLevel;
        statusSlider[1].value = dangerLevel;
        statusSlider[2].value = trustLevel;
    }

    public int EnlightenmentLevel
    {
        get { return enlightenmentLevel; }
        set { enlightenmentLevel = value; }
    }

    public int DangerLevel
    {
        get { return dangerLevel; }
        set { dangerLevel = value; }
    }

    public int TrustLevel
    {
        get { return dangerLevel; }
        set { dangerLevel = value; }
    }

    public int TurnCount
    {
        get { return dangerLevel; }
        set { dangerLevel = value; }
    }

    public int Money
    {
        get { return money; }
        set { money = value; }
    }
}
