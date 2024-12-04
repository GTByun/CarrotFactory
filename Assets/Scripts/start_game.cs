using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_game : MonoBehaviour
{
    private Vector3 target;
    public Vector3 Target { get => target; set => target = value; }
    private Vector3 goal;
    public Vector3 Goal { get => goal; set => goal = value; }

    public float speed; //�̵� �ӵ�

    public GameManager gm;
    public Factory factory;

    public GameObject Intro_game_Text, Intro_game, Main_game;
    //Ŭ���ϸ� ��������� ��ġ����ŸƮ
    //���ΰ���ȭ�鿡 �����ϸ� ������ ��Ʈ�� ȭ��
    //�����ϸ� �Ⱥ��̰��� ���ΰ��� UI

    private void Awake()
    {
        Target = new Vector3(0, -2, -10); //������ġ
        Goal = new Vector3(0, 0, -12);
    }

    private void Start()
    {
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitUntil(() => Input.GetMouseButton(0)); //Ŭ���ϸ� �������� �����
        factory.SetCollider(false);
        Destroy(Intro_game_Text); //�������� ������� ��Ʈ�� �ؽ�Ʈ�� ������
        while (transform.position.y > 0) // ���� y��ġ�� 0���� �۰ų� ũ�� �������� ������� ����
        {
            transform.position = Vector3.Lerp(transform.position, Target, speed * Time.deltaTime); //������ġ���� target ��ġ���� 1f �ӵ��� ������
            yield return null;
        }
        transform.position = Goal;
        Main_game.SetActive(true); //���ΰ��� UI�� �ٽ� �����ְ�
        Destroy(Intro_game);  //��Ʈ�θ� ������
        factory.SetCollider(true); //���� Ŭ���� �����
        gm.enabled = true;
        Destroy(GetComponent<start_game>());
    }
}
