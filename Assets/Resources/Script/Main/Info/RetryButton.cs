using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BeansFarm
{
    public class RetryButton : MonoBehaviour
    {
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            // TODO: 現在シーンを取得して設定する
            SceneManager.LoadScene("Level0");
        }

    }

}
