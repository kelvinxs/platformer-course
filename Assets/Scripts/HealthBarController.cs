using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private GameObject healthIndicator;
    [SerializeField] private float healthIndicatorWidth, healthIndicatorDistance;
    [SerializeField] private Sprite fullHealthSprite, halfHealthSprite, noHealthSprite;
    private PlayerHealthController _playerHealthController;
    private List<GameObject> _healthIndicators;
    private int _numHealthIndicators = 3;

    // Start is called before the first frame update
    void Start()
    {
        _healthIndicators = new List<GameObject>();
        _playerHealthController = FindObjectOfType<PlayerHealthController>();
        _numHealthIndicators = Mathf.CeilToInt(_playerHealthController.GetMaxHealth() / 2f);

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (var i = 0; i < _numHealthIndicators; i++)
        {
            var indicator = Instantiate(healthIndicator, new Vector3((healthIndicatorWidth + healthIndicatorDistance) * i, 0, 0), Quaternion.identity);
            indicator.transform.SetParent(this.transform, false);
            _healthIndicators.Add(indicator);
        }

        UpdateHealthDisplay();
    }

    public void UpdateHealthDisplay()
    {
        var healthPerIndicator = 2;
        var health = _playerHealthController.GetCurrentHealth();
        for (var i = 0; i < _numHealthIndicators; i++)
        {
            var dif = health - healthPerIndicator * (i+1);
            if (dif >= 0)
            {
                _healthIndicators[i].GetComponent<Image>().sprite = fullHealthSprite;
            }
            else if (Mathf.Abs(dif) >= healthPerIndicator)
            {
                _healthIndicators[i].GetComponent<Image>().sprite = noHealthSprite;
            }
            else
            {
                _healthIndicators[i].GetComponent<Image>().sprite = halfHealthSprite;
            }
        }
    }
}