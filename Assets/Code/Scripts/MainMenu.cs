using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] Menus;

    private void Start()
    {
        Menus[0].SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    public void Controls()
    {
        Menus[0].SetActive(false); 
        Menus[1].SetActive(true);
    }

    public void Back()
    {
        Menus[0].SetActive(true);
        Menus[1].SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
