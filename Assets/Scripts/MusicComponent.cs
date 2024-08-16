using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicComponent : MonoBehaviour
{
    public AudioClip Music;

    // Start is called before the first frame update
    void Start()
    {
        if (Music != null)
        {
            GameObject go = new GameObject();
            AudioSource MusicSoundSource = go.AddComponent<AudioSource>();
            MusicSoundSource.loop = true;
            MusicSoundSource.transform.localPosition = transform.position;
            MusicSoundSource.clip = Music;
            MusicSoundSource.Play();
            //Destroy(go, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
