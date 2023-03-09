using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject toMove;
    public float toMoveSpeed = 1.0f;
    public bool isConstant = false;
    public GameObject target;

    private Vector3 originPos;
    private Vector3 originButtonScale;
    private Vector3 targetPos;
    private bool triggered = false;
    private GameObject child;
    private float timeOffset = 0;
    private List<GameObject> touchingObjects = new();
    private Vector3 position1;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = target.transform.position;
        originPos = toMove.transform.position;
        child = this.transform.Find("sprite").gameObject;
        originButtonScale = child.transform.localScale;
        position1 = toMove.transform.position;
    }

    void platformActivated()
    {
        Vector3 oldPosition = toMove.transform.position;
        toMove.transform.position = Vector3.Lerp(position1, targetPos, Mathf.PingPong((Time.time-timeOffset) * toMoveSpeed, 1.0f));

        Vector3 displacement = toMove.transform.position - oldPosition;

        foreach(GameObject obj in touchingObjects)
        {
            if (obj.transform.position.y <= toMove.transform.position.y) continue;

            obj.transform.position += displacement;
            if(obj.GetComponent<CharacterController2D>())
                obj.GetComponent<CharacterController2D>().Translate(displacement);
        }

    }

    void asConstant()
    {
        if (triggered) {
            platformActivated();
            if (child.transform.localScale.y > 0)
                child.transform.localScale -= new Vector3(0, 0.05f, 0);
        } else {
            timeOffset += Time.deltaTime;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touchingObjects.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touchingObjects.Remove(collision.gameObject);
    }
}
