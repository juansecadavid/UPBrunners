using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionPrefab : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI missionTitle;
    [SerializeField]
    public TextMeshProUGUI missionDescription;

    public void SetMissionTitle(string missionTile)
    {
        this.missionTitle.text = missionTile;
    }
    public void SetMissionDescription(string missionDescription)
    {
        this.missionDescription.text = missionDescription;
    }
}
