using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WindowManager : MonoBehaviour
{
    public string[] RabbitName { get; set; }
    public string[] CarrotStr { get; set; }
    public string[] DescStr { get; set; }

    private Vector2 initPos;
    public Vector2 InitPos { get => initPos; set => initPos = value; }

    public Vector2[] RabbitPos { get; set; }
    public Vector2[] RabbitSize { get; set; }

    public Hashtable WindowShow { get; set; }
    public Hashtable WindowHide { get; set; }

    public Sprite[] ProgressImage { get; set; }
    public Sprite[] RabbitSpr { get; set; }

    public CommonFunc Cm { get; set; }

    public GameObject[] PassiveDesc { get; set; }

    public RectTransform elementTf, windowBar, anchor;

    public GameManager gm;
    public Factory factory;

    public Image windowBack, progress, rabbit;
    public TextMeshProUGUI windowName, giveBtnText, rabbitPassiveDescText;

    public Button giveBtn;

    public GameObject mainButtons, blackScreen, element, worldWindow, rabbitWindow, passiveWindow;

    private void Awake()
    {
        InitPos = elementTf.anchoredPosition;
        WindowShow = iTween.Hash("delay", 0.01f, "time", 0.3f);
        WindowHide = iTween.Hash("delay", 0.01f, "time", 0.2f,
            "oncomplete", nameof(ResetWindow), "oncompletetarget", gameObject);
        Cm = gm.loader.cm;
        RabbitName = Cm.RabbitName;
        CarrotStr = new string[Cm.SizeOfRabbit];
        DescStr = new string[Cm.SizeOfRabbit];
        for (int i = 0; i < Cm.SizeOfRabbit; i++)
        {
            CarrotStr[i] = Cm.SbConcat(new string[] { "당근주기 - ", Cm.NeedCarrot[i].ToString() });
            DescStr[i] = Cm.SbConcat(new string[] { "호감도 패시브 : ", Cm.Passive[i] });
        }
        RabbitPos = new Vector2[Cm.SizeOfRabbit];
        RabbitSize = new Vector2[Cm.SizeOfRabbit];
        for (int i = 0; i < Cm.SizeOfRabbit; i++)
        {
            RabbitPos[i] = new Vector2(Cm.RabbitX[i], Cm.RabbitY[i]);
            RabbitSize[i] = new Vector2(Cm.RabbitW[i], Cm.RabbitH[i]);
        }
        ProgressImage = new Sprite[11];
        for (int i = 0; i < 11; i++)
            ProgressImage[i] = Resources.Load<Sprite>(Cm.SbConcat(new string[] { "Progress/moom_R-", i.ToString(), "(percent)" }));
        RabbitSpr = new Sprite[Cm.SizeOfRabbit];
        for (int i = 0; i < Cm.SizeOfRabbit; i++)
            RabbitSpr[i] = Resources.Load<Sprite>(Cm.SbConcat(new string[] { "Rabbits/moon_R(", (i + 1).ToString(), ")" }));
        PassiveDesc = new GameObject[Cm.SizeOfRabbit];
        passiveWindow.SetActive(true);
        for (int i = 0; i < PassiveDesc.Length; i++)
        {
            PassiveDesc[i] = GameObject.Find(Cm.SbConcat(new string[] { "Passive (", (i + 1).ToString(), ")" })).transform.Find("Pass_Desc").gameObject;
            PassiveDesc[i].transform.Find("How").GetComponent<TextMeshProUGUI>().text = Cm.Passive[i];
        }
        passiveWindow.SetActive(false);
    }

    public void SetBaseSprite(Sprite sprite)
    {
        windowBack.rectTransform.sizeDelta = sprite.rect.size;
        windowBack.sprite = sprite;
    }

    public void SetBarY(float y)
    {
        windowBar.anchoredPosition = new Vector2(0, y);
    }

    public void SetWindowName(string name)
    {
        windowName.text = name;
    }

    public void SetWindowName_Rabbit(int num)
    {
        SetWindowName(RabbitName[num]);
    }

    public void SetProgressImage(int num)
    {
        progress.sprite = ProgressImage[num];
    }

    public void SetGiveButton_Interactable(bool on)
    {
        giveBtn.interactable = on;
    }

    public void PassiveDescActive(int num)
    {
        PassiveDesc[num].SetActive(true);
    }

    public void LoadRabbitInfo(int num)
    {
        giveBtnText.text = CarrotStr[num];
        rabbitPassiveDescText.text = DescStr[num];
        rabbit.sprite = RabbitSpr[num];
        rabbit.rectTransform.anchoredPosition = RabbitPos[num];
        rabbit.rectTransform.sizeDelta = RabbitSize[num];
    }

    private Hashtable HashAdj_Y(Hashtable hash, float anchorY)
    {
        anchor.anchoredPosition = new Vector2(0, anchorY);
        if (hash.Contains("y"))
            hash.Remove("y");
        hash.Add("y", anchor.position.y);
        return hash;
    }

    private void OpenWindow()
    {
        blackScreen.SetActive(true);
        iTween.MoveTo(element, HashAdj_Y(WindowShow, 0));
        mainButtons.SetActive(false);
        factory.SetCollider(false);
        rabbitWindow.SetActive(false);
        gm.PlaySound(GameManager.Sounds.se_btnclick, GameManager.Channels.SE);
    }

    public void ClickMapBtn()
    {
        OpenWindow();
        worldWindow.SetActive(true);
        passiveWindow.SetActive(false);
    }

    public void ClickPassiveBtn()
    {
        OpenWindow();
        worldWindow.SetActive(false);
        passiveWindow.SetActive(true);
    }

    public void ClickCancleBtn()
    {
        gm.PlaySound(GameManager.Sounds.se_btnclose, GameManager.Channels.SE);
        iTween.MoveTo(element, HashAdj_Y(WindowHide, 1800));
    }

    private void ResetWindow()
    {
        elementTf.anchoredPosition = InitPos;
        blackScreen.SetActive(false);
        factory.SetCollider(true);
        mainButtons.SetActive(true);
    }
}