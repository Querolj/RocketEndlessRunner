using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class IngameUIManager : MonoBehaviour
{
    public GameObject _game_over_ui;

    public static IngameUIManager Instance;

    private void Awake()
    {
        Instance = this;
        _game_over_ui.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    //------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------------------

    public void GameOver()
    {
        _game_over_ui.SetActive(true);
    }
}
