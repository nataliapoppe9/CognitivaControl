using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GalleryDisplay : MonoBehaviour
{
    public Gallery gallery;

    public Image[] imagenes;
    int hasta;

  

    public void MontarGalería()
    {
        Debug.Log(gallery.img.Length);

        for (int i = 0; i < gallery.img.Length; i++)
        {
            Texture2D texture = gallery.img[i].texture;
            imagenes[i].sprite = gallery.img[i];
            imagenes[i].gameObject.SetActive(true);
            hasta = i;
        }
        for (int i = hasta + 1; i < imagenes.Length; i++)
        {
            imagenes[i].gameObject.SetActive(false);
        }
    }
    public void ChangeCarpeta(Gallery galeria)
    {
        Debug.Log(galeria.ejercicio);
        gallery = galeria;
        for (int i = 0; i < gallery.img.Length; i++)
        {
            Texture2D texture = gallery.img[i].texture;
            imagenes[i].sprite = gallery.img[i];
            imagenes[i].gameObject.SetActive(true);
            hasta = i;
        }
        for (int i = hasta + 1; i < imagenes.Length; i++)
        {
            imagenes[i].gameObject.SetActive(false);
        }

    }

    void Start()
    {
        MontarGalería();
    }



   
}
