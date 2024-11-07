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

    private PlayerType _playerType;
    
    public AL al;

    public int playerIndex;
//    private GameObject player;F

    #region GameSettings
/*this function progress the next level*/
    public void playGame(){ 
        switch (playerIndex)
        {
            case 0:
                _playerType = PlayerType.Accountant;
                break;
            case 1:
                _playerType = PlayerType.Civilian;
                break;
            case 2:
                _playerType = PlayerType.Soldier;
                break;
        }

        Debug.Log("Loading scene with " + playerIndex);
        SceneManager.LoadScene("new_fcking_scene");
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
    // public GameObject PauseMenuUI;

    // public static bool GameIsPaused =false;
    void Update (){
        if (_player == null && SceneManager.GetActiveScene().name == "new_fcking_scene")
        {
            _player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerController>();
        }

        // if (Input.GetKeyDown(KeyCode.Escape)){
        //     if (GameIsPaused){
        //         Resume();
        //     }else{
        //         Pause();
        //     }
        // }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("new_fcking_scene");
        }
    }
    // public void Resume(){
    //     PauseMenuUI.SetActive(false);
    //     Time.timeScale=1f;
    //     GameIsPaused = false;

    // }
    public void LoadMenu(){
        Time.timeScale =1f;
        Debug.Log("settings button");
        SceneManager.LoadScene("Menu");

    }
    
    // void Pause(){
    //     PauseMenuUI.SetActive(true);
    //     Time.timeScale =0f;
    //     GameIsPaused = true;

    // }

    public void SetPlayerType(int index)
    {
        PlayerPrefs.SetInt("Type", index);
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
        //PauseMenuUI.SetActive(false);
        EventManager.SelectedCharacterChangedEvent += OnSelectedCharacterChanged;
    }

    private void OnSelectedCharacterChanged(object sender, int e)
    {
        playerIndex = e;
    }

    void Awake()
    {
//        player = GameObject.FindGameObjectWithTag("Player");
        //DontDestroyOnLoad(transform.gameObject);
        if (Instance == null)
        {
            Instance = this;
            _musicOn = true;
            _musicVolume = 100;
            try
            {
            al = GameObject.FindGameObjectWithTag("AL").GetComponent<AL>();
            } catch (Exception e) { Debug.Log(e.StackTrace); }
            DontDestroyOnLoad(gameObject);
            return;
        }

        

       Destroy(gameObject);
    }
}
