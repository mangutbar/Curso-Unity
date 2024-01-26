using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float speed = 3.0f;
    public bool hasPowerup = false;
    private float powerupStrength = 20.0f;
    public GameObject powerupIndicator;
    public bool gameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        // Obtenemos los valores de Focal Point
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        // El jugador gira cuando gira Focal Point
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed);
        // Para activar el indicador de powerup en la posición de player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (playerRb.transform.position.y < -2)
        {
            gameOver = true;
            Destroy(gameObject);
            Destroy(powerupIndicator);
        }
    }

    // Cuando obtiene potenciador éste se elimina
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            // Para activar el indicador
            powerupIndicator.SetActive(true);

            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // Creamos un contador para deshabilitar el powerup
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // Si choca con el enemigo y tiene potenciador activado
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            // La posición del enemigo menos la del jugador para enviarlo asi esa dirección
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
