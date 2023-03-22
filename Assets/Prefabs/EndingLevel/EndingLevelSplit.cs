using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingLevelSplit : MonoBehaviour
{
    [SerializeField]
    public string levelName;

    [SerializeField]
    private EndingTriggerSplit blueTrigger;

    [SerializeField]
    private EndingTriggerSplit orangeTrigger;

    float playersReady = 0;

    int playersInTrigger = 0;

    private void Start()
    {
        blueTrigger.OnEntered.AddListener(BlueEntered);
        blueTrigger.OnExited.AddListener(BlueExited);
        orangeTrigger.OnEntered.AddListener(OrangeEntered);
        orangeTrigger.OnExited.AddListener(OrangeExited);

    }

    private void BlueEntered(ColoredCharacter character)
    {
        if(character.color == ElementColor.Color1)
        {
            playersReady++;
            CheckEnd();
        }
    }

    private void BlueExited(ColoredCharacter character)
    {
        if (character.color == ElementColor.Color1)
        {
            playersReady--;
            CheckEnd();
        }
    }

    private void OrangeEntered(ColoredCharacter character)
    {
        if (character.color == ElementColor.Color2)
        {
            playersReady++;
            CheckEnd();
        }
    }

    private void OrangeExited(ColoredCharacter character)
    {
        if (character.color == ElementColor.Color2)
        {
            playersReady--;
            CheckEnd();
        }
    }

    private void CheckEnd()
    {
        if(playersReady >= 2)
        {
            SceneManager.LoadScene(levelName);
        }
    }
}
