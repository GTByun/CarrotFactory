using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int carrot;
    public int Carrot
    {
        get => carrot;
        set
        {
            scoretext.text = value.ToString();
            carrot = value;
        }
    }
    private float cool_time;
    public float Cool_time
    {
        get => cool_time;
        set
        {
            float instant = Cm.Ceil(value);
            if (instant != Count_Cool)
            {
                cooltimetext.text = CoolDesc[(int)instant - 1];
                Count_Cool = instant;
            }
            cool_time = value;
        }
    }
    public float Count_Cool { get; set; }

    public int[] Level { get; set; }
    public int[] NeedCarrot { get; set; }
    public string[] CoolDesc { get; set; }

    public CommonFunc Cm { get; set; }

    public Dictionary<string, SoundEmitter> SoundManager { get; set; }
    
    public enum Sounds
    {
        se_factory,
        se_getCarrot,
        se_giveCarrot,
        se_fullCarrot,
        se_btnclick,
        se_btnclose,
        se_house,
        main_bgm,
        clear_bgm
    }
    public enum Channels
    {
        BGM,
        SE
    }

    public int get_carrot;
    public float cool_time_seconds;

    public CmLoader loader;
    public WindowManager wm;

    public SoundEmitter[] soundEmitters;

    public TextMeshProUGUI scoretext, cooltimetext;

    public GameObject newcarrot;

    public void RabbitResult(int num)
    {
        switch (num)
        {
            case 0:
                get_carrot *= 2;
                break;
            case 1:
                cool_time_seconds *= 0.8f;
                break;
            case 2:
                get_carrot *= 3;
                break;
            case 3:
                cool_time_seconds *= 0.6f;
                break;
            case 4:
                get_carrot *= 4;
                break;
            default:
                break;
        }
    }

    public void PlaySound(Sounds key, Channels channel)
    {
        SoundManager[channel.ToString()].PlaySound(key.ToString());
    }

    public void FactoryClick()
    {
        Cool_time -= 1; //공장을 클릭하면 cool_time에 1을 더함
        PlaySound(Sounds.se_factory, Channels.SE);
    }

    public void SetWindowUI(int num)
    {
        wm.SetProgressImage(Level[num]);
        wm.SetGiveButton_Interactable(Level[num] != 10);
    }

    public void GiveCarrot(int num)
    {
        if (Carrot >= NeedCarrot[num])
        {
            Carrot -= NeedCarrot[num];
            Level[num]++;
            SetWindowUI(num);
            PlaySound(Sounds.se_giveCarrot, Channels.SE);
        }
    }

    public bool MaxLevel(int num)
    {
        return Level[num] == 10;
    }

    private void Awake()
    {
        Carrot = 10000;
        Cm = loader.cm;
        CoolDesc = new string[(int)Cm.Ceil(cool_time_seconds)];
        for (int i = 0; i < CoolDesc.Length; i++)
            CoolDesc[i] = Cm.SbConcat(new string[] { "당근 재배까지 ", (i + 1).ToString(), "초" });
        Count_Cool = 0.0f;
        Cool_time = cool_time_seconds;
        Level = new int[Cm.SizeOfRabbit];
        for (int i = 0; i < Cm.SizeOfRabbit; i++)
            Level[i] = 0;
        NeedCarrot = Cm.NeedCarrot;
        SoundManager = new Dictionary<string, SoundEmitter>();
        for (int i = 0; i < soundEmitters.Length; i++)
        {
            SoundEmitter soundEmitter = soundEmitters[i];
            SoundManager.Add(soundEmitter.channelName, soundEmitter);
        }
        PlaySound(Sounds.main_bgm, Channels.BGM);
    }

    private void Update()
    {
        Cool_time -= Time.deltaTime;
        if (Cool_time <= 0)
        {
            Carrot += get_carrot;
            Cool_time = cool_time_seconds;
            newcarrot.SetActive(true);
            PlaySound(Sounds.se_getCarrot, Channels.SE);
        }
    }
}
