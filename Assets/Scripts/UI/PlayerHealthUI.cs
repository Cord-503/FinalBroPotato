using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Text healthText;

    [Header("Player Reference")]
    [SerializeField] private PlayerHealthManager playerHealth;

    void OnEnable()
    {
        playerHealth.OnHealthUpdate += UpdateHealthUI;
    }

    void OnDisable()
    {
        playerHealth.OnHealthUpdate -= UpdateHealthUI;
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{currentHealth} / {maxHealth}";
        }
    }
}
