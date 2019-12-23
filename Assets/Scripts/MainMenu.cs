using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string level_1;
    public GameObject optionsScreen;
    public GameObject loadingScreen, loadingIcon;
    public Text loadingText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void start()
    {
        //SceneManager.LoadScene(level_1);
        StartCoroutine(loadStart());
    }

    public void openOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void closeOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }

    public IEnumerator loadStart()
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(level_1);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= .9f)
            {
                loadingText.text = "Press any key to continue";
                loadingIcon.SetActive(false);
                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                    Time.timeScale = 1f;
                }
            }
            yield return null;
        }
    }
}
