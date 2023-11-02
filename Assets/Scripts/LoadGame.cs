using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadGame : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highScoreText;
    [SerializeField]
    private GameObject[] skins;
    [SerializeField]
    private string[] names;
    [SerializeField]
    private TextMeshProUGUI name;

    public bool[] skinsDesbloqueadas; // Arreglo de booleanos para el estado de desbloqueo de las skins
    // Start is called before the first frame update
    void Awake()
    {
        SaveSystem.LoadGame(); 
        SaveSystem.LoadVolume();
        SaveSystem.LoadSkin();
        SaveSystem.LoadSchool();
        highScoreText.text = $"HighScore {GameManager.HighScore}";
        skins[GameManager.Skin].SetActive(true);
        name.text = $"{names[GameManager.Skin]}";

        skinsDesbloqueadas = new bool[4]; 

        // Desbloquear la primera skin y bloquear las demás
        skinsDesbloqueadas[0] = true;
        for (int i = 1; i < 4; i++) // Iterar solo 3 veces ya que la primera skin ya está desbloqueada
        {
            skinsDesbloqueadas[i] = false;
        }
    }
    private void Update()
    {
        name.text = $"{names[GameManager.Skin]}";
    }

    public void RightSkinBtn()
    {
        if (skinsDesbloqueadas[(GameManager.Skin + 1) % skins.Length]) 
        {
            skins[GameManager.Skin].SetActive(false);
            GameManager.Skin = (GameManager.Skin + 1) % skins.Length; 
            skins[GameManager.Skin].SetActive(true);
        }
    }
    
    public void LeftSkinBtn()
    {
        if (skinsDesbloqueadas[(GameManager.Skin + skins.Length - 1) % skins.Length]) 
        {
            skins[GameManager.Skin].SetActive(false);
            GameManager.Skin = (GameManager.Skin + skins.Length - 1) % skins.Length; 
            skins[GameManager.Skin].SetActive(true);
        }
    }
}
