using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private UIBarController manaBar;
    [SerializeField] private GameObject plumeScore;
    [SerializeField] private Text plumeScoreText;
    [SerializeField] private GameObject rockScore;
    [SerializeField] private Text rockScoreText;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameOver gameOverScreen;
    private bool isPlumeCollectableVisible;
    private bool isRockCollectableVisible;

    public static HUDManager instance;
    public GameOver GameOverScreen => gameOverScreen;
    public GameObject VictoryScreen => victoryScreen;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        IsPlumeScoreVisible(false);
        IsRockScoreVisible(false);
        victoryScreen.SetActive(false);
    }
    private void Start()
    {
        LevelManager.instance.OnPlayerAssing += PlayerAssing;
    }

    public void UpdateMana(int currentMana, int maxMana)
    {
        manaBar.UpdateLifeBar(currentMana, maxMana);
    }

    public void UpdatePlumeScore(int newscore)
    {
        plumeScoreText.text = $"x{newscore.ToString()}";
        if (!isPlumeCollectableVisible)
            IsPlumeScoreVisible(true);
    }

    public void UpdateRockScore(int newscore)
    {
        rockScoreText.text = $"x{newscore.ToString()}";
        if (!isRockCollectableVisible)
            IsRockScoreVisible(true);
    }

    public void IsPlumeScoreVisible(bool value)
    {
        isPlumeCollectableVisible = value;
        plumeScore.SetActive(value);
    }

    public void IsRockScoreVisible(bool value)
    {
        isRockCollectableVisible = value;
        rockScore.SetActive(value);
    }

    public void IsParticleSystemVisible(bool value)
    {
        manaBar.PlayParticles(value);
    }

    public void PlayerAssing()
    {
        LevelManager.instance.Player.MagicController.UpdateMana += UpdateMana;
        LevelManager.instance.OnPlayerAssing -= PlayerAssing;
    }
}
