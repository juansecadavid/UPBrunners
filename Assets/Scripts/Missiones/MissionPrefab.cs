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
    [SerializeField]
    public TextMeshProUGUI missionCurrentStatus;
    public void SetMissionTitle(string missionTile)
    {
        this.missionTitle.text = missionTile;
    }
    public void SetMissionDescription(string missionDescription)
    {
        this.missionDescription.text = missionDescription;
    }
    public void SetCurrentStatus(int missionCurrentStatus,int goal)
    {
        this.missionCurrentStatus.text = $"{missionCurrentStatus}/{goal}";
    }
}
