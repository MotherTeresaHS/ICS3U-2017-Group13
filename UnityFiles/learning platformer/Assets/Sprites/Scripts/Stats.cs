using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats
{
    [SerializeField]
    private HealthBar bar;

    [SerializeField]
    private float maxValue;

    [SerializeField]
    private float currentValue;

    [SerializeField]
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            
            this.currentValue = value;
            bar.Value = currentValue;
        }
    }

    public float MaxValue
    {
        get
        {
            return maxValue;
        }

        set
        {

            this.maxValue = value;
            bar.MaxValue = maxValue;
        }
    }
    public void Initialize()
    {
        this.MaxValue = maxValue;
        this.CurrentValue = currentValue;
    }
}
