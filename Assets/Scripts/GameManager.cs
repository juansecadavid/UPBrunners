using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager 
{
    private static int highScore=0;
    private static int loses=0;
    private static float volume=0;
    private static int skin=0;
    private static int school=0;
    private static int letras = 0;
    private static bool isPaused=false;
    private static bool hasLost=false;
    private static bool isAutoPlaying=false;
    private static List<GameObject> ActiveLetter=new List<GameObject>();

    public static int HighScore { get => highScore; set => highScore = value; }
    public static int Loses { get => loses; set => loses = value; }
    public static float Volume { get => volume; set => volume = value; }
    public static int Skin { get => skin; set => skin = value; }
    public static int School { get => school; set => school = value; }
    public static int Letras { get => letras; set => letras = value; }
    public static List<GameObject> ActiveLetter1 { get => ActiveLetter; set => ActiveLetter = value; }
    public static bool IsPaused { get => isPaused; set => isPaused = value; }
    public static bool HasLost { get => hasLost; set => hasLost = value; }
    public static bool IsAutoPlaying { get => isAutoPlaying; set => isAutoPlaying = value; }
}
