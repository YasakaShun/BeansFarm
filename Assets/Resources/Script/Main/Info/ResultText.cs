using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeansFarm
{
    public class ResultText : MonoBehaviour
    {
        void Start()
        {
            mIsVisible = false;
            GetComponent<Text>().enabled = false;
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
            GetComponent<Text>().enabled = true;
        }

        private bool mIsVisible;
    }
}
