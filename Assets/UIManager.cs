using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    private bool isVoiceOn=true;
    public Sprite[] voiceonoffSprites;

    public GameObject spaceMan;
    void Start()
    {
        InvokeRepeating("CreateSpaceMan", 0, 2);
    }

    void Update()
    {
        
    }

    public void CreateSpaceMan()
    {
        var x = Random.Range(0, Screen.height);
        var y = Random.Range(0, Screen.width);
        var SMan = Instantiate(spaceMan, new Vector2(y, x), Quaternion.identity ,gameObject.transform);
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
