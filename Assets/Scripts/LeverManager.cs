using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    public bool isPlatform = false;
    public GameObject toMove;
    public float toMoveSpeed = 1.0f;

    private Vector3 originPos;
    private Vector3 targetPos;
    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        originPos = this.transform.position;
        targetPos = toMove.transform.Find("target").gameObject.transform.position;
    }

    void asDoor()
    {
        toMove.transform.position = Vector3.Lerp(toMove.transform.position, targetPos, Time.deltaTime * toMoveSpeed);
        if (this.transform.localScale.y > 0)
            this.transform.localScale -= new Vector3(0, 0.05f, 0);
    }

    void asPlatform()
    {
        toMove.SendMessage("activatePlatform", true);
        if (this.transform.localScale.y > 0)
            this.transform.localScale -= new Vector3(0, 0.05f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
            if (isPlatform)
                asPlatform();
            else asDoor();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
            triggered = !triggered;
    }
}
