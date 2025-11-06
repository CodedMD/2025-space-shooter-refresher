using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    // Shield variables
    [SerializeField]
    private Sprite[] _shieldSprites;
    [SerializeField]
    private Image _shieldImages;
   
    // Ammo variables
    [SerializeField]
    private Sprite[] _ammoSprites;
    [SerializeField]
    private Image _ammoImages;

    // Lives variables
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesImage;

    // Game Over variables
    [SerializeField]
    private Text gameOver_Text;
    [SerializeField]
    private Text restartText;

    // Reference to Player and Game Manager
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
        _shieldImages.gameObject.SetActive(false);
        _ammoImages.gameObject.SetActive(true);
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
    // Update the ammo image based on current ammo count
    public void UpdateAmmo(int currentAmmo)
    {
        currentAmmo = Mathf.Clamp(currentAmmo, 0, 15);
        _ammoImages.sprite = _ammoSprites[currentAmmo];
        if (currentAmmo == 0)
        {
            
        }
       
    }
    // Update the shield image based on current shield strength
    public void UpdateShield(int currentShieldLevel)
    {
        _shieldImages.gameObject.SetActive(true);
        _shieldImages.sprite = _shieldSprites[currentShieldLevel];

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
