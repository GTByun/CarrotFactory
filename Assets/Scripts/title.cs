using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class title : MonoBehaviour
{
    float S_time;

    private void Update()
    {
        S_time += Time.deltaTime;
        if (S_time > 2)
            SceneManager.LoadScene("Main");
    }
}
