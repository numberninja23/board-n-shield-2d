using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lance : MonoBehaviour {

    public Animator myAnim;

    public UIManager ui;

    public Player2Controller a;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        myAnim.SetBool("Jumping", false);
        if (other.collider.CompareTag("Player"))
        {
            a.Die();          
        }
    }
}
