using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;
    public ResItem[] resolutions;
    public int selectedResolution;
    public Text resolutionLabel;
    public AudioMixer myMixer;
    public Slider masterSlider, musicSlider, sfxSlider;
    public Text masterLabel, musicLabel, sfxLabel;
    public AudioSource sfxloop;

    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;
        
        if(QualitySettings.vSyncCount==0){
            vsyncTog.isOn=false;
        }
        else{
            vsyncTog.isOn=true;
        }

        //search for resolution in list
        bool foundResolution = false;

        for(int i=0;i<resolutions.Length-1;i++){
            if(Screen.width==resolutions[i].horizontal && Screen.height==resolutions[i].vertical){
                foundResolution=true;
                selectedResolution=i;
                UpdateResLabel();
            }
        }

        if(!foundResolution){
            resolutionLabel.text=Screen.width.ToString() + " x " + Screen.height.ToString();
        }

        if(PlayerPrefs.HasKey("MasterVolume")){
            myMixer.SetFloat("MasterVolume",PlayerPrefs.GetFloat("MasterVolume"));
            masterSlider.value=PlayerPrefs.GetFloat("MasterVolume");
        }

        if(PlayerPrefs.HasKey("MusicVolume")){
            myMixer.SetFloat("MusicVolume",PlayerPrefs.GetFloat("MusicVolume"));
            musicSlider.value=PlayerPrefs.GetFloat("MusicVolume");

        }

        if(PlayerPrefs.HasKey("SFXVolume")){
            myMixer.SetFloat("SFXVolume",PlayerPrefs.GetFloat("SFXVolume"));
            sfxSlider.value=PlayerPrefs.GetFloat("SFXVolume");
        }

        masterLabel.text=(masterSlider.value+80).ToString();
        musicLabel.text=(masterSlider.value+80).ToString();
        sfxLabel.text=(sfxSlider.value+80).ToString();

    }

    void Update()
    {
        
    }

    public void ResLeft(){

        selectedResolution--;
        if(selectedResolution<0){
            selectedResolution=0;
        }

        UpdateResLabel();

    }

    public void ResRight(){
    
        selectedResolution++;
        if(selectedResolution > resolutions.Length - 1){
            selectedResolution = resolutions.Length - 1;
        }
        UpdateResLabel();
    }

    public void ApplyGraphics(){

        //Apply Fullscreen
        //Screen.fullScreen = fullscreenTog.isOn;

        //Apply VSync
        if(vsyncTog.isOn){

            QualitySettings.vSyncCount = 1;
        }

        else{
            QualitySettings.vSyncCount = 0;
        }

        //Set Resolution
        Screen.SetResolution(resolutions[selectedResolution].horizontal,resolutions[selectedResolution].vertical,fullscreenTog.isOn);
    
    }

    public void SetMasterVolume(){

        masterLabel.text = (masterSlider.value + 80).ToString();
        myMixer.SetFloat("MasterVolume",masterSlider.value);
        PlayerPrefs.SetFloat("MasterVolume",masterSlider.value);

    }

    public void SetMusicVolume(){

        musicLabel.text = (musicSlider.value + 80).ToString();
        myMixer.SetFloat("MusicVolume",musicSlider.value); 
        PlayerPrefs.SetFloat("MusicVolume",musicSlider.value);

    }

    public void SetSFXVolume(){
    
        sfxLabel.text = (sfxSlider.value + 80).ToString();
        myMixer.SetFloat("SFXVolume",sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume",sfxSlider.value); 
    }

    public void UpdateResLabel(){

        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    
    }

    public void PlaySFXLoop(){
    
        sfxloop.Play();

    }

    public void StopSFXLoop(){
    
        sfxloop.Stop();
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
