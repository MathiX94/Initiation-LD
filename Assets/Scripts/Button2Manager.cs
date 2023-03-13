using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button2Manager : MonoBehaviour
{
    public GameObject toMove;
    public GameObject model;
    private bool triggered = false;
    private Vector3 originButtonScale;
    public bool defaultState = false;
    
    void Start()
    {
        originButtonScale = model.transform.localScale;
        triggered = defaultState;
    }

    // Update is called once per frame
    void Update()
    {
        toMove.SendMessage("activatePlatform", triggered);
        
        // Animation
        if (triggered) {
            if (model.transform.localScale.y > 0.10)
                model.transform.localScale -= new Vector3(0, 0.05f, 0);
        } else {
            if (model.transform.localScale.y < originButtonScale.y)
                model.transform.localScale += new Vector3(0, 0.05f, 0);
            }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            triggered = !triggered;
            
            Debug.Log("triggered");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            triggered = !triggered;
            Debug.Log("not triggered");
        }
    }
}
