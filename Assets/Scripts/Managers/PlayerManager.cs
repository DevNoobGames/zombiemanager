using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public float money = 1000;
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        moneyText.text = "$" + money.ToString();
    }

    public void ChangeMoney(float change)
    {
        money += change;
        moneyText.text = "$" + money.ToString();
    }
}
