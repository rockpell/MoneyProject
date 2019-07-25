using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private List<Station> nowPath;

    private static InputManager instance;

    public static InputManager getInstance()
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
        nowPath = new List<Station>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNowPath(Station station)
    {
        nowPath.Add(station);
    }

    public void initNowPath()
    {
        nowPath.Clear();
    }
}
