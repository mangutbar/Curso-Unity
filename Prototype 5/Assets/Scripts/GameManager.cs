using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Para actualizar el Score
using TMPro;
// Para obtener las escenas utilizadas
using UnityEngine.SceneManagement;
// Obtener las interfaces
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public bool isGameActive;
    private float spawnRate = 1.0f;
    private int score;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Creamos una corutina para que aparezcan objetivos aleatorios
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
        
    }

    // Actualizamos la puntuación
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Si se activa aparece la pantalla de gameover
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    // Función para reiniciar el juego
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Función para comenzar el juego
    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        score = 0;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        // Desactivamos la pantalla del título al iniciar el juego
        titleScreen.gameObject.SetActive(false);
    }
}
