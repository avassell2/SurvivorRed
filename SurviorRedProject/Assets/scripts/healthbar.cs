﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sethealth(int health)
    {
        
        slider.value = health;
    }


    public void setmaxhealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
