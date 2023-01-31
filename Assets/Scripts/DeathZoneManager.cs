using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D pCollision)
    {
        if (pCollision.gameObject.CompareTag("Player"))
        {
            pCollision.gameObject.GetComponent<CharacterController2D>().Die();
        }
    }
}
