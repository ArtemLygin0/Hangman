using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartLevel : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void Load()
    {
        SceneManager.LoadSceneAsync(_sceneName);
    }
}
