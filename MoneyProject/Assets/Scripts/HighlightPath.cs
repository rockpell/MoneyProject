using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightPath : MonoBehaviour
{
    [SerializeField] private List<GameObject> paths;
    void Start()
    {
        
    }

    public void TurnOnPath(string start, string end)
    {
        paths.Find(x => x.name == (start + "-" + end)).SetActive(true);
    }
    public void TurnOffPath(string start, string end)
    {
        Debug.Log("start"+start);
        Debug.Log("end" + end);
        paths.Find(x => x.name == (start + "-" + end)).SetActive(false);
    }
    void Update()
    {
        
    }
}
