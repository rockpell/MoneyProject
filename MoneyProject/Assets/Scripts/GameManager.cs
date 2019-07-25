using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextTurn()
    {

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
