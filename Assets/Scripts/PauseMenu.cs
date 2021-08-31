using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject optionScreen, pauseScreen, loadingScreen, loadingIcon, dots;
    public string mainMenuScene;
    private bool isPaused;
    public Text loadingText;

    void Start()
    {

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            PauseUnpause();
        }
    }

    public void PauseUnpause(){
        if(!isPaused){
            pauseScreen.SetActive(true);
            isPaused=true;
            Time.timeScale=0f;
        }
        else{
            pauseScreen.SetActive(false);
            isPaused=false;
            Time.timeScale=1f;
        }
    }

    public void OpenOptions(){
        optionScreen.SetActive(true);
    }

    public void CloseOptions(){
        optionScreen.SetActive(false);
    }

    public void QuitToMainMenu(){
        //SceneManager.LoadScene(mainMenuScene);

        StartCoroutine(LoadStart());
    }

    public IEnumerator LoadStart(){
        loadingScreen.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuScene);

        asyncLoad.allowSceneActivation=false;

        while(!asyncLoad.isDone){
            if(asyncLoad.progress >=.9f){
                loadingText.text="Press any key to continue";
                loadingIcon.SetActive(false);
                dots.SetActive(false);

                if(Input.anyKeyDown){
                    Time.timeScale=1f;
                    asyncLoad.allowSceneActivation=true;

                }
            }

            yield return null;
        }
    }
}
