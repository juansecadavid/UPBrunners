using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Missions/Conditions/CollectCoinsCondition")]
public class CollectCoinsConditionSO : MissionConditionSO
{
    public int coinsToCollect;

    public override bool Evaluate(MissionLive missionLive)
    {
        currentStatus = missionLive.coinsCollected;
        return missionLive.coinsCollected >= goal;
    }
}
