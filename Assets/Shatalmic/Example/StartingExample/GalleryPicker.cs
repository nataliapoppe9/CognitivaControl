using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class GalleryPicker : MonoBehaviour
{
    public Sprite mySprite;
    //[SerializeField] GameObject imagen;
    public static List<GameObject> lista = new List<GameObject>();
    private bool ctrl = false;
    private bool continuar = false;
    [SerializeField] GameObject good;
    [SerializeField] Sprite close;
    void Start()
    {
        GameObject colorBut = this.gameObject;
        GameObject[] prueba = FindObjectsOfType<GameObject>();

        foreach (GameObject myGameObject in prueba)
        {
            if (!lista.Contains(myGameObject))
            {
                if (myGameObject.name == "One" || myGameObject.name == "Two" || myGameObject.name == "Three" || myGameObject.name == "Four" || myGameObject.name == "Five" || myGameObject.name == "Six")
                {

                    lista.Add(myGameObject);
                    Debug.Log(myGameObject.name);
                }
            }
        }
    }

    void Update()
    {
        GetInput();
    }

    public void PickImage(int maxSize, string imagen)
    {
        Debug.Log(this.name);
        if (NativeGallery.IsMediaPickerBusy())
            return;
        else
        {
            NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
            {
                Debug.Log("Image path: " + path);
                if (path != null)
                {
                    // Create Texture from selected image
                    Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                    if (texture == null)
                    {
                        Debug.Log("Couldn't load texture from " + path);
                        return;
                    }
                    mySprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

                    foreach (GameObject myGameObject in lista)
                    {
                        if (imagen == myGameObject.name)
                        {
                            myGameObject.GetComponent<Image>().sprite = mySprite;
                        }
                    }

                    
                    //	chosen = true;
                }
            });

            Debug.Log("Permission result: " + permission);
        }
    }


    /*public void GetInput()
	{
			if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Stationary))
			{
				RaycastHit2D hit = Physics2D.Raycast(Input.GetTouch(0).position, -Vector2.up);
				if (hit.collider != null && !ctrl)
				{
					Debug.Log("Something Hit");
					if (!chosen)
					{
						PickImage(512);
						hit.transform.GetComponent<Image>().sprite = mySprite;
					}
					if (chosen)
					{

						foreach (GameObject myGameObject in lista)
						{
							myGameObject.SetActive(false);											
						}

					good.GetComponent<Image>().sprite = hit.transform.GetComponent<Image>().sprite;
					good.GetComponent<Image>().enabled = true;
					ctrl = true;
					}
				}
				else { Continue(); }

			}
			else
			{
				if (ctrl)
					Debug.Log("ENDEdS");
				foreach (GameObject myGameObject in lista)
				{
					myGameObject.SetActive(true);
					good.GetComponent<Image>().enabled = false;
				}
				ctrl = false;
			}

		
      
	}*/

    public void GetInput()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Stationary))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.GetTouch(0).position, -Vector2.up);

            if (hit.collider != null && hit.collider.name == "continue")
            {
                continuar = true;
                Debug.Log("conitnua");
                hit.collider.gameObject.GetComponent<Image>().sprite = close;
            }
            else if (hit.collider != null && !continuar)
            {
  
                PickImage(512, hit.transform.name);
                Debug.Log(hit.transform.name);
            }
            else if (hit.collider != null && continuar && !ctrl)
            {

                foreach (GameObject myGameObject in lista)
                {
                    myGameObject.SetActive(false);
                }

                good.GetComponent<Image>().sprite = hit.transform.GetComponent<Image>().sprite;
                good.GetComponent<Image>().enabled = true;
                ctrl = true;
            }
           
        }
       else
        {
            if (ctrl)
                Debug.Log("ENDEdS");
            foreach (GameObject myGameObject in lista)
            {
                myGameObject.SetActive(true);
                good.GetComponent<Image>().enabled = false;
            }
            ctrl = false;
        }



    }

}
