using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public List<MissionBase> activeMissions = new List<MissionBase>();      // Lista de misiones activas
    public List<MissionBase> completedMissions = new List<MissionBase>();   // Lista de misiones completadas
    public List<MissionBase> availableMissions = new List<MissionBase>();   // Todas las misiones disponibles (esto puede ser una lista de ScriptableObjects que crees en el editor)

    // M�todo para agregar una misi�n aleatoria de las disponibles a las activas
    public void AssignRandomMission()
    {
        if (availableMissions.Count > 0)
        {
            int randomIndex = Random.Range(0, availableMissions.Count);
            activeMissions.Add(availableMissions[randomIndex]);
            availableMissions.RemoveAt(randomIndex);
        }
    }

    // M�todo para chequear el estado de las misiones
    public void CheckMissionsStatus()
    {
        foreach (MissionBase mission in activeMissions)
        {
            if (mission.CheckMissionComplete())
            {
                completedMissions.Add(mission);
                activeMissions.Remove(mission);
                AssignRandomMission();
            }
        }
    }
}
