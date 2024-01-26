using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    public float speed = 1.0f;
    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            // Para que el enemigo siga al jugador (se mueva hacia su posición)
            enemyRb.AddForce(lookDirection * speed);
            
        }
        // Eliminamos al enemigo cuando salga de la zona
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }

    }
}
