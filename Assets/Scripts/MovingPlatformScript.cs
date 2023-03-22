using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public Transform position_1;
    public Transform position_2;

    public float speed = 1.0f;

    private List<GameObject> touchingObjects = new();
    public bool isActive = true;

    [System.NonSerialized] public float timeOffset = 0;
    private float distanceTraveled;

    void Start()
    {
        distanceTraveled = (position_1.position-position_2.position).magnitude;
        timeOffset = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
            Vector3 oldPosition = transform.position;
            transform.position = Vector3.Lerp(position_1.position, position_2.position, Mathf.PingPong((Time.time-timeOffset) * speed / distanceTraveled, 1.0f));

            Vector3 displacement = transform.position - oldPosition;

            foreach(GameObject obj in touchingObjects)
            {
                if (obj.transform.position.y <= transform.position.y) continue;

                obj.transform.position += displacement;
                if(obj.GetComponent<CharacterController2D>())
                    obj.GetComponent<CharacterController2D>().Translate(displacement);
            }
        } else {
            timeOffset += Time.deltaTime;
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

    public void activatePlatform(bool val)
    {
        isActive = val;
    }

    // public void resetPlatform()
    // {
    //     timeOffset = Time.time;
    // }
}
