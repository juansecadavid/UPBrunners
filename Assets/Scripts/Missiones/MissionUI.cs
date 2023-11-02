using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    public MissionManager missionManager;
    //public GameObject missionPanelPrefab;   // Prefab de la UI para una misi�n
    public Transform missionsParent;        // D�nde instanciar las misiones en la UI

    [SerializeField]
    private GameObject missionPanel;
    public void UpdateMissionUI()
    {
        // Borrar UI actual
        foreach (Transform child in missionsParent)
            Destroy(child.gameObject);

        // Actualizar UI con misiones activas
        foreach (MissionBase mission in missionManager.activeMissions)
        {
            GameObject panel = Instantiate(missionPanel, missionsParent);
            // Aqu� puedes configurar el texto y otros elementos de la UI del panel con los datos de 'mission'
        }
    }
    public void ShowMissions(List<MissionBase> activeMissions, List<GameObject> missionList)
    {
        GameObject activeM;
        foreach (var mission in activeMissions)
        {
            activeM = FindActiveMission(mission,missionList);
            GameObject funcionaPls = Instantiate(activeM);
            funcionaPls.transform.SetParent(missionPanel.transform, false);
            Debug.Log("ENTR� AQU�2");
        }
    }
    private GameObject FindActiveMission(MissionBase ActiveMission, List<GameObject> missionList)
    {
        for (int i = 0; i < missionList.Count; i++)
        {
            if (missionList!=null&&ActiveMission.MissionName == missionList[i].tag)
            {
                return missionList[i];
            }
            else
            {
                Debug.Log("Encontr� el error");
            }
        }
        return null;
    }
}
