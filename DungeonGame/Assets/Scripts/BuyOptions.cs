using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyOptions : MonoBehaviour
{
    private GeneratorOptions gOptions;

    private void Start()
    {
        gOptions = gameObject.GetComponent<GeneratorOptions>();
    }

    public void IncreaseDamage()
    {
        Debug.Log("Coins: " + Coin.totalCoins + " - increase: " + gOptions.increaseDamageCosts);
        if (Coin.totalCoins >= gOptions.increaseDamageCosts)
        {
            Coin.totalCoins -= gOptions.increaseDamageCosts;
            gOptions.playerDamage += 3;
            Coin.totalCoins -= gOptions.increaseDamageCosts;
        }
    }
}
