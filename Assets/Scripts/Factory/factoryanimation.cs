using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class factoryanimation : MonoBehaviour
{
    public float ThisTime { get; set; }

    private Vector3 originalSize;
    public Vector3 OriginalSize { get => originalSize; set => originalSize = value; }

    public factoryanimation ThisScript { get; set; }

    public float factory_size, ani_time;

    private void Awake()
    {
        ThisTime = 0;
        OriginalSize = transform.localScale;
        ThisScript = GetComponent<factoryanimation>();
    }

    public void Reseter()
    {
        ThisTime = 0;
    }

    private void Update()
    {
        ThisTime += Time.deltaTime;
        if (ThisTime > ani_time)
        {
            if (ThisTime > ani_time * 2)
            {
                transform.localScale = OriginalSize;
                ThisScript.enabled = false;
            }
            else
                transform.localScale = (1 + (factory_size * ((2 * ani_time) - ThisTime))) * OriginalSize;
        }
        else
            transform.localScale = (1 + (factory_size * ThisTime)) * OriginalSize;
    }
}

