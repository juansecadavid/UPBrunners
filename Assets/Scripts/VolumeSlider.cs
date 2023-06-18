using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_AudioSource;
    [SerializeField]
    private Slider m_Slider;
    // Start is called before the first frame update
    void Start()
    {
        m_Slider.value = VolumeManager.volumeValue;
    }

    // Update is called once per frame
    void Update()
    {
        m_AudioSource.volume = m_Slider.value;
        VolumeManager.volumeValue = m_Slider.value;
    }
    public void VolumeVariable()
    {
        
        m_AudioSource.volume = m_Slider.value;
    }
}
