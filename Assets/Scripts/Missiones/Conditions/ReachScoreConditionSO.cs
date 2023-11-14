using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Missions/Conditions/ReachScoreCondition")]
public class ReachScoreConditionSO : MissionConditionSO
{

    public override bool Evaluate(MissionLive missionLive)
    {
        currentStatus = missionLive.score;
        return currentStatus >= goal;
    }
}
