using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance { get; private set; }
    public MissionUI missionUI;
    public List<GameObject> missionList=new List<GameObject>();
    public List<GameObject> actualMissionObjects = new List<GameObject>();
    public List<MissionBase> activeMissions = new List<MissionBase>();      // Lista de misiones activas
    public List<MissionBase> availableMissions = new List<MissionBase>();   // Todas las misiones disponibles
    bool firstTime=true;
    public MissionLive missionLive;
    public MessagesOnPlay messageOnPlay;
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
        
        //InstantiateMissions();
        GameManager.MMInstance.missionUI = FindAnyObjectByType<MissionUI>();
        GameManager.MMInstance.missionUI.ShowMissions(GameManager.MMInstance.activeMissions, GameManager.MMInstance.missionList,actualMissionObjects);
    }
    private void InstantiateMissions()
    {
        foreach (var mission in GameManager.MMInstance.availableMissions)
        {
            mission.SetMission(transform);
            //mission.mis.tag = $"{mission.MissionName}";
            GameManager.MMInstance.missionList.Add(mission.mis);
        }
    }
    private void InitializeMissions()
    {
        // Aquí podrías cargar las misiones desde un archivo de guardado si fuera necesario
        // Por ahora, solo asegurémonos de que no reiniciamos la lista si ya hay misiones activas
        if (activeMissions.Count == 0)
        {
            AssignRandomMission(3); // Asigna una misión aleatoria para empezar, si es necesario
        }
    }

    public void AssignRandomMission(int numberOfMissions)
    {
        for (int i = 0; i < numberOfMissions; i++)
        {
            if (availableMissions.Count > 0&&firstTime)
            {
                int randomIndex = Random.Range(0, availableMissions.Count);
                activeMissions.Add(availableMissions[randomIndex]);
                availableMissions.RemoveAt(randomIndex);
            }
            else if(availableMissions.Count > 0)
            {
                int randomIndex = Random.Range(0, availableMissions.Count);
                activeMissions.Add(availableMissions[randomIndex]);
                availableMissions.RemoveAt(randomIndex);
            }
        } 
    }

    public void CheckMissionsStatus()
    {
        // Debido a que vamos a modificar la lista mientras la iteramos, usamos un bucle hacia atrás
        
        for (int i = activeMissions.Count - 1; i >= 0; i--)
        {
            if (missionLive != null && activeMissions[i].condition!=null &&activeMissions[i].condition.Evaluate(missionLive))
            {
                messageOnPlay.ShowMessage($"Has completado la misión: {activeMissions[i].MissionName}",3f);
                availableMissions.Add(activeMissions[i]);
                activeMissions[i].OnCompleted();
                activeMissions.RemoveAt(i);
                AssignRandomMission(1);
            }
            else if(missionLive != null && activeMissions[i].condition != null && !activeMissions[i].condition.Evaluate(missionLive))
            {
                activeMissions[i].UpdateWhileEvaluation();
            }
        }
    }

    void OnEnable()
    {
        // Asegúrate de suscribir el método al evento cuando el objeto se activa
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Es igualmente importante desuscribir el método cuando el objeto se desactiva
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Aquí puedes comprobar si es la escena correcta antes de ejecutar `firstConfig`
        if (scene.name == "Inicio"&&!firstTime)
        {
            FirstConfig();
        }
        firstTime = false;
        if(scene.name=="SceneTest")
        {
            missionLive=FindAnyObjectByType<MissionLive>();
            messageOnPlay=FindAnyObjectByType<MessagesOnPlay>();
        }
    }
    // Resto de la lógica del MissionManager...
}
