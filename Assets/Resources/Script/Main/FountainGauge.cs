using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FountainGauge : MonoBehaviour
{
    private FountainOld fountain;
    private Slider slider;

	void Start()
    {
        fountain = this.GetComponentInParent<FountainOld>();
        slider = transform.Find("Bar").GetComponent<Slider>();

        Debug.Assert(fountain != null);
        Debug.Assert(slider != null);

        UpdateValue();
	}
	
	void Update()
    {
        UpdateValue();
        transform.rotation = Camera.main.transform.rotation;
	}

    private void UpdateValue()
    {
        slider.value = fountain.power / fountain.MaxPower;
    }
}
