using System;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private int currentHealth = 6, maxHealth = 6;
    [SerializeField] private float invincibilityDurationInSeconds = 2f;
    private float _invincibilityCounter = 0f;
    private HealthBarController _healthBarController;
    private SpriteRenderer _spriteRendererRef;
    private PlayerController _playerControllerRef;

    private void Start()
    {
        _healthBarController = FindObjectOfType<HealthBarController>();
        _spriteRendererRef = GetComponent<SpriteRenderer>();
        _playerControllerRef = GetComponent<PlayerController>();
        _invincibilityCounter = -1f;
    }

    private void FixedUpdate()
    {
        if (_invincibilityCounter > 0)
        {
            _invincibilityCounter -= Time.deltaTime;
            if (_invincibilityCounter <= 0)
            {
                var spriteColor = _spriteRendererRef.color;
                _spriteRendererRef.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if (_invincibilityCounter > 0.001f) return;

        currentHealth -= 1;
        _healthBarController.UpdateHealthDisplay();
        _invincibilityCounter = invincibilityDurationInSeconds;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            var spriteColor = _spriteRendererRef.color;
            _spriteRendererRef.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, .5f);
            _playerControllerRef.Knockback();
        }
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}