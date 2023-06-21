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
    private void Awake()
    {
        SaveSystem.LoadVolume();
    }
    void Start()
    {
        m_Slider.value = GameManager.Volume;
    }
    public void VolumeVariable()
    {
        m_AudioSource.volume = m_Slider.value;
        GameManager.Volume = m_AudioSource.volume;
        SaveSystem.SaveVolume();
    }
}
