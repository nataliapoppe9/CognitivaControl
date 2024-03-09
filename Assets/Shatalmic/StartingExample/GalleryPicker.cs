using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GalleryPicker : MonoBehaviour
{
    public Sprite mySprite;
    //[SerializeField] GameObject imagen;
    public static List<GameObject> lista = new List<GameObject>();
    private bool ctrl = false;
    private bool menu = false;
    private bool continuar = false;
    [SerializeField] GameObject good,panelTurnOff, panelTurnOn, panelBase, panelSelectImg;
    [SerializeField] Sprite close;
    [SerializeField] GameObject one, two, three, four, five, six;

    string nameSelected;


    ImagesButons instance;
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
       StartCoroutine(GetInput());
    }

    public void PickImage(int maxSize, string imagenNomb)
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

                    foreach (GameObject but in lista)
                    {
                        if (imagenNomb == but.name)
                        {
                            but.GetComponent<Image>().sprite = mySprite;
                            
                            switch (imagenNomb)
                            {
                                case "One":
                                    one.GetComponent<Image>().sprite = mySprite;
                                    break;
                                case "Two":
                                    two.GetComponent<Image>().sprite = mySprite;
                                    break;
                                case "Three":
                                    three.GetComponent<Image>().sprite = mySprite;
                                    break;
                                case "Four":
                                    four.GetComponent<Image>().sprite = mySprite;
                                    break;
                                case "Five":
                                    five.GetComponent<Image>().sprite = mySprite;
                                    break;
                                case "Six":
                                    six.GetComponent<Image>().sprite = mySprite;
                                    break;
                            }
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
    public void CerrarPanel(GameObject panel)
    {
        panel.SetActive(false);
        foreach(GameObject but in lista)
        {
            but.GetComponent<Collider2D>().enabled = true;
        }
        menu = false;
    }

 //IEnumerator
    public IEnumerator GetInput()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Stationary))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.GetTouch(0).position, -Vector2.up);



            if (menu)
            {
                if (hit.collider != null && hit.collider.name.Contains("Image")){

                    foreach(GameObject but in lista)
                    { 
                        if(nameSelected ==  but.name)
                        {
                            Debug.Log(but.name + " " + nameSelected);
                            but.GetComponent<Image>().sprite = hit.collider.GetComponent<Image>().sprite;
                        }
                    }
                    yield return new WaitForSeconds(0.2f);
                    CerrarPanel(panelSelectImg);
                    

                }
                else if(hit.collider != null && hit.collider.name== "Abrir")
                {
                    PickImage(512, hit.transform.name);
                    Debug.Log(hit.transform.name);
                    CerrarPanel(panelSelectImg);
                    menu = false;
                    yield return new WaitForSeconds(0.07f);
                }
            }
            else if(!menu)
            {
                if (hit.collider != null && hit.collider.name == "continue")
                {
                    continuar = true;
                    Debug.Log("conitnua"); 
                    yield return new WaitForSeconds(0.07f);

                }
                else if (hit.collider != null && !continuar)
                {
                    Debug.Log("vez");
                    foreach (GameObject but in lista)
                    {
                        but.GetComponent<Collider2D>().enabled = false;
                    }
                    yield return new WaitForSeconds(0.2f);
                                      
                    nameSelected = hit.collider.name;
                    menu = true;
                    panelSelectImg.SetActive(true);

                }

            }

            
            /*
            if (hit.collider != null && hit.collider.name == "continue")
            {
                continuar = true;
                Debug.Log("conitnua");

                //hit.collider.gameObject.GetComponent<Image>().sprite = close;
                panelTurnOn.SetActive(true);

                panelTurnOff.SetActive(false);
            }
            else if (hit.collider != null && !continuar)
            {
                panelTurnOn.SetActive(true);

                panelTurnOff.SetActive(false);
                menu = true;
                // PickImage(512, hit.transform.name);
                Debug.Log(hit.transform.name);

            }
            else if (hit.collider != null && continuar && !ctrl)
            {
                panelTurnOn.SetActive(true);
                // instance.startGame=true;
                panelTurnOff.SetActive(false);

                /*foreach (GameObject myGameObject in lista)
                {
                    myGameObject.SetActive(false);
                }

                good.GetComponent<Image>().sprite = hit.transform.GetComponent<Image>().sprite;
                good.GetComponent<Image>().enabled = true;
                //ctrl = true;
            //
            }
            */
           
        }
       /*else
        {
            if (ctrl)
                Debug.Log("ENDEdS");
            foreach (GameObject myGameObject in lista)
            {
                myGameObject.SetActive(true);
                good.GetComponent<Image>().enabled = false;
            }
            ctrl = false;
        }*/



    }

}
