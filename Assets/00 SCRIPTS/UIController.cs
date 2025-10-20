using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private Slider _energySlider;
    [SerializeField] private TMP_Text _energyText;

    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TMP_Text _healthText;

    [SerializeField] private TMP_Text _scoreText;

    public GameObject pausePanel;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // cap nhat UI energy
    public void UpdateEnergySlider(float current, float max) 
    {
        _energySlider.maxValue = max;
        _energySlider.value = Mathf.RoundToInt(current);
        _energyText.text = _energySlider.value + "/" + _energySlider.maxValue;
    }

    // cap nhat UI health
    public void UpdateHealthSlider(float current, float max)
    {
        _healthSlider.maxValue = max;
        _healthSlider.value = Mathf.RoundToInt(current);
        _healthText.text = _healthSlider.value + "/" + _healthSlider.maxValue;
        //Debug.Log("lan 2" + _healthSlider.value);
    }

    // cap nhat UI score
    public void UpdateScoreText(int currentScore)
    {
        _scoreText.text = "Score: " + currentScore;
    }
}
