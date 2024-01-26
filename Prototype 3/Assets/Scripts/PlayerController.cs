using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Utilizamos rigidbody para aplicarle fuerza al personaje
    private Rigidbody playerRb;
    private Animator playerAnim;
    // Para que el personaje tenga una fuente de sonido
    private AudioSource playerAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtyParticle;
    // Variables para añadir efectos de sonido
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        // Obtenemos el componente Rigidbody del jugador y le aplicamos una fuerza hacia arriba
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        // Physics.gravity = Physics.gravity * gravityModifier
        Physics.gravity *= gravityModifier;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            // Le aplicamos una fuerza de impulso
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            // Añadimos una animación de salto
            playerAnim.SetTrigger("Jump_trig");
            // Paramos las particulas de tierra cuando salta
            dirtyParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    // Si el personaje está tocando el suelo
    private void OnCollisionEnter(Collision collision)
    {
        // Si el objeto con el que colisiona tiene la etiqueta "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtyParticle.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            gameOver = true;
            // Añadimos animación de muerte
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            // Para activar la explosion cuando el jugador choque
            explosionParticle.Play();
            dirtyParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
