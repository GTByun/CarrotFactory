using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_game : MonoBehaviour
{
    private Vector3 target;
    public Vector3 Target { get => target; set => target = value; }
    private Vector3 goal;
    public Vector3 Goal { get => goal; set => goal = value; }

    public float speed; //이동 속도

    public GameManager gm;
    public Factory factory;

    public GameObject Intro_game_Text, Intro_game, Main_game;
    //클릭하면 사라지게할 터치투스타트
    //메인게임화면에 도착하면 제거할 인트로 화면
    //시작하면 안보이게할 메인게임 UI

    private void Awake()
    {
        Target = new Vector3(0, -2, -10); //도착위치
        Goal = new Vector3(0, 0, -12);
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitUntil(() => Input.GetMouseButton(0)); //클릭하면 움직임을 허락함
        factory.SetCollider(false);
        Destroy(Intro_game_Text); //움직임을 허락으면 인트로 텍스트를 가리고
        while (transform.position.y > 0) // 현재 y위치가 0보다 작거나 크면 움직임을 허락하지 않음
        {
            transform.position = Vector3.Lerp(transform.position, Target, speed * Time.deltaTime); //현재위치에서 target 위치까지 1f 속도로 내려감
            yield return null;
        }
        transform.position = Goal;
        Main_game.SetActive(true); //메인게임 UI를 다시 보여주고
        Destroy(Intro_game);  //인트로를 제거함
        factory.SetCollider(true); //공장 클릭을 허락함
        gm.enabled = true;
        Destroy(GetComponent<start_game>());
    }
}
