using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class AudioManager : MonoBehaviour
{
    public Audio[] words;
    public Audio[] clipXcasilla = new Audio[6];
    public AudioSource[] wordSource;
    [SerializeField] GameObject[] good;
    public int audioNumber;
    [SerializeField] GameObject panelAudio;
    [SerializeField] Sprite imagenAudioOk;
    GalleryPicker galleryPicker;
    public bool closeAudio = false;

 
    public void PlayWord()
    {
        GameObject s = EventSystem.current.currentSelectedGameObject;
        string audioName = s.transform.parent.GetComponent<TextMeshProUGUI>().text;
        Debug.Log(audioName);

        foreach (Audio audio in words) { 
            if (audio.name == audioName)
            {
                wordSource[0].clip=audio.clip;
                wordSource[0].Play();
            }
        }
    }

    private void Awake()
    {
        galleryPicker = FindObjectOfType<GalleryPicker>();
        
    }

    private void Update()
    {
        //print(closeAudio + " " + galleryPicker.audioPanelOpen);
        if (!galleryPicker.audioPanelOpen && closeAudio) { 
            closeAudio = false; 
            print("cierro panel"); }

    }

    public void SaveAudio()
    {
        GameObject s = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        string audioName = s.GetComponent<TextMeshProUGUI>().text;
        Debug.Log(audioName);
        //galleryPicker = FindObjectOfType<GalleryPicker>();

        foreach (Audio audio in words)
        {
            if (audio.name == audioName)
            {
                clipXcasilla[audioNumber-1].clip = audio.clip;
                good[audioNumber-1].GetComponent<Image>().sprite = imagenAudioOk;
                closeAudio = true;
            }
        }

    }

  

}
