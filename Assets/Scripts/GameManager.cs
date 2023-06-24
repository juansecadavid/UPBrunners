using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager 
{
    private static int highScore=0;
    private static int loses=0;
    private static float volume=0;
    private static int skin=0;

    public static int HighScore { get => highScore; set => highScore = value; }
    public static int Loses { get => loses; set => loses = value; }
    public static float Volume { get => volume; set => volume = value; }
    public static int Skin { get => skin; set => skin = value; }
}
