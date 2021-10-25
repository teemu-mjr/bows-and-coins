using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyItems : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        UpdateCoinText();
    }

    public void BuyStat()
    {
        Player.stats.coins -= 10;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinText.text = Player.stats.coins.ToString();
    }
}
