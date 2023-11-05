using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMissionTemplate", menuName = "Missions/Nueva Misi�n")]
public class MissionBase : ScriptableObject
{
    public string MissionName;       // Nombre de la misi�n
    public string MissionText;       // Descripci�n de la misi�n
    public bool IsCompleted;
    public MissionConditionSO condition;// Estado de la misi�n
    public MissionPrefab MissionPrefab;
    public GameObject mis;
    // Para este ejemplo, vamos a usar una condici�n simple. Pero puedes expandir esto m�s tarde.
    public int RequiredItemCount=0;    // Ejemplo: Recolecta 10 manzanas
    public int CurrentItemCount=0;     // Cu�ntos �tems ha recolectado el jugador
    //public GameObject MissionUI;
    public Reward reward;            // Recompensa al completar la misi�n
    [System.Serializable]
    public class Reward
    {
        public int ExperiencePoints; // Puntos de experiencia al completar la misi�n
        public int Gold;             // Oro o moneda al completar
        // Aqu� puedes a�adir otras recompensas si lo necesitas
    }


    // M�todo para recibir la recompensa
    public Reward GetReward()
    {
        if (IsCompleted)
            return reward;
        else
            return null;
    }
    public void SetMission(Transform parent)
    {
        MissionPrefab.missionTitle.text = MissionName;
        MissionPrefab.missionDescription.text = MissionText;
        MissionPrefab.SetCurrentStatus(CurrentItemCount,RequiredItemCount);
        mis = Instantiate(MissionPrefab.gameObject,parent,false);
        if(condition!=null)
        {
            condition.goal = RequiredItemCount;
        }     
    }
    public void OnCompleted()
    {
        CurrentItemCount = 0;
    }
    public void UpdateWhileEvaluation()
    {
        CurrentItemCount = condition.currentStatus;
    }
}
