using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;
using UnityEngine.SceneManagement;

public class FieldCreator : MonoBehaviour
{
    [SerializeField] private int _n;
    [SerializeField] private int _m;
    [SerializeField] GameObject _cell_prefab;
    public GameObject[,] CellsMatrix;
    Dictionary<string, bool> _situation_was_in_past;
    public bool _update_is_allowed = false;
    float _threshold_time = 1f;
    float _time = 0f;
    bool _update_flag = false;

    void Start()
    {
        string tmp = "";
        for (int i = 0; i < _n * _m; ++i)
        {
            tmp += "0";
        }
        _situation_was_in_past = new Dictionary<string, bool> {{tmp, true}};
        CellsMatrix = new GameObject[_n, _m];
        for (int i = 0; i < _n; i++)
        {
            for (int j = 0; j < _m; j++)
            {
                CellsMatrix[i, j] = Instantiate(_cell_prefab,
                                                new Vector2((float)-(_n - 1) / 2 + i, (float)-(_m - 1) / 2 + j),
                                                _cell_prefab.transform.rotation);
            }
        }
    }

    void Update()
    {
        if (_update_is_allowed)
        {
            if (_update_flag)
            {
                int[,] alive_neighbours_cnt = new int[_n, _m];

                string situation_name = CountingNeighbours(alive_neighbours_cnt);

                if (_situation_was_in_past.ContainsKey(situation_name))
                {
                    SceneManager.LoadScene("SampleScene");
                    return;
                }
                _situation_was_in_past.TryAdd(situation_name, true);

                UpdateCells(alive_neighbours_cnt);
                _update_flag = false;
            }
            _time += Time.deltaTime;
            if (_time > _threshold_time)
            {
                _time = 0f;
                _update_flag = true;
            }
        }
    }

    public void CreateByRandom()
    {
        var rnd = new System.Random();
        for (int i = 0; i < _n; ++i)
        {
            for (int j = 0; j < _m; ++j)
            {
                if (Convert.ToBoolean(rnd.Next(2)))
                {
                    CellsMatrix[i, j].GetComponent<CellManager>().ChangeLiveness();
                }
            }
        }
    }

    public void ChangeSpeed(float speed)
    {
        _threshold_time = speed * speed - 0.9f;
    }

    string CountingNeighbours(int[,] alive_neighbours_cnt)
    {
        string situation_name = "";
        for (int i = 0; i < _n; i++)
        {
            for (int j = 0; j < _m; j++)
            {
                var neighbours = Neighbours(i, j);
                alive_neighbours_cnt[i, j] = 0;
                foreach (var tmp in neighbours)
                {
                    if (tmp.GetComponent<CellManager>().is_alive)
                    {
                        alive_neighbours_cnt[i, j]++;
                        situation_name += "1";
                    }
                    else
                    {
                        situation_name += "0";
                    }
                }
            }
        }
        return situation_name;
    }

    void UpdateCells(int[,] alive_neighbours_cnt)
    {
        for (int i = 0; i < _n; i++)
        {
            for (int j = 0; j < _m; j++)
            {
                if (alive_neighbours_cnt[i, j] == 3)
                {
                    CellsMatrix[i, j].GetComponent<CellManager>().MakeAlive();
                }
                if (alive_neighbours_cnt[i, j] < 2 || alive_neighbours_cnt[i, j] > 3)
                {
                    CellsMatrix[i, j].GetComponent<CellManager>().MakeDead();
                }
            }
        }
    }

    List<GameObject> Neighbours(int i, int j)
    {
        var neighbour = new List<GameObject>
        {
            CellsMatrix[Mod(i - 1, _n), Mod(j - 1, _m)],
            CellsMatrix[Mod(i - 1, _n), Mod(j,     _m)],
            CellsMatrix[Mod(i - 1, _n), Mod(j + 1, _m)],
            CellsMatrix[Mod(i,     _n), Mod(j - 1, _m)],
            CellsMatrix[Mod(i,     _n), Mod(j + 1, _m)],
            CellsMatrix[Mod(i + 1, _n), Mod(j - 1, _m)],
            CellsMatrix[Mod(i + 1, _n), Mod(j,     _m)],
            CellsMatrix[Mod(i + 1, _n), Mod(j + 1, _m)]
        };
        return neighbour;
    }

    int Mod(int a, int n)
    {
        if (a == -1) return n - 1;
        if (a == n) return 0;
        return a;
    }
}
