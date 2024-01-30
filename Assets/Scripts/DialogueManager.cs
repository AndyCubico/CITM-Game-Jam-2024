using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public FloatSO Dialogue;
    public FloatSO Boss1;
    public FloatSO Boss2;

    public GameObject[] dialogue;
    public int storyTeller = 0;
    public UIFadeToBlack fadeToBlack;
    public UIFadeFromBlack fadeFromBlack;

    public bool textHasChanged;
    public bool returnFromBlack;
    bool start = false;

    public Image fadeToBlackImage;

    public ChangeSceneDialogue changeScene;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < dialogue.Length; i++)
        {
            dialogue[i].SetActive(false);
        }

        textHasChanged = false;
        returnFromBlack = false;
        start = true;

        dialogue[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (start) 
        {
            if (unFade())
            {
                start = false;
            }
        }

        if (!start)
        {
            if (Input.anyKeyDown)
            {
                textHasChanged = true;
            }
        }

        if(textHasChanged && !returnFromBlack && storyTeller == dialogue.Length - 1)
        {
            if (Fade())
            {
                if (Dialogue.Value == 0)
                {
                    changeScene.ChangeToScene("Tutorial");
                }
                
                if (Dialogue.Value == 1)
                {
                    Boss1.Value = 1;
                    changeScene.ChangeToScene("Boss 1 Clown");
                } 
                
                if (Dialogue.Value == 2)
                {
                    Boss2.Value = 1;
                    changeScene.ChangeToScene("Boss 2 PerroSanchez");
                }
            }
        }

        if (storyTeller != dialogue.Length - 1)
        {
            if (textHasChanged && !returnFromBlack)
            {
                if (Fade())
                {
                    dialogue[storyTeller].SetActive(false);

                    storyTeller++;

                    dialogue[storyTeller].SetActive(true);

                    textHasChanged = false;
                    returnFromBlack = true;
                }
            }
        }

        if (returnFromBlack)
        {
            if (unFade()) 
            {
                returnFromBlack = false;
            }
        }
    }

    private bool Fade() 
    {
        bool hasEnded = false;

        if (fadeToBlackImage.color.a < 1f) 
        {
            fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, fadeToBlackImage.color.a + 0.8f * Time.deltaTime);
        }
        else 
        {
            hasEnded = true;
        }

        return hasEnded;
    }

    private bool unFade() 
    {
        bool hasEnded = false;

        if (fadeToBlackImage.color.a > 0f)
        {
            fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, fadeToBlackImage.color.a - 0.8f * Time.deltaTime);
        }
        else
        {
            hasEnded = true;
        }

        return hasEnded;
    }
}