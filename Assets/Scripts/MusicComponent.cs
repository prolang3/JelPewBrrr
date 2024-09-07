using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicComponent : MonoBehaviour
{
    public AudioClip Music;
    public float Volume = 1f;
    public bool DontPlay = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Music != null && !DontPlay)
        {
            GameObject go = new GameObject();
            AudioSource MusicSoundSource = go.AddComponent<AudioSource>();
            MusicSoundSource.loop = true;
            MusicSoundSource.transform.localPosition = transform.position;
            MusicSoundSource.clip = Music;
            MusicSoundSource.Play();
            MusicSoundSource.volume = Volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
