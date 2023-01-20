using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeBar : MonoBehaviour
{
    [SerializeField] private Slider liferBar;

    public void SetSliderInitialValues(float maxLife, float life)
    {
        liferBar.maxValue = maxLife;
        liferBar.value = life;
    }

    public void SetLiferBarBehavior(bool mirrored)
    {
        if(mirrored)
            liferBar.direction = Slider.Direction.RightToLeft;
        else
            liferBar.direction = Slider.Direction.LeftToRight;
    }

    public void SetLifeBarValues(float value)
    {
        liferBar.value = value;
    }
}
