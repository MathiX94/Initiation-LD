using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTrigger : MonoBehaviour
{
    public Text text;
    public GameObject triggerBox;

    private void Start()
    {
        text.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            text.enabled = true;
            Destroy(text, 5);
            Destroy(triggerBox, 5);
        }
    }
}
