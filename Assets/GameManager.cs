using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int life = 30;
    public int keys = 0;

    public GameObject gameOverScreen;

    public int playerCurrentLife;
    public int playerLife;

    public TextMeshProUGUI lifeText;

    //Inventario
    public List<int> inventory = new List<int>();

    public int equipment;

    //Singleton
    private static GameManager _instance;

    public static GameManager Instance {  get { return _instance; } }

    public void Start()
    {
        playerLife = life;
        playerCurrentLife = playerLife;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void IsDead()
    {
        if (life <= 0)
        {
            gameOverScreen.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void LifeUpdate()
    {
        if (playerCurrentLife != playerLife)
        {
            playerLife = playerCurrentLife;
            life = playerLife;
            IsDead();
            lifeText.text = "vida:" + playerLife;
        }
    }

}
