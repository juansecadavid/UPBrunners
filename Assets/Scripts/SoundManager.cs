using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    private AudioClip[] audios;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlaySound(int clip)
    {
        audioSource.PlayOneShot(audios[clip],GameManager.Volume);
    }
}
