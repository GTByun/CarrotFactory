using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitter : MonoBehaviour
{
    public Dictionary<string, AudioClip> KeyNClip { get; set; }

    public AudioSource Audiosource { get; set; }

    public string channelName;

    public AudioClip[] clip;

    private void Awake()
    {
        KeyNClip = new Dictionary<string, AudioClip>();
        for (int i = 0; i < clip.Length; i++)
            KeyNClip.Add(clip[i].name, clip[i]);
        Audiosource = GetComponent<AudioSource>();
    }

    public void PlaySound(string key)
    {
        Audiosource.clip = KeyNClip[key];
        Audiosource.Play();
    }
}
