using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour
{
    public static PlayerInterface Instance;
    [SerializeField] private Image fillBar;
    [SerializeField] private GameObject youWin;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject powerUp;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    //Actualizamos la vida constantemente y si llega a 0 mostramos el texto de game over
    public void UpdateHealth(float health)
    {
        // Actualizamos la barra de vida
        fillBar.fillAmount = health / 100;
        if (health <= 0)
        {
            gameOver.SetActive(true);
        }
    }

    //Mostramos el texto de powerUp
    public void ShowPowerUp()
    {
        StartCoroutine(PowerUp());
    }

    //Mostramos el texto de youwin
    public void ShowYouWin()
    {
        youWin.SetActive(true);
    }

    public IEnumerator PowerUp()
    {
        powerUp.SetActive(true);
        yield return new WaitForSeconds(2);
        powerUp.SetActive(false);
    }

}
