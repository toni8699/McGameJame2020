using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using DG.Tweening;

//Singleton
public class GameManager : MonoBehaviour
{
     
    public static GameManager Instance;
    [SerializeField] private PlayerController _player;

    private GameObject player;

    #region GameSettings
/*this function progress the next level*/
    public void playGame(){ 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1 );

    }
    public void Quit(){
        Debug.Log("Quit");
        Application.Quit();
    }
    public AudioMixer audioMixer;
    
    
    public void FullScreen(bool isfullscreen){
        Debug.Log(isfullscreen);
        Screen.fullScreen = isfullscreen;
    }
    public GameObject PauseMenuUI;

    public static bool GameIsPaused =false;
    void Update (){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }
    public void Resume(){
        PauseMenuUI.SetActive(false);
        Time.timeScale=1f;
        GameIsPaused = false;

    }
    public void LoadMenu(){
        Time.timeScale =1f;
        Debug.Log("settings button");
        SceneManager.LoadScene("Menu");

    }
    
    void Pause(){
        PauseMenuUI.SetActive(true);
        Time.timeScale =0f;
        GameIsPaused = true;

    }


    public void SetPlayerType(PlayerType type)
    {
        player.GetComponent<PlayerController>().Type = type;
    }
    public void SetPlayerToAccountant()
    {
        player.GetComponent<PlayerController>().Type = PlayerType.Accountant;
    }
    

    public double _musicVolume;
    public bool _musicOn;

    #endregion

    //all scores are from 0 to 1
    private double _overallScore;

    private int _timeTravelUsed; 
   // void Start(){
       // Time.timeScale=1;
    //}

    public PlayerController Player => _player;

    void Start()
    {
        PauseMenuUI.SetActive(false);
    }
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //DontDestroyOnLoad(transform.gameObject);
        if (Instance == null)
        {
            Instance = this;
            _musicOn = true;
            _musicVolume = 100;
            return;
        }

        Destroy(gameObject);
    }
}
