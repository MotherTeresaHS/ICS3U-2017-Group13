using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    [SerializeField]
    private float fillAmout;

    [SerializeField]
    private Image content;

    public float MaxValue { get; set; }

    public float Value
    {
        set
        {
            // 0 is the least amout of health
            // 0,1 is the fill amount range
            fillAmout = Map(value, 0, MaxValue, 0, 1);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleBar();
	}

    private void HandleBar()
    {
        if (fillAmout != content.fillAmount)
        {
            content.fillAmount = fillAmout;
        }
        
        
        

    }
    private float Map(float value, float inMin, float inMax,float outMin, float outMax )
    {
        // takes current health and changes it between a vaule 1 - 0
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        // (78 -0) * (1 -0) /(230 - 0) + 0
        // 78 * 1 /230 =0.339
    }
}
