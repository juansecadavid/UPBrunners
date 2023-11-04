using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionUI : MonoBehaviour
{
    public MissionManager missionManager;
    //public GameObject missionPanelPrefab;   // Prefab de la UI para una misión
    public Transform missionsParent;        // Dónde instanciar las misiones en la UI
    int pos=0;
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
            // Aquí puedes configurar el texto y otros elementos de la UI del panel con los datos de 'mission'
        }
    }
    public void ShowMissions(List<MissionBase> activeMissions, List<GameObject> missionList,List<GameObject> actualMissionsObjects)
    {
        GameObject activeM;
        foreach (var mission in activeMissions)
        {
            activeM = FindActiveMission(mission,missionList);
            actualMissionsObjects.Add(activeM);
            GameObject missionCopy = Instantiate(activeM);
            missionCopy.transform.SetParent(missionPanel.transform, false);
            SetTransform(missionCopy,activeMissions);
            Debug.Log("ENTRÉ AQUÍ2");
        }
    }
    private GameObject FindActiveMission(MissionBase ActiveMission, List<GameObject> missionList)
    {
        for (int i = 0; i < missionList.Count; i++)
        {
            MissionPrefab missionBase=missionList[i].GetComponent<MissionPrefab>();
            if (missionList!=null&&ActiveMission.MissionName == missionBase.missionTitle.text)
            {
                UpdateObjects(ActiveMission,missionBase);
                return missionList[i];
            }
        }
        return null;
    }
    private void SetTransform(GameObject mission, List<MissionBase> activeMissions)
    {
        //GameManager.ActiveMissions.Add(mission); //MUY IMPORTANTE QUITAR ESTO DE ACÁ Y PONERLO EN EL MISSIONMANAGER
        RectTransform rect = mission.GetComponent<RectTransform>();
        for (int i = 0; i < activeMissions.Count; i++)
        {
            if (activeMissions[i]==mission)
            {
                pos = i;
            }
        }
        switch(pos)
        {
            case 0:
                rect.anchoredPosition3D = new Vector3(rect.anchoredPosition3D.x, -570,rect.anchoredPosition3D.z);
                pos++;
                break;
            case 1:
                rect.anchoredPosition3D = new Vector3(rect.anchoredPosition3D.x, -770, rect.anchoredPosition3D.z);
                pos++;
                break;
            case 2:
                rect.anchoredPosition3D = new Vector3(rect.anchoredPosition3D.x, -970, rect.anchoredPosition3D.z);
                pos++;
                break;

        }
    }
    private void UpdateObjects(MissionBase misionBase, MissionPrefab missionPrefab)
    {
        missionPrefab.SetCurrentStatus(misionBase.CurrentItemCount,misionBase.RequiredItemCount);
    }
}
