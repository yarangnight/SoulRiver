using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitlePress2Start : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
