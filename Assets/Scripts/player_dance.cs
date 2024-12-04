using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_dance : MonoBehaviour
{
    public int Count { get; set; }
    public CommonFunc cm { get; set; }

    public GameManager gm;

    private void Awake()
    {
        Count = 0;
        cm = gm.loader.cm;
    }

    public void Counter()
    {
        Count++;
        if (Count == cm.SizeOfRabbit)
        {
            GetComponent<Animator>().SetTrigger("Dance");
            gm.PlaySound(GameManager.Sounds.clear_bgm, GameManager.Channels.BGM);
            Destroy(GetComponent<player_dance>());
        }
    }
}
