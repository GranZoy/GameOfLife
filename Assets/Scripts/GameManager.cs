using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _start_menu;
    [SerializeField] GameObject _game_menu;

    private void Start()
    {
        _game_menu.SetActive(false);
    }
    public void Play()
    {
        _start_menu.SetActive(false);
        _game_menu.SetActive(true);
        FindFirstObjectByType<FieldCreator>().GetComponent<FieldCreator>()._update_is_allowed = true;
    }
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
