using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Missions/Conditions/CollectCoinsCondition")]
public class CollectCoinsConditionSO : MissionConditionSO
{

    public override bool Evaluate(MissionLive missionLive)
    {
        currentStatus = missionLive.coinsCollected;
        return currentStatus >= goal;
    }
}
