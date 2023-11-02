using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }
    public MissionUI missionUI;
    public List<GameObject> missionList=new List<GameObject>();
    public List<MissionBase> activeMissions = new List<MissionBase>();      // Lista de misiones activas
    public List<MissionBase> availableMissions = new List<MissionBase>();   // Todas las misiones disponibles

    private void Awake()
    {
        if (Instance == null&&GameManager.MMInstance==null)
        {
            FirstConfig();
        }
        else if (Instance != this)
        {
            //FirstConfig();
            Destroy(gameObject); // Si ya existe un MissionManager, destruye el duplicado
        }
    }
    private void FirstConfig()
    {
        if (GameManager.MMInstance == null)
        {
            Instance = this;
            GameManager.MMInstance = Instance;
            DontDestroyOnLoad(gameObject); // Mantener este objeto a través de las cargas de escena
            InstantiateMissions();
            InitializeMissions(); // Inicializar las misiones si es necesario, solo la primera vez    
        }
        InstantiateMissions();
        GameManager.MMInstance.missionUI = FindAnyObjectByType<MissionUI>();
        GameManager.MMInstance.missionUI.ShowMissions(GameManager.MMInstance.activeMissions, GameManager.MMInstance.missionList);
    }
    private void InstantiateMissions()
    {
        foreach (var mission in GameManager.MMInstance.availableMissions)
        {
            mission.SetMission();
            mission.mis.tag = $"{mission.MissionName}";
            GameManager.MMInstance.missionList.Add(mission.mis);
        }
    }
    private void InitializeMissions()
    {
        // Aquí podrías cargar las misiones desde un archivo de guardado si fuera necesario
        // Por ahora, solo asegurémonos de que no reiniciamos la lista si ya hay misiones activas
        if (activeMissions.Count == 0)
        {
            AssignRandomMission(); // Asigna una misión aleatoria para empezar, si es necesario
        }
    }

    public void AssignRandomMission()
    {
        if (availableMissions.Count > 0)
        {
            int randomIndex = Random.Range(0, availableMissions.Count);
            activeMissions.Add(availableMissions[randomIndex]);
            availableMissions.RemoveAt(randomIndex);
        }
    }

    public void CheckMissionsStatus()
    {
        // Debido a que vamos a modificar la lista mientras la iteramos, usamos un bucle hacia atrás
        for (int i = activeMissions.Count - 1; i >= 0; i--)
        {
            if (activeMissions[i].CheckMissionComplete())
            {
                activeMissions.RemoveAt(i);
                AssignRandomMission();
            }
        }
    }

    // Resto de la lógica del MissionManager...
}
