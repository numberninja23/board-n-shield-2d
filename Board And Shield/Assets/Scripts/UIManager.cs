using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image speedRender;
    public Sprite[] speedSprites = new Sprite[39];

    public Rigidbody2D playerRB;

    private Vector2 noSpeed = new Vector2(Mathf.Abs(.1f), Mathf.Abs(.1f));

    public float maxSpeed = 15f;

    public int spritePick;

    // Use this for initialization
    void Start () {
        speedRender = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if(playerRB.velocity.Equals(noSpeed))
        {
            speedRender.overrideSprite = speedSprites[0];
        }
        else
        {
            spritePick = (int) Mathf.Round((playerRB.velocity.magnitude/maxSpeed)*38f);
            if(spritePick >= speedSprites.Length-1)
            {
                speedRender.overrideSprite = speedSprites[speedSprites.Length - 1];
            }
            else
            {
                speedRender.overrideSprite = speedSprites[spritePick];
            }
            
        }
    }

    public int GetSprite()
    {
        return spritePick;
    }
}
