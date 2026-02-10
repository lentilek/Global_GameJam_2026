using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private PlayerStats psStart, psPlayer;

    private void Start()
    {
        psPlayer.SetUp(psStart, psPlayer);
        Cursor.visible = true;
    }

    public void StartGame()
    {
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
