using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeansFarm
{
    public class Result : MonoBehaviour
    {
        void Start()
        {
            mIsVisible = false;
            GetComponent<Canvas>().enabled = false;
        }

        void Update()
        {
            if (mIsVisible)
            {
                return;
            }

            if (!GameManager.Instance.StageManager.IsResult)
            {
                return;
            }

            mIsVisible = true;
            GetComponent<Canvas>().enabled = true;
        }

        private bool mIsVisible;
    }
}
