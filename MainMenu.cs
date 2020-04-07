using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator animator;

    public void StartExperience ()
    {
        animator.SetTrigger("FadeOut");
    }

    public void StartExperience2 ()
    {
        SceneManager.LoadScene("jase_buoy_test");
    }

    public void QuitExperience ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
