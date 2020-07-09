using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenChaptetSelectScene : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Open());
    }

    IEnumerator Open()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("ChapterSelect");
    }

}
