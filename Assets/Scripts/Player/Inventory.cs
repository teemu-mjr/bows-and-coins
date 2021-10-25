using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    public void AddCoin(int amount)
    {
        Player.stats.coins += amount;
        coinText.text = $"{Player.stats.coins.ToString()}$";
    }
}
