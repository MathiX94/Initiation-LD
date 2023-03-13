using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever2Manager : MonoBehaviour
{
    public GameObject toMove;
    public GameObject model;
    private bool triggered = false;
    private Vector3 originButtonScale;
    
    void Start()
    {
        originButtonScale = model.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // Animation
        if (triggered) {
            if (model.transform.localScale.y > 0.00)
                model.transform.localScale -= new Vector3(0, 0.05f, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            triggered = true;
            toMove.SendMessage("activatePlatform", true);
        }
    }

}
