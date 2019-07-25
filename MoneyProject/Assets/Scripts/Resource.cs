using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public RTYPE rType;
    public int price;
    public int count;

    public Resource(RTYPE rType)
    {
        this.rType = rType;
    }
}
