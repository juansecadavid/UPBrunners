using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    public MissionManager missionManager;
    public GameObject missionPanelPrefab;   // Prefab de la UI para una misión
    public Transform missionsParent;        // Dónde instanciar las misiones en la UI

    public void UpdateMissionUI()
    {
        // Borrar UI actual
        foreach (Transform child in missionsParent)
            Destroy(child.gameObject);

        // Actualizar UI con misiones activas
        foreach (MissionBase mission in missionManager.activeMissions)
        {
            GameObject panel = Instantiate(missionPanelPrefab, missionsParent);
            // Aquí puedes configurar el texto y otros elementos de la UI del panel con los datos de 'mission'
        }
    }
}
