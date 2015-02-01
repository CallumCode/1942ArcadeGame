using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour 
{
    public GameObject mainBackgroundObject;
   RawImage mainBackground;

   float offset = 0;

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
        offset = pos.y + Time.deltaTime * scaleRate;
        offset = Mathf.Repeat(offset, 1);
        mainBackground.uvRect = new Rect(pos.x, offset, 2, 2);

	}
}
