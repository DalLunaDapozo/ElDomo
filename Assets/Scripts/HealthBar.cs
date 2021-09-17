using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI hp;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        hp.SetText(health.ToString());
    }


    public void SetHealth(int health)
    {
        slider.value = health;
        hp.SetText(health.ToString());
    }

}
