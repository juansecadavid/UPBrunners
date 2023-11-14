using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Missions/Conditions/UnlockSkin")]
public class UnlockSkin : MissionConditionSO
{
    public override bool Evaluate(MissionLive missionLive)
    {
        currentStatus = missionLive.skinsCollected;
        return currentStatus >= goal;
    }
}
