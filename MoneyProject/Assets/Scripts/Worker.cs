using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField] private STATUS status;
    [SerializeField] private Sprite ship;
    [SerializeField] private Sprite wagon;

    void Start()
    {
        
    }

    void Update()
    {
        changeSprite();
    }

    private void changeSprite()
    {
        Sprite changeSprite;
        if (status == STATUS.WAGON)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = wagon;
        else if (status == STATUS.SHIP)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = ship;
    }
}
