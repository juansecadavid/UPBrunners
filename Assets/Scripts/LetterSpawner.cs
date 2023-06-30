using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] letters;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.ActiveLetter1.Count<1)
        {
            Vector3 pos = transform.position;
            Quaternion rot = transform.rotation;
            if (GameManager.Letras == 0)
            {
                Instantiate(letters[0], pos, rot);
                GameManager.ActiveLetter1.Add(letters[0]);
            }
            else if (GameManager.Letras == 1)
            {
                Instantiate(letters[1], pos, rot);
                GameManager.ActiveLetter1.Add(letters[1]);
            }
            else
            {
                Instantiate(letters[2], pos, rot);
                GameManager.ActiveLetter1.Add(letters[2]);
            }
        }       
    }
}
