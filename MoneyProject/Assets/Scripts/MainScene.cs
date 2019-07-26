using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("Prolog");
    }

    private void activeButton()
    {
        startButton.SetActive(true);
    }
}
