using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AL : MonoBehaviour
{
    private int selectedCharacterIndex=0;
    private Color desiredColor;
   // private color desiredColor;
    [Header("List of characters")]
    [SerializeField] private List<CharacterSelect> characterlist = new List<CharacterSelect>();

    [Header("Stuffs")]
    [SerializeField] private TextMeshProUGUI CharacterName;
    [SerializeField] private Image CharacterSplash;
    [SerializeField] private Image background;

    public int SelectedCharacterIndex
    {
        get => selectedCharacterIndex;
        set
        {
            selectedCharacterIndex = value;
            EventManager.ChangeSelectedCharacter(value);
            GameManager.Instance.playerIndex = selectedCharacterIndex;
        }
    }

    public void LeftButton()
    {
        if (SelectedCharacterIndex - 1 < 0)
        {
            SelectedCharacterIndex = 2;
        }
        else
        {
            SelectedCharacterIndex--;
        }

        UpdateCharacter();
    }
    public void RightButton()
    {
        SelectedCharacterIndex = (SelectedCharacterIndex + 1) % characterlist.Count;
        Debug.Log("character log " + SelectedCharacterIndex);
        UpdateCharacter();
    }
    private void Start()
    {
        SelectedCharacterIndex = 0;
        UpdateCharacter();
    }
    public int getCurrentIndex(){
        return selectedCharacterIndex;
    }

private void UpdateCharacter(){
        CharacterSplash.sprite = characterlist[selectedCharacterIndex].splash;
        CharacterName.text = characterlist[selectedCharacterIndex].characterName;
        desiredColor = characterlist[selectedCharacterIndex].charactercolor;

    }
    [System.Serializable]
    public class CharacterSelect{
        public Sprite splash;
        public string characterName;
        public Color charactercolor;

    }
    
}

