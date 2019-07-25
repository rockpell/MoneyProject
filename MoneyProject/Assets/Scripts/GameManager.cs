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
        resource = new Resource[3];
        resource[0] = new Resource(RTYPE.FABRIC);
        resource[1] = new Resource(RTYPE.GRAIN);
        resource[2] = new Resource(RTYPE.SEAFOOD);
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
