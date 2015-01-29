using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour 
{
    public GameObject mainBackgroundObject;
   RawImage mainBackground;


   float scaleRate = 0.25f;
	// Use this for initialization
	void Start () 
    {
        mainBackground = mainBackgroundObject.GetComponent<RawImage>(); 
	}
	
	// Update is called once per frame
	void Update ()
    {

        Vector2 pos = mainBackground.uvRect.position;
        mainBackground.uvRect = new Rect(0, pos.y + Time.deltaTime * scaleRate ,1,1);

	}
}
