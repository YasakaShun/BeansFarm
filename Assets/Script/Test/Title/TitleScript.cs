using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
	void OnGUI()
    {
        if (Input.GetButtonUp("Submit"))
        {
            Application.LoadLevel("TestStage");
        }
	}
}
