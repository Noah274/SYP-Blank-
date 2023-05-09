using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour
{
    Text counterText;

    void Start()
    {
        counterText = GetComponent<Text>();
    }

    void Update()
    {
        if (counterText.text != Coin.totalCoins.ToString())
        {
            counterText.text = Coin.totalCoins.ToString();
        }
    }
}