using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionConditionSO : ScriptableObject
{
    public int goal;
    public int currentStatus;
    public abstract bool Evaluate(MissionLive missionLive);
}
