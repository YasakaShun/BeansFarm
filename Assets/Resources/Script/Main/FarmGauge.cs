using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FarmGauge : MonoBehaviour
{
    private Field.Cell cell;
    private Slider slider;

	void Start()
    {
        cell = this.GetComponentInParent<Field.Cell>();
        slider = transform.Find("Bar").GetComponent<Slider>();

        Debug.Assert(cell != null);
        Debug.Assert(cell.kind == Field.Cell.Kind.Farm);
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
        slider.value = cell.waterPower / Field.Farm.Constant.MaxWaterPower;
    }
}
