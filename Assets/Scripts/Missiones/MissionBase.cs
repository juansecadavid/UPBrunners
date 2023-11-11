using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMissionTemplate", menuName = "Missions/Nueva Misión")]
public class MissionBase : ScriptableObject
{
    public string MissionName;       // Nombre de la misión
    public string MissionText;       // Descripción de la misión
    public bool IsCompleted;
    public MissionConditionSO condition;// Estado de la misión
    public MissionPrefab MissionPrefab;
    public GameObject mis;
    // Para este ejemplo, vamos a usar una condición simple. Pero puedes expandir esto más tarde.
    public int RequiredItemCount=0;    // Ejemplo: Recolecta 10 manzanas
    public int CurrentItemCount=0;     // Cuántos ítems ha recolectado el jugador
    //public GameObject MissionUI;
    public Reward reward;            // Recompensa al completar la misión
    [System.Serializable]
    public class Reward
    {
        public int ExperiencePoints; // Puntos de experiencia al completar la misión
        public int Gold;             // Oro o moneda al completar
        // Aquí puedes añadir otras recompensas si lo necesitas
    }


    // Método para recibir la recompensa
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
