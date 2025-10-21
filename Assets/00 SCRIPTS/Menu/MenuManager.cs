using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject guidePanel01;
    public GameObject guidePanel02;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }
    
    public void NewGame()
    {
        if (!guidePanel01.activeSelf & !guidePanel02.activeSelf)
            SceneManager.LoadScene("Level1");
    }

    public void GuideGame()
    {
        guidePanel01.SetActive(true);
    }

    public void QuitGuide()
    {
        guidePanel01.SetActive(false);
        guidePanel02.SetActive(false);
    }

    public void NextGuide() 
    {
        guidePanel01.SetActive(false);
        guidePanel02.SetActive(true);
    }

    public void BackGuide()
    {
        guidePanel01.SetActive(true);
        guidePanel02.SetActive(false);
    }

    public void QuitGame()
    {
        if (guidePanel01 == null & guidePanel02 == null)
            Application.Quit();
    }
}
