using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMainGame : MonoBehaviour
{
    [SerializeField] private LevelLoader loader;
    void Start()
    {
        Invoke("LoadMainMenu", 5f);
    }
    void LoadMainMenu()
    {
        loader.LoadLevel(1);
    }
}
