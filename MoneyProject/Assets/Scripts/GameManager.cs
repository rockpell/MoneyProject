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
    [SerializeField] Text priceCountText;
    [SerializeField] Text calendarText;

    private int enlightenmentLevel;
    private int dangerLevel;
    private int trustLevel;

    private int turnCount;
    private int priceChangeLeftTurn; // 시세 변동까지 남은 시간
    [SerializeField] private int priceChangeTurnCount = 3; // 시세 변동 주기

    private Calendar calendar;

    private int money;
    private Resource[] resource; // 분리하는게 나을지도?

    private List<Worker> workers;

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
        resource[0] = new Resource(RTYPE.GRAIN);
        resource[1] = new Resource(RTYPE.SEAFOOD);
        resource[2] = new Resource(RTYPE.FABRIC);

        workers = GameObject.Find("WorkerListBtn").GetComponent<WorkerList>().getWorkers();

        calendar = new Calendar();
    }

    // Update is called once per frame
    void Update()
    {
        //refreshUI();
    }

    public void nextTurn()
    {
        ++turnCount;
        --priceChangeLeftTurn;
        calendar.nextDay();

        if (priceChangeLeftTurn <= 0)
        {
            priceChange();
        }

        foreach (Worker worker in workers)
        {
            worker.progressTurn();
        }
    }

    private void priceChange() // 가격 변동 적용 해줘야함
    {
        priceChangeLeftTurn = priceChangeTurnCount;
    }

    private void refreshUI()
    {
        //moenyText.text = money.ToString();
        //fabricText.text = money.ToString();
        //seafoodText.text = money.ToString();
        //grainText.text = money.ToString();

        resourceStatus[0].text = money.ToString(); // 돈
        resourceStatus[1].text = resource[0].ToString(); // 농산물
        resourceStatus[2].text = resource[1].ToString(); // 해산물
        resourceStatus[3].text = resource[2].ToString(); // 면직물

        statusSlider[0].value = enlightenmentLevel;
        statusSlider[1].value = dangerLevel;
        statusSlider[2].value = trustLevel;

        priceCountText.text = priceChangeLeftTurn.ToString() + "턴 후 시세 변동";

        calendarText.text = calendar.year.ToString() + "." + calendar.month.ToString() + "." + calendar.day.ToString();
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
