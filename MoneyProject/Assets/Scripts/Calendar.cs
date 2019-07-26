using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendar
{
    public int year = 1919;
    public int month = 1;
    public int day = 1;

    
    public void nextDay()
    {
        ++day;
        day = checkMonth(month);
    }

    private int checkMonth(int month)
    {
        int _result = 1;
        switch (month)
        {
            case 1:
                if (day > 30)
                    _result = 1;
                break;
            case 2:
                if (day > 30)
                    _result = 1;
                break;
            case 3:
                if (day > 30)
                    _result = 1;
                break;
            case 4:
                if (day > 30)
                    _result = 1;
                break;
            case 5:
                if (day > 30)
                    _result = 1;
                break;
            case 6:
                if (day > 30)
                    _result = 1;
                break;
            case 7:
                if (day > 30)
                    _result = 1;
                break;
            case 8:
                if (day > 30)
                    _result = 1;
                break;
            case 9:
                if (day > 30)
                    _result = 1;
                break;
            case 10:
                if (day > 30)
                    _result = 1;
                break;
            case 11:
                if (day > 30)
                    _result = 1;
                break;
            case 12:
                if (day > 30)
                    _result = 1;
                break;
        }
        return _result;
    }
}
