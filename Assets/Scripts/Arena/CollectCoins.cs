using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    private void Awake()
    {
        ArenaController.OnNextWave += ArenaController_OnNextWave;
    }

    private void ArenaController_OnNextWave(object sender, ArenaController.WaveArgs e)
    {
        int totalValue = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Coin>())
            {
                totalValue += transform.GetChild(i).GetComponent<Coin>().coinValue;
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        Player.stats.AddCoins(Mathf.RoundToInt(totalValue * 0.75f));
    }

    private void OnDisable()
    {
        ArenaController.OnNextWave -= ArenaController_OnNextWave;
    }
}
