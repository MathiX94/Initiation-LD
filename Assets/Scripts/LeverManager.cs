using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverManager : MonoBehaviour
{
    public GameObject toMove;
    private Vector3 targetPos;
    private bool triggered = false;
    public float toMoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = toMove.transform.Find("target").gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered) {
            toMove.transform.position = Vector3.Lerp(toMove.transform.position, targetPos, Time.deltaTime * toMoveSpeed);
            if (this.transform.localScale.y > 0)
                this.transform.localScale -= new Vector3(0, 0.05f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player")
            triggered = !triggered;
    }
}
