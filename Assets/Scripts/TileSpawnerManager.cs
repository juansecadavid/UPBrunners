using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnerManager : MonoBehaviour
{
    private void OnDestroy()
    {
        if(GameManager.ActiveLetter1.Count>0)
        {
            GameManager.ActiveLetter1.RemoveAt(0);
        }
    }
}
