using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public GameObject optionsScreen, loadingScreen, loadingIcon, dots;
    public Text loadingText;

    void Start()
    {
        optionsScreen.SetActive(false);
    }

    void Update()
    {
        
    }

    public void StartGame(){

        //SceneManager.LoadScene(firstLevel);
        StartCoroutine(LoadMain());

    }

    public void OpenOptions(){

        optionsScreen.SetActive(true);
    
    }

    public void CloseOptions(){

        optionsScreen.SetActive(false);
    
    }

    public void QuitGame(){
        Application.Quit();
    }

    public IEnumerator LoadMain(){
        loadingScreen.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(firstLevel);

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
