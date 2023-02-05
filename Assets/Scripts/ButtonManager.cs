using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject toMove;
    public float toMoveSpeed;
    public bool isConstant = false;

    private Vector3 originPos;
    private Vector3 originButtonScale;
    private Vector3 targetPos;
    private bool triggered = false;
    private GameObject child;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = toMove.transform.Find("target").gameObject.transform.position;
        originPos = toMove.transform.position;
        child = this.transform.Find("sprite").gameObject;
        originButtonScale = child.transform.localScale;
    }

    void asConstant()
    {
        if (triggered) {
            toMove.SendMessage("activatePlatform", true);
            if (child.transform.localScale.y > 0)
                child.transform.localScale -= new Vector3(0, 0.05f, 0);
        } else {
            toMove.SendMessage("activatePlatform", false);
            if (child.transform.localScale.y < originButtonScale.y)
                child.transform.localScale += new Vector3(0, 0.05f, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isConstant)
            asConstant();
        else {
            if (triggered) {
                toMove.transform.position = Vector3.Lerp(toMove.transform.position, targetPos, Time.deltaTime * toMoveSpeed);
                if (child.transform.localScale.y > 0)
                    child.transform.localScale -= new Vector3(0, 0.05f, 0);
            } else {
                toMove.transform.position = Vector3.Lerp(toMove.transform.position, originPos, Time.deltaTime * toMoveSpeed);
                if (child.transform.localScale.y < originButtonScale.y)
                    child.transform.localScale += new Vector3(0, 0.05f, 0);
            }
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
