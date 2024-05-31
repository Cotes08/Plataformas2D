using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private AudioSource audioSource;
    private List<Enemy> enemies;
    private int numEnemies;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            gameObject.GetComponent<AudioListener>().enabled = false;
            audioSource = GetComponent<AudioSource>();
        }
    }

    //El GameManager lleva la cuenta de todos los enemigos para ganar el juego
    void Start()
    {
        //Obtenemos una referencia de cada enemigo
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
        numEnemies = enemies.Count;
    }

    //Se activara el audiolistener global si el personaje muere y por ende pierde su audiolistener
    public void ActivateAudioListener()
    {
        gameObject.GetComponent<AudioListener>().enabled = true;
    }

    public void RestartLevel()
    {
        StartCoroutine(RestartLose());
    }

    public void EnemyDestroyed()
    {
        numEnemies--;
        if (numEnemies == 0)
        {
            StartCoroutine(RestartWin());
        }
    }

    IEnumerator RestartLose()
    {
        audioSource.mute = true;
        AudioManager.Instance.PlayMusic("GameOverSound");
        yield return new WaitForSeconds(2);
        AudioManager.Instance.PlayMusic("GameOver");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator RestartWin()
    {
        audioSource.mute = true;
        PlayerInterface.Instance.ShowYouWin();
        AudioManager.Instance.PlayMusic("WinSound");
        yield return new WaitForSeconds(3);
        AudioManager.Instance.PlayMusic("YouWin");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}
