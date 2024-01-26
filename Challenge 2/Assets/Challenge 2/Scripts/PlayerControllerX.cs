using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private bool enableDog = true;
    private float delaySpawn = 0.5f;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && enableDog)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            enableDog = false;
            Invoke("EnableDog", delaySpawn);
        }
    }

    // Para añadir delay al perro y no poderlo lanzar seguidos
    void EnableDog()
    {
        enableDog = true;
    }
}
