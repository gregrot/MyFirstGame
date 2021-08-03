using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public Button startButton;
    void Start() 
    {
        var b = startButton.GetComponent<Button>();
        b.onClick.AddListener(StartButtonClicked);
    }
    void StartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
}
