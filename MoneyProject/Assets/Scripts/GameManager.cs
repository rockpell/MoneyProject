using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IObserverSubject
{
    //[SerializeField] Text moenyText;
    //[SerializeField] Text fabricText;
    //[SerializeField] Text seafoodText;
    //[SerializeField] Text grainText;
    [SerializeField] Text[] resourceStatus;
    [SerializeField] Slider[] statusSlider;
    [SerializeField] Text priceCountText;
    [SerializeField] Text calendarText;

    [SerializeField] GameObject resultPanel;

    private bool isTouchable = true;

    private int enlightenmentLevel;
    private int dangerLevel;
    private int trustLevel;

    private int turnCount;
    private int priceChangeLeftTurn; // 시세 변동까지 남은 시간
    [SerializeField] private int priceChangeTurnCount = 3; // 시세 변동 주기

    private int taxAndSalaryDayLeftTurn;
    [SerializeField] private int taxAndSalaryDayCount = 4;
    [SerializeField] private int salaryAmount = 500;

    private Calendar calendar;

    private int money = 4000;
    private int profit = 0;
    private int disbursment = 0;
    private Resource[] resource; // 분리하는게 나을지도?

    private List<Worker> workers;

    Slider buyChoiceSlider;

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
        workers = GameObject.Find("WorkerListBtn").GetComponent<WorkerList>().getWorkers();

        calendar = new Calendar();

        priceChangeLeftTurn = priceChangeTurnCount;
        taxAndSalaryDayLeftTurn = taxAndSalaryDayCount;

        buyChoiceSlider = InputManager.getInstance().getBuyChoiceMenu().transform.GetChild(1).GetComponent<Slider>();
    }

    List<Observer> observerList = new List<Observer>();
    public void Notify()
    {
        Debug.Log("Notify!");
        for (int i = 0; i < observerList.Count; i++)
        {
            Debug.Log("observer notifying");
            observerList[i].OnNotify();
        }
    }
    public void AddObserver(Observer ob)
    {
        observerList.Add(ob);
    }
    public void RemoveObserver(Observer ob)
    {
        observerList.Remove(ob);
    }
    // Update is called once per frame
    void Update()
    {
        refreshUI();
    }

    public void nextTurn()
    {
        ++turnCount;
        --priceChangeLeftTurn;
        --taxAndSalaryDayLeftTurn;
        calendar.nextDay();

        if (priceChangeLeftTurn <= 0)
        {
            priceChange();
        }
        if(taxAndSalaryDayLeftTurn <= 0)
        {
            taxAndSalary();
        }
        foreach (Worker worker in workers)
        {
            worker.progressTurn();
        }

        if(enlightenmentLevel > 60)
        {
            EndGame.getInstance().selectEnding(ENDING.ENLIGHTMENT);
            Debug.Log("ENDING.ENLIGHTMENT");
        }
        else if(dangerLevel > 50)
        {
            EndGame.getInstance().selectEnding(ENDING.DANGER);
            Debug.Log("ENDING.DANGER");
        }

        resultPanel.SetActive(true);
        resultPanel.GetComponent<ResultPanel>().UpdateValue();
        IsTouchable = false;
        Invoke("disappearResultPanel", 2f);
    }

    private void disappearResultPanel()
    {
        resultPanel.SetActive(false);
        IsTouchable = true;
    }

    private void taxAndSalary()
    {
        int originMoney = money;
        money -= workers.Count * salaryAmount;
        if(originMoney*0.1f > money)
        {
            EndGame.getInstance().selectEnding(ENDING.BANKRUPTCY);
            Debug.Log("ENDING.BANKRUPTCY");
        }
        else
        {
            money -= (int)(originMoney*0.1f);
        }
        taxAndSalaryDayLeftTurn = taxAndSalaryDayCount;
    }
    private void priceChange() // 가격 변동 적용 해줘야함
    {
        priceChangeLeftTurn = priceChangeTurnCount;
        InputManager.getInstance().showMessage("시세가 변동됩니다.");
        Notify();
    }

    private void refreshUI()
    {
        //moenyText.text = money.ToString();
        //fabricText.text = money.ToString();
        //seafoodText.text = money.ToString();
        //grainText.text = money.ToString();
        Worker selectWorker = GameObject.Find("WorkerListBtn").GetComponent<WorkerList>().getSelectWorker();
        if(selectWorker != null)
        {
            resource = selectWorker.GetResources;

            resourceStatus[0].text = money.ToString(); // 돈
            
            foreach(Resource hasWorker in resource)
            {
                switch(hasWorker.rType)
                {
                    case RTYPE.FABRIC:
                        resourceStatus[3].text = hasWorker.count.ToString();
                        break;
                    case RTYPE.GRAIN:
                            resourceStatus[1].text = hasWorker.count.ToString();
                        break;
                    case RTYPE.SEAFOOD:
                        resourceStatus[2].text = hasWorker.count.ToString();
                        break;
                }
            }
        }
        else
        {
            resourceStatus[0].text = money.ToString(); // 돈
            resourceStatus[1].text = "0"; // 농산물
            resourceStatus[2].text = "0"; // 해산물
            resourceStatus[3].text = "0"; // 면직물
        }
        statusSlider[0].value = enlightenmentLevel;
        statusSlider[1].value = dangerLevel;
        statusSlider[2].value = trustLevel;

        priceCountText.text = priceChangeLeftTurn.ToString() + "턴 후 시세 변동";

        calendarText.text = calendar.year.ToString() + "." + calendar.month.ToString() + "." + calendar.day.ToString();

        if(trustLevel < 0)
        {
            buyChoiceSlider.maxValue = 1;
        }
        else if(trustLevel < 10)
        {
            buyChoiceSlider.maxValue = 10;
        }
        else
        {
            buyChoiceSlider.maxValue = trustLevel;
        }
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
        get { return trustLevel; }
        set { trustLevel = value; }
    }

    public int TurnCount
    {
        get { return turnCount; }
        set { turnCount = value; }
    }

    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    public bool IsTouchable
    {
        get { return isTouchable; }
        set { isTouchable = value; }
    }

    public int Profit
    {
        get { return profit; }
        set { profit = value; }
    }

    public int Disbursment
    {
        get { return disbursment; }
        set { disbursment = value; }
    }
}
