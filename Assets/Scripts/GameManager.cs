using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private bool win;

    public int Score { get => score; set => score = value; }
    public bool Win { get => win; set => win = value; }

    public static GameManager gameManager;

    private void Awake()
    {
        gameManager = this;
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameObject);
        }
            
        else Destroy(gameObject);
    }
}
