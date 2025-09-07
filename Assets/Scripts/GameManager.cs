using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource slashingSound;
    public Text scoreText;
    private int score;
    //spawner & blade -to disable when game ends
    private Blade blade;
    private Spawner spawner;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        PlayerPrefs.SetString("EndScore", "0");
        //reset spawner, blade, & score
        blade.enabled = true;
        spawner.enabled = true;
        score = 0;
        scoreText.text = score.ToString();
        ClearScene(); //clear from pervious game
    }

    public void ClearScene()
    {
        Fruits[]  fruits = FindObjectsOfType<Fruits>(); 
        foreach (Fruits fruit in fruits)
        {
            Destroy(fruit.gameObject);
        }

        Bomb[]  bombs = FindObjectsOfType<Bomb>(); 
        foreach (Bomb bomb in bombs)
        {
            Destroy(bomb.gameObject);
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        PlayerPrefs.SetString("EndScore", "Your Score: " + scoreText.text);
    }

    public void Explode()
    {
        //disable game play since game over
        blade.enabled = false;
        spawner.enabled = false;
        //change scene
        slashingSound.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
