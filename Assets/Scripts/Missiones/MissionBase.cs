using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMissionTemplate", menuName = "Misiones/Nueva Misión")]
public class MissionBase : ScriptableObject
{
    public string MissionName;       // Nombre de la misión
    public string MissionText;       // Descripción de la misión
    public bool IsCompleted;         // Estado de la misión

    // Para este ejemplo, vamos a usar una condición simple. Pero puedes expandir esto más tarde.
    public int RequiredItemCount;    // Ejemplo: Recolecta 10 manzanas
    public int CurrentItemCount;     // Cuántos ítems ha recolectado el jugador

    public Reward reward;            // Recompensa al completar la misión

    [System.Serializable]
    public class Reward
    {
        public int ExperiencePoints; // Puntos de experiencia al completar la misión
        public int Gold;             // Oro o moneda al completar
        // Aquí puedes añadir otras recompensas si lo necesitas
    }

    // Método para chequear si la misión está completa
    public bool CheckMissionComplete()
    {
        if (CurrentItemCount >= RequiredItemCount)
        {
            IsCompleted = true;
            return true;
        }
        return false;
    }

    // Método para recibir la recompensa
    public Reward GetReward()
    {
        if (IsCompleted)
            return reward;
        else
            return null;
    }
}
