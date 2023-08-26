using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelFirstLose;
    [SerializeField]
    private GameObject panelFirstLose2;
    [SerializeField]
    private GameObject panelDefLose;
    private SkinSelector skinSelector;
    private Animator animator;
    private int losses=0;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private TextMeshProUGUI finalScore;
    private bool hasEnoughCoins=false;
    [SerializeField]
    private int valueToRespawn;
    [SerializeField]
    private VideoPlayer videoPlayer;
    [SerializeField]
    private VideoClip[] videoClips;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private GameObject blackPanel;
    [SerializeField]
    private TextMeshProUGUI pauseTimer;
    private SoundManager SoundManager;
    // Start is called before the first frame update
    private void Awake()
    {
        if (GameManager.ActiveLetter1.Count < 0)
        {
            GameManager.ActiveLetter1.RemoveAt(0);
        }
        GameManager.Letras = 0;
    }
    void Start()
    {
        SoundManager = FindObjectOfType<SoundManager>();
        skinSelector = FindObjectOfType<SkinSelector>();
        animator = skinSelector.Skins[GameManager.Skin].GetComponent<Animator>();
        GameManager.Letras = 0;
        if (GameManager.ActiveLetter1.Count<0)
        {
            GameManager.ActiveLetter1.RemoveAt(0);
        }
    }
    public void Lose()
    {
        if(losses==0)
        {
            if (CheckConditionsToRespawn())
            {
                GameManager.IsPaused = true;
                panelFirstLose.SetActive(true);
                animator.enabled = false;
                losses++;
            }
            else
            {
                GameManager.IsPaused = true;
                panelFirstLose2.SetActive(true);
                animator.enabled = false;
                losses++;
            }           
        }
        else
        {
            panelDefLose.SetActive(true);
            animator.enabled = false;
            Score score=player.GetComponent<Score>();
            finalScore.text = $"Tu puntuación final fué: {score.CurrentNumber}";
        }     
    }
    public void Respawn()
    {
        if (panelFirstLose.activeInHierarchy)
        {
            panelFirstLose.SetActive(false);
        }
        else
        {
            panelFirstLose2.SetActive(false);
        }
        StartCoroutine(RespawnTimer());       
    }
    public bool CheckConditionsToRespawn()
    {
        PowerCoins coins=player.GetComponent<PowerCoins>(); 
        if(coins.Coins>=valueToRespawn)
        {
            hasEnoughCoins = true;
            return true;
        }
        else
            return false;
    }
    public void ShowVideo()
    {
        blackPanel.SetActive(true);
        int clip = Random.Range(0,videoClips.Length);
        videoPlayer.clip = videoClips[clip];
        audioSource.volume = GameManager.Volume;
        videoPlayer.Play();
        StartCoroutine(StopPlaying());
        
    }
    IEnumerator StopPlaying()
    {
        yield return new WaitForSeconds((float)videoPlayer.clip.length);
        videoPlayer.Stop();
        blackPanel.SetActive(false);
        Respawn();
    }
    IEnumerator RespawnTimer()
    {
        pauseTimer.text = "";
        yield return new WaitForSeconds(0.2f);
        SoundManager.PlaySound(6);
        pauseTimer.text = "3";
        yield return new WaitForSeconds(1f);
        pauseTimer.text="2";
        yield return new WaitForSeconds(1f);
        pauseTimer.text="1";
        yield return new WaitForSeconds(1f);
        pauseTimer.text = "";
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 5);
        GameManager.IsPaused = false;
        Movement mov = player.GetComponent<Movement>();
        if(mov.velocidadMovimiento>20)
        {
            mov.velocidadMovimiento -= 5;
            mov.velocidadMovimientoLateral -= 1.25f;
        }      
        UpdateSpped upd = player.GetComponent<UpdateSpped>();
        Score score = player.GetComponent<Score>();
        PowerCoins coins = player.GetComponent<PowerCoins>();
        if (hasEnoughCoins)
        {
            coins.Coins -= valueToRespawn;
        }
        else
            coins.Coins = 0;
        mov.HasRespawn();
        mov.enabled = true;
        upd.enabled = true;
        score.enabled = true;
        animator.enabled = true;

        VolumeSlider volume = FindObjectOfType<VolumeSlider>();
        volume.PlayMusic();
    }
    public void HideAndShow(GameObject toHide)
    {
        StartCoroutine(hideAndShow(toHide));
    }
    IEnumerator hideAndShow(GameObject toHide)
    {
        toHide.SetActive(false);
        yield return new WaitForSeconds(1);
        toHide.SetActive(true);
        yield return null;
    }
}
