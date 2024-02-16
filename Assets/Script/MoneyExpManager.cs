using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyExpManager : MonoBehaviour
{
    public int money;
    public int exp;
    public Text Money;
    public Text Exp;
    
    public void setMoneyText(string txt)
    {
        if (Money)
        {
            Money.text = txt;
        }
    }
    public void setExpText(string txt)
    {
        if (Exp)
        {
            Exp.text = txt;
        }
    }
    void Start()
    {
        money = 175;
        exp = 0;
    }

    void Update()
    {
        setMoneyText(Convert.ToString(money));
        setExpText(Convert.ToString(exp));
    }
}
