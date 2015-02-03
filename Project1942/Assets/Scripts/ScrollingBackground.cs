using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour 
{
   public GameObject backgroundObject;
   public GameObject foregroundObject;

   public GameObject greyPlanet;
   public GameObject redPlanet;


   RawImage background;
   RawImage foreground;

   float offset = 0;

   public float scrollRateBackground = 0.25f;
   public float scrollRateForeground = 0.25f;

   public float greyPlanetSpeed = .01f;
   public float redPlanetSpeed = .01f;

	// Use this for initialization
	void Start () 
    {
        background = backgroundObject.GetComponent<RawImage>();
        foreground = foregroundObject.GetComponent<RawImage>(); 

	}
	
	// Update is called once per frame
	void Update ()
    {

        Vector2 pos = background.uvRect.position;
        offset = pos.y + Time.deltaTime * scrollRateBackground;
        offset = Mathf.Repeat(offset, 1);
        background.uvRect = new Rect(pos.x, offset, 2, 2);


        pos = foreground.uvRect.position;
        offset = pos.y + Time.deltaTime * scrollRateForeground;
        offset = Mathf.Repeat(offset, 1);
        foreground.uvRect = new Rect(pos.x, offset, 2, 2);

        greyPlanet.transform.Translate(Vector3.down * Time.deltaTime * greyPlanetSpeed);
        redPlanet.transform.Translate(Vector3.down * Time.deltaTime * redPlanetSpeed , Space.World);


        
	}
}
