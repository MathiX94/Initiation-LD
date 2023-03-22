using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndingTriggerSplit : MonoBehaviour
{
    [SerializeField]
    public UnityEvent<ColoredCharacter> OnEntered;
    [SerializeField]
    public UnityEvent<ColoredCharacter> OnExited;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnEntered.Invoke(collision.GetComponent<ColoredCharacter>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnExited.Invoke(collision.GetComponent<ColoredCharacter>());
        }
    }
}
