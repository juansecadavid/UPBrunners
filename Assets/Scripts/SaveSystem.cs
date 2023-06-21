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
}
