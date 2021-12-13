using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoins : MonoBehaviour
{
    private bool hadCoins;

    // audio
    public AudioClip coinPickUp;
    private PlaySound playSound;

    private void Awake()
    {
        ArenaController.OnNextWave += ArenaController_OnNextWave;

        playSound = GetComponent<PlaySound>();
        playSound = playSound.Init();
    }

    private void ArenaController_OnNextWave(object sender, WaveEventArgs e)
    {
        int totalValue = 0;
        hadCoins = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Coin>())
            {
                totalValue += transform.GetChild(i).GetComponent<Coin>().coinValue;
                Destroy(transform.GetChild(i).gameObject);
                if (!hadCoins)
                {
                    hadCoins = true;
                }
            }

            if (hadCoins)
            {
                playSound.Play(coinPickUp, true);
            }
        }

        Player.stats.AddCoins(Mathf.RoundToInt(totalValue * 0.75f));
    }

    private void OnDisable()
    {
        ArenaController.OnNextWave -= ArenaController_OnNextWave;

        playSound.DestroyAudio();
    }
}
