using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem 
{
    public static void LoadGame()
    {
        if(PlayerPrefs.HasKey("highScore"))
        {
            GameManager.HighScore = PlayerPrefs.GetInt("highScore");
        }
        else
        {
            GameManager.HighScore = 0;
        }     
    }
    public static void SaveGame()
    {
        PlayerPrefs.SetInt("highScore", GameManager.HighScore);
    }
    public static void SaveVolume()
    {
        PlayerPrefs.SetFloat("volume",GameManager.Volume);
    }
    public static void LoadVolume()
    {
        if(PlayerPrefs.HasKey("volume"))
        {
            GameManager.Volume = PlayerPrefs.GetFloat("volume");
        }
        else
            GameManager.Volume = 1f;
    }
    public static void SaveSkin()
    {
        PlayerPrefs.SetInt("skin", GameManager.Skin);
    }
    public static void LoadSkin()
    {
        if (PlayerPrefs.HasKey("skin"))
        {
            GameManager.Skin = PlayerPrefs.GetInt("skin");
        }
        else
            GameManager.Skin = 0;
    }
    public static void SaveSchool()
    {
        PlayerPrefs.SetInt("school", GameManager.School);
    }
    public static void LoadSchool()
    {
        if (PlayerPrefs.HasKey("school"))
        {
            GameManager.School = PlayerPrefs.GetInt("school");
        }
        else
            GameManager.School = 0;
    }
}
