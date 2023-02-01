using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //[SerializeField] private Button newGameBtn;
    //[SerializeField] private Button creditsBtn;
    //[SerializeField] private Button optionsBtn;
    //[SerializeField] private Button ExitBtn;

    private void Awake()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public void OnNewGameClicked()
    {
        
        foreach(Transform obj in gameObject.GetComponentInChildren<Transform>())
        {
            obj.gameObject.SetActive(false);
        }
        SceneManager.LoadScene("TestScene", LoadSceneMode.Additive);
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void OnSceneUnloaded(Scene which)
    {
        if (which.name.Equals("TestScene"))
        {
            foreach (Transform obj in gameObject.GetComponentInChildren<Transform>())
            {
                obj.gameObject.SetActive(true);
            }
        }
    }
}
