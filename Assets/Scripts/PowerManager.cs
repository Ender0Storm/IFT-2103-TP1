
using UnityEngine;
using UnityEngine.UI;

public class PowerManager : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Graphic powerImage;

    public void SetPower(float power)
    {
        slider.value = power;
        gradient.Evaluate(power/2);
        powerImage.color = gradient.Evaluate(slider.normalizedValue);
    }
}