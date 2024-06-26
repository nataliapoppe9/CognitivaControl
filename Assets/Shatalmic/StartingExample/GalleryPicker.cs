using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GalleryPicker : MonoBehaviour
{
    public Sprite mySprite;
    //[SerializeField] GameObject imagen;
    public static List<GameObject> lista = new List<GameObject>();
    public static List<GameObject> listaBotonesAudio = new List<GameObject>();
    private bool menu = false;
    public bool continuar = false;
    [SerializeField] GameObject panelTurnOff, panelTurnOn, panelBase, panelSelectImg, audioPanel, mec, rrPanel;
    [SerializeField] Sprite close, spriteAņadir, spriteAņadirAudio;
    [SerializeField] GameObject one, two, three, four, five, six, start;
    //    float endPressTime = 0;
    float pressTime = 0;
    string rrSelectedName;
    bool panelRRactivo = false;
   


    public bool audioPanelOpen = true;
    [SerializeField] Audio[] clipXcasillaG;
    AudioManager audioManager;
    //public bool closedAudio = true;

    string nameSelected;
    bool controlA = true;
    ImagesButons instance;
   
    void Start()
    {
        GameObject[] prueba = FindObjectsOfType<GameObject>();
        audioManager = FindAnyObjectByType<AudioManager>();
        instance = FindAnyObjectByType<ImagesButons>();
     
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

            if (!listaBotonesAudio.Contains(myGameObject))
            {
                if (myGameObject.name == "Audio1" || myGameObject.name == "Audio2" || myGameObject.name == "Audio3" || myGameObject.name == "Audio4" || myGameObject.name == "Audio5" || myGameObject.name == "Audio6")
                {
                    listaBotonesAudio.Add(myGameObject);
                    Debug.Log(myGameObject.name);
                }
            }
        }
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    pressTime = 0;
                    break;

                case TouchPhase.Stationary:
                    pressTime += Time.deltaTime;
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (pressTime < 0.1f)
                    {
                        Debug.Log("Tap. ");
                        if(!panelRRactivo) StartCoroutine(GetInput());
                    }
                    else if(pressTime > 0.4f)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(Input.GetTouch(0).position, -Vector2.up);
                        if (!menu && !audioPanelOpen && hit.collider.name!="RayCastHit") 
                        {
                            
                            Debug.Log(hit.collider.name);
                            rrSelectedName = hit.collider.name;

                            Debug.Log("Long Press Ended");
                            rrPanel.SetActive(true);
                            panelRRactivo = true;
                        }
                    }
                    pressTime = 0;
                    break;
            }

        }


       
        if(audioManager.closeAudio) { CerrarPanel(audioPanel); }
        
    }


    public void RemoveImg()
    {
        foreach (GameObject img in lista)
        {
            if (rrSelectedName == img.name)
            {

                img.GetComponent<Image>().sprite = spriteAņadir; ;
            }
        }
        
        CerrarPanel(rrPanel);
    }

    public void RotateImg()
    {
        foreach (GameObject img in lista)
        {
            if (rrSelectedName == img.name)
            {

                img.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);

            }
        }
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


    public void OpenAudioPanel()
    {
        if (!panelRRactivo)
        {
            foreach (GameObject but in lista)
            {
                but.GetComponent<Collider2D>().enabled = false;
            }
            audioPanelOpen = true;
            //for(int i =0,........ 
            //Se puede optimizar
            switch (EventSystem.current.currentSelectedGameObject.name)
            {
                case "Audio1":
                    audioManager.audioNumber = 1;
                    break;
                case "Audio2":
                    audioManager.audioNumber = 2;
                    break;
                case "Audio3":
                    audioManager.audioNumber = 3;
                    break;
                case "Audio4":
                    audioManager.audioNumber = 4;
                    break;
                case "Audio5":
                    audioManager.audioNumber = 5;
                    break;
                case "Audio6":
                    audioManager.audioNumber = 6;
                    break;
            }

            audioPanel.SetActive(true);
        }
    }

    public void CerrarPanel(GameObject panel)
    {
        foreach (GameObject but in lista)
        {
            but.GetComponent<Collider2D>().enabled = true;
        }
        if (panelRRactivo) { rrPanel.SetActive(false); panelRRactivo = false; }
        if(menu) menu = false;
        panel.SetActive(false);
        if (audioPanelOpen) { audioPanelOpen = false; }       
    }

    public void StartAgain()
    {
        if (!instance.centerImg.activeInHierarchy)
        {
            continuar = false;
            foreach (GameObject obj in lista)
            {
                obj.GetComponent<Image>().sprite = spriteAņadir;
                obj.transform.rotation = Quaternion.identity;
                obj.SetActive(true);
            }
            foreach (GameObject obj in listaBotonesAudio)
            {
                obj.GetComponent<Image>().sprite = spriteAņadirAudio;
                obj.SetActive(true);
            }
            foreach (Audio audio in clipXcasillaG)
            {
                audio.clip = null;
            }
            mec.SetActive(true);
            start.SetActive(true);
        }
        Debug.Log(!instance.centerImg.activeInHierarchy);
    }


 
    //IEnumerator
    public IEnumerator GetInput()
    {
        
        
            RaycastHit2D hit = Physics2D.Raycast(Input.GetTouch(0).position, -Vector2.up);



            if (menu)
            {
                if (hit.collider != null && hit.collider.name.Contains("Image")) {
                    Debug.Log(hit.collider.name);
                    foreach (GameObject but in lista)
                    {
                        if (nameSelected == but.name)
                        {
                            // Debug.Log(but.name + " " + nameSelected);
                            but.GetComponent<Image>().sprite = hit.collider.GetComponent<Image>().sprite;
                            but.transform.rotation = hit.collider.transform.rotation;
                        }
                    }
                    yield return new WaitForSeconds(0.09f);
                    CerrarPanel(panelSelectImg);


                }
                else if (hit.collider != null && hit.collider.name.Contains("Abrir")&& controlA)
                {
                    controlA = false;
                    PickImage(512, nameSelected);
                    Debug.Log(hit.transform.name);
                    CerrarPanel(panelSelectImg);
                   
                    yield return new WaitForSeconds(0.09f);
                }
                else if(hit.collider !=null && hit.collider.name.Contains("Close")&& controlA)
                {
                    CerrarPanel(panelSelectImg);
                }
            }
            else if(!menu)
            {
                if (hit.collider != null && hit.collider.name == "continue")
                {
                    continuar = true;
                    foreach (GameObject obj in lista) { obj.SetActive(false); }
                    foreach (GameObject obj in listaBotonesAudio) { obj.SetActive(false); }
                    mec.SetActive(false);
                    start = hit.collider.gameObject;
                    hit.collider.gameObject.SetActive(false);
                    Debug.Log("conitnua"); 
                    yield return new WaitForSeconds(0.07f);

                }
                else if(hit.collider != null && !continuar && !audioPanelOpen && hit.collider.name == "NoRayCast") { Debug.Log("button"); }
                else if (hit.collider != null && !continuar && !audioPanelOpen )
                {
                    Debug.Log("vez");
                    foreach (GameObject but in lista)
                    {
                        but.GetComponent<Collider2D>().enabled = false;
                    }
                    yield return new WaitForSeconds(0.07f);
                                      
                    nameSelected = hit.collider.name;
                    menu = true;
                    controlA = true;
                    panelSelectImg.SetActive(true);

                }
            }
        

       
     
    }
}
