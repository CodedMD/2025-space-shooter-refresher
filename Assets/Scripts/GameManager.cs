using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {

          SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
        Debug.Log("Game Over!");
    }
}
