using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public  RectTransform PopupMenu;

   bool menuOpen = false;
   int counter = 0;
   public void OpenMenu(){
      counter++;
      Debug.Log("asdasd" + counter);
        if (counter %2 ==1){
            menuOpen = menuOpen=true;
            PopupMenu.DOAnchorPos(new Vector2(123,330),0.20f);
            
        }else{
            menuOpen = menuOpen=false;
            PopupMenu.DOAnchorPos(new Vector2(123,18),0.20f);
        }
        
    }
    public static bool GameIsPaused =false;

    public GameObject PauseMenuUI;
    
    void Update (){
//        if (Input.GetKeyDown(KeyCode.Escape)){
//            if (GameIsPaused){
//                Resume();
//            }else{
//                Pause();
//            }
//        }
    }
    public void FullScreen(bool isfullscreen){
        Debug.Log(isfullscreen);
        Screen.fullScreen = isfullscreen;
    }
    void Start()
    {
        PauseMenuUI.SetActive(false);
    }
}
