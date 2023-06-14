using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicUpExperience : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Score score = other.GetComponentInParent<Score>();
            score.CurrentNumber += 50;
            Destroy(gameObject);
        }
    }
}
