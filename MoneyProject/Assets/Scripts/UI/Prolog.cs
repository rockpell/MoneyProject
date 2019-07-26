using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Prolog : MonoBehaviour
{
    [SerializeField] GameObject startButton;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("activeButton", 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("SampleUI");
    }

    private void activeButton()
    {
        startButton.SetActive(true);
    }
}
