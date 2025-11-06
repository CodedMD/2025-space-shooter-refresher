using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text gameOver_Text;
    [SerializeField]
    private Text restartText;
    private Player _player;
    [SerializeField]
    private GameManager gameManager;
    // Thruster power variables
    [SerializeField]
    private Slider _thrusterBar;
    [SerializeField]
    private TMP_Text _thrusterBarPrecentage;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        gameOver_Text.gameObject.SetActive(false);
        restartText.gameObject.SetActive(false);
        _scoreText.text = "Score: " + 0;
        if (gameManager == null)
        {
            Debug.LogError("Game Manager is NULL");
        }
    }

    // Update the score display
    public void UpdateScore(int playerScore) { 
        _scoreText.text = "Score:"+ playerScore;
    
    }
    // Update the lives image based on current lives
    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _livesSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();

        }
    }
    // Update the thruster bar and percentage text
    public void UpdateThrusterBar(float currentBoostLevel)
    {
        currentBoostLevel = Mathf.Clamp(currentBoostLevel, 0, 100);
        _thrusterBar.value = currentBoostLevel;
        _thrusterBarPrecentage.text = Mathf.RoundToInt(currentBoostLevel)  + "%";
    }

    public void GameOverSequence()
    {
        
        restartText.gameObject.SetActive(true);
        gameManager.GameOver();
        StartCoroutine(GameOverFlickerRoutine());
        
    }


    IEnumerator GameOverFlickerRoutine()
    {

        while (true)
        {
            gameOver_Text.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameOver_Text.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            gameOver_Text.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

}
