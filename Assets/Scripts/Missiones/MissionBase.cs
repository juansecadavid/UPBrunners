using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMissionTemplate", menuName = "Misiones/Nueva Misi�n")]
public class MissionBase : ScriptableObject
{
    public string MissionName;       // Nombre de la misi�n
    public string MissionText;       // Descripci�n de la misi�n
    public bool IsCompleted;         // Estado de la misi�n

    // Para este ejemplo, vamos a usar una condici�n simple. Pero puedes expandir esto m�s tarde.
    public int RequiredItemCount;    // Ejemplo: Recolecta 10 manzanas
    public int CurrentItemCount;     // Cu�ntos �tems ha recolectado el jugador

    public Reward reward;            // Recompensa al completar la misi�n

    [System.Serializable]
    public class Reward
    {
        public int ExperiencePoints; // Puntos de experiencia al completar la misi�n
        public int Gold;             // Oro o moneda al completar
        // Aqu� puedes a�adir otras recompensas si lo necesitas
    }

    // M�todo para chequear si la misi�n est� completa
    public bool CheckMissionComplete()
    {
        if (CurrentItemCount >= RequiredItemCount)
        {
            IsCompleted = true;
            return true;
        }
        return false;
    }

    // M�todo para recibir la recompensa
    public Reward GetReward()
    {
        if (IsCompleted)
            return reward;
        else
            return null;
    }
}
