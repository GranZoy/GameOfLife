using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    [SerializeField] public bool is_alive;

    public void MakeAlive()
    {
        is_alive = true;
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
    }

    public void MakeDead()
    {
        is_alive = false;
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.white;
    }

    public void ChangeLiveness()
    {
        if (is_alive)
        {
            MakeDead();
        }
        else
        {
            MakeAlive();
        }
    }

    private void OnMouseUpAsButton()
    {
        ChangeLiveness();
    }
}
