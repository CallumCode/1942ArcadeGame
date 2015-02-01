using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour 
{
    public GameObject mainBackgroundObject;
   RawImage mainBackground;


   public float scaleRate = 0.25f;
	// Use this for initialization
	void Start () 
    {
        mainBackground = mainBackgroundObject.GetComponent<RawImage>(); 
	}
	
	// Update is called once per frame
	void Update ()
    {

        Vector2 pos = mainBackground.uvRect.position;
        float newy =  pos.y + Time.deltaTime * scaleRate ;
        newy = Mathf.Repeat(newy , 1);
        mainBackground.uvRect = new Rect(pos.x, newy, 1, 1);

	}
}
