using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    [SerializeField] AudioSource music;
    void Start()
    {
        StartCoroutine(waitAndStart());
    }
    IEnumerator waitAndStart()
    {
        yield return new WaitForSeconds(1.5f);
        music.Play();
        StartCoroutine(FadeAudioSource.StartFade(music, 6, 0.8f));
    }
}