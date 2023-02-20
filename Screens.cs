using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Screens : MonoBehaviour
{
    public void NextLevel(int _screen)
    {
        SceneManager.LoadScene(_screen);
    }
}
