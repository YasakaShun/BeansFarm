using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BeansFarm
{
    public class TitleButton : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            SceneManager.LoadScene("Title");
        }

    }

}
