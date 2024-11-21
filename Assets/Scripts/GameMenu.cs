using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject _field_creator;

    void Continue()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
        _field_creator.GetComponent<FieldCreator>()._update_is_allowed = true;
    }
    void Pause()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        _field_creator.GetComponent<FieldCreator>()._update_is_allowed = false;
    }
    public void ContinuePause()
    {
        if (_field_creator.GetComponent<FieldCreator>()._update_is_allowed)
        {
            Pause();
        }
        else
        {
            Continue();
        }
    }
}
