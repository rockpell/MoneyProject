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
        Debug.Log(paths.Find(x => x.name == start + "-" + end));
    }
    public void TurnOffPath(string start, string end)
    {
        Debug.Log(paths.Find(x => x.name == start + "-" + end));
    }
    void Update()
    {
        
    }
}
