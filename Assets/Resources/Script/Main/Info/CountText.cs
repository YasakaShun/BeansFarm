using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeansFarm
{
    public class CountText : MonoBehaviour
    {

        void Start()
        {

        }

        void Update()
        {
            int count = Player.Manager.AllPlayer().Length;

            GetComponent<Text>().text = count + " / " + GameManager.Instance.StageManager.goalCount;
        }
    }
}
