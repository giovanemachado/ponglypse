using RouteTeamStudio.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RouteTeamStudio
{
    public class BarsManager : Controller
    {
        public Slider HealthSlider => _healthSlider;
        public Slider HitBallSlider => _hitBallSlider;
        public Slider BallSlider => _ballSlider;

        [SerializeField] Slider _healthSlider;
        [SerializeField] Slider _hitBallSlider;
        [SerializeField] Slider _ballSlider;

        public void SetMaxValue(Slider slider, int value)
        {
            slider.maxValue = value;
            slider.value = value;
        }

        public void UpdateValue(Slider slider, int value)
        {
            slider.value = value;
        }
    }
}
