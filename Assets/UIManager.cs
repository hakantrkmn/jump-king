using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    private bool isVoiceOn=true;
    public Sprite[] voiceonoffSprites; 
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void onPlayButtonClick()
    {
        SceneManager.LoadScene("level 1", LoadSceneMode.Single);
    }
    public void onPauseButtonClick()
    {
        Time.timeScale = 0f;
    }
    public void onContinuoButtonClick()
    {
        Time.timeScale = 1f;
    }
    public void onVoiceButtonClick(GameObject thisButton)
    {
        Image myImage=thisButton.GetComponent<Image>();
        if (isVoiceOn)
        {
            isVoiceOn = false;
            myImage.sprite = voiceonoffSprites[0];

        }
        else
        {
            isVoiceOn = true;
            myImage.sprite = voiceonoffSprites[1];

        }
    }


}
