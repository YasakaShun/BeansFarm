using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleGameSystem : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("Level0");
        // Application.LoadLevel("Level0");
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
