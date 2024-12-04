using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private Vector3 colliderSize;
    public Vector3 ColliderSize { get => colliderSize; set => colliderSize = value; }

    public BoxCollider BoxCollider { get; set; }

    public factoryanimation Ani { get; set; }
    
    public GameManager gm;

    private void Awake()
    {
        BoxCollider = GetComponent<BoxCollider>();
        Ani = GetComponent<factoryanimation>();
        ColliderSize = BoxCollider.size;
    }

    private void OnMouseDown()
    {
        gm.FactoryClick();
        Ani.enabled = true;
        Ani.Reseter();
    }

    public void SetCollider(bool isOn)
    {
        BoxCollider.size = isOn ? ColliderSize : Vector3.zero;
    }
}
