using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string Scene;
    public void SceneChange(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
