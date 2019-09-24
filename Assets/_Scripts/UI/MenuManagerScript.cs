using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    public Animator cameraAnim;
    public Transform mainMenu, optionsMenu, playMenu, extrasMenu, Credits, Quit;
    public AudioSource ClickSource;
    
    void Update()
    {

    }

    public void OpenOptions(bool clicked)
    {
        if (clicked)
        {
            ClickSource.Play();
            optionsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            optionsMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }
    public void Return(bool clicked)
    {
        if (clicked)
        {
            ClickSource.Play();
            mainMenu.gameObject.SetActive(clicked);
            optionsMenu.gameObject.SetActive(false);

        }
        else
        {
            mainMenu.gameObject.SetActive(clicked);
            optionsMenu.gameObject.SetActive(true);
        }
    }
    public void OpenPlayMenu(bool clicked)
    {
        if (clicked)
        {
            ClickSource.Play();
            cameraAnim.SetTrigger("MainPlay");
            playMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);

        }
        else
        {
            playMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }
    public void OpenExtrasMenu(bool clicked)
    {
        if (clicked)
        {
            ClickSource.Play();
            cameraAnim.SetTrigger("MainExtras");
            extrasMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            extrasMenu.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }
    public void ReturnFromPlay(bool clicked)
    {
        if (clicked)
        {
            ClickSource.Play();
            cameraAnim.SetTrigger("PlayMain");
            mainMenu.gameObject.SetActive(clicked);
            playMenu.gameObject.SetActive(false);
        }
        else
        {
            mainMenu.gameObject.SetActive(clicked);
            playMenu.gameObject.SetActive(true);
        }
    }
    public void ReturnFromExtras(bool clicked)
    {
        if (clicked)
        {
            ClickSource.Play();
            cameraAnim.SetTrigger("ExtrasMain");
            mainMenu.gameObject.SetActive(clicked);
            extrasMenu.gameObject.SetActive(false);
        }
        else
        {
            mainMenu.gameObject.SetActive(clicked);
            extrasMenu.gameObject.SetActive(true);
        }
    }
    public void LoadGameScene(string levelname)
    {
        ClickSource.Play();
        SceneManager.LoadScene(levelname);
    }
    public void ShowCredits(bool clicked)
    {
        if (clicked)
        {
            ClickSource.Play();
            Credits.gameObject.SetActive(clicked);
            extrasMenu.gameObject.SetActive(false);
        }
        else
        {
            Credits.gameObject.SetActive(clicked);
            extrasMenu.gameObject.SetActive(true);
        }
    }
    public void HideCredits(bool clicked)
    {
        if (clicked)
        {
            ClickSource.Play();
            extrasMenu.gameObject.SetActive(clicked);
            Credits.gameObject.SetActive(false);
        }
        else
        {
            extrasMenu.gameObject.SetActive(clicked);
            Credits.gameObject.SetActive(true);
        }
    }
    public void Exit(bool clicked)
    {
        if (clicked)
        {
            ClickSource.Play();
            Quit.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(false);
        }
        else
        {
            Quit.gameObject.SetActive(clicked);
            mainMenu.gameObject.SetActive(true);
        }
    }
    public void Yaya(bool clicked)
    {
        if (clicked)
        {
            // Quit.gameObject.SetActive(true);
            //quit application
            Application.Quit();
        }
        else
        {
            Quit.gameObject.SetActive(clicked);
        }
    }
    public void Nono(bool clicked)
    {
        if (clicked)
        {
            mainMenu.gameObject.SetActive(clicked);
            Quit.gameObject.SetActive(false);
        }
        else
        {
            mainMenu.gameObject.SetActive(clicked);
            Quit.gameObject.SetActive(true);
        }
    }
    public void OpenTrailer()
    {
        ClickSource.Play();
        Application.OpenURL("https://youtu.be/PT5Uv-js1I0");
    }
}