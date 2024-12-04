using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rabbits : MonoBehaviour
{
    public int Num { get; set; }
    
    public CommonFunc Cm { get; set; }

    public GameObject[] RabbitsHelp { get; set; }
    
    public Transform rabbitHelp;

    public GameManager gm;
    public WindowManager wm;
    public player_dance player;

    public Button passBtn;

    private void Awake()
    {
        Cm = gm.loader.cm;
        Num = -1;
        RabbitsHelp = new GameObject[Cm.SizeOfRabbit];
        for (int i = 0; i < Cm.SizeOfRabbit; i++)
            RabbitsHelp[i] = rabbitHelp.Find(Cm.SbConcat(new string[] { "Help", (i + 1).ToString() })).gameObject;
    }

    public void SetNum(int num)
    {
        wm.SetWindowName_Rabbit(num);
        if (Num != num)
        {
            Num = num;
            wm.LoadRabbitInfo(Num);
            gm.SetWindowUI(Num);
        }
    }

    public void clicked()
    {
        gm.GiveCarrot(Num);
        if (gm.MaxLevel(Num))
        {
            RabbitsHelp[Num].SetActive(true);
            wm.PassiveDescActive(Num);
            player.Counter();
            passBtn.onClick.Invoke();
            gm.RabbitResult(Num);
            gm.PlaySound(GameManager.Sounds.se_fullCarrot, GameManager.Channels.SE);
        }
    }
}
