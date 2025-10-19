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
        _energySlider.value = Mathf.RoundToInt(current);
        _energySlider.maxValue = max;
        _energyText.text = _energySlider.value + "/" + _energySlider.maxValue;
    }
}
