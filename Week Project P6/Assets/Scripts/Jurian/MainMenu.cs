using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ButtonType
{
    Start,
    ExitGame,
}

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ButtonType m_Type = ButtonType.Start;

    public void OnClick()
    {
        if (m_Type == ButtonType.ExitGame)
            Application.Quit();
        else if (m_Type == ButtonType.Start)
            SceneManager.LoadSceneAsync(1);
    }
}