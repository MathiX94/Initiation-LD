using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlatformScript : MonoBehaviour
{
    public Transform position_1;
    public Transform position_2;

    public float speed = 20.0f;
    public float scaleSpeed = 1.0f;

    private List<GameObject> touchingObjects = new();
    public bool isActive = false;

    private float distanceTraveled;
    private Vector3 minCoords,maxCoords,minScale,maxScale;
    private Vector3 originScale;

    void Start()
    {
        distanceTraveled = (position_1.position-position_2.position).magnitude;

        minCoords = Vector3.Min(position_1.position,position_2.position);
        maxCoords = Vector3.Max(position_1.position,position_2.position);

        originScale = this.transform.localScale;
        minScale = Vector3.Min(position_1.localScale,position_2.localScale);
        maxScale = Vector3.Max(position_1.localScale,position_2.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isActive) {
            //Stuff for the scale
            this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, Vector3.Scale(position_2.localScale,originScale), scaleSpeed * Time.deltaTime);

            //Position
            Vector3 oldPosition = transform.position;
            Vector3 newPosition = Vector3.MoveTowards(this.transform.position, position_2.position, speed * Time.deltaTime);
            newPosition = new Vector3(Mathf.Clamp(newPosition.x,minCoords.x,maxCoords.x),Mathf.Clamp(newPosition.y,minCoords.y,maxCoords.y),Mathf.Clamp(newPosition.z,minCoords.z,maxCoords.z));
            transform.position = newPosition;
            Vector3 displacement = transform.position - oldPosition;

            foreach(GameObject obj in touchingObjects)
            {
                if (obj.transform.position.y <= transform.position.y) continue;

                obj.transform.position += displacement;
                if(obj.GetComponent<CharacterController2D>())
                    obj.GetComponent<CharacterController2D>().Translate(displacement);
            }

        } else {
            //Stuff for the scale
            this.transform.localScale = Vector3.MoveTowards(this.transform.localScale, Vector3.Scale(position_1.localScale,originScale), scaleSpeed * Time.deltaTime);

            //Position            
            Vector3 oldPosition = transform.position;
            Vector3 newPosition = Vector3.MoveTowards(this.transform.position, position_1.position, speed * Time.deltaTime);
            newPosition = new Vector3(Mathf.Clamp(newPosition.x,minCoords.x,maxCoords.x),Mathf.Clamp(newPosition.y,minCoords.y,maxCoords.y),Mathf.Clamp(newPosition.z,minCoords.z,maxCoords.z));

            transform.position = newPosition;

            Vector3 displacement = transform.position - oldPosition;
            foreach(GameObject obj in touchingObjects)
            {
                if (obj.transform.position.y <= transform.position.y) continue;

                obj.transform.position += displacement;
                if(obj.GetComponent<CharacterController2D>())
                    obj.GetComponent<CharacterController2D>().Translate(displacement);            
            }
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
}
