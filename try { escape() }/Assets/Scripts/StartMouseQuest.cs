using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartMouseQuest : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("dkfndkl");
        SceneManager.LoadScene("Maze2D");  
    }
}
