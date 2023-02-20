using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static int level=0;
    public void PlayGame(int _screen)
    {
        SceneManager.LoadScene(_screen);
    }
    public void ExitGame()
    {
        Application.Quit();
        PlayerPrefs.DeleteAll();
    }
    public void Contin()
    {
        maingame.readfile = true;
    }
    public void LevelGame(int idb)
    {
        if (idb==1) level = 1;
        else if (idb == 2) level = 2;
        else level = 3;
    }
}
