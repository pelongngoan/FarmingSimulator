using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunc : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {

    }
    public void OutMenu()
    {
        SceneManager.LoadScene(1);
    }
}
