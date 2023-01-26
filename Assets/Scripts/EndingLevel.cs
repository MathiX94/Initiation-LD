using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingLevel : MonoBehaviour
{
    [SerializeField]
    public string levelname;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D pCollision)
    {
        //if (pCollision.gameObject.CompareTag("Player"))
        //{
            SceneManager.LoadScene(levelname);
        //}
    }
}
