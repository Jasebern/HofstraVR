using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class female_animate : MonoBehaviour
{
    /// <summary>
    /// Trigger this event so that game objects respond to the player waving.
    /// </summary>
    public event EventHandler HandWaved;
	public string sceneName;
	protected float timer = 0.0f;

	Animator anim;
    void Start()
    {
		anim= gameObject.GetComponent<Animator>();
    }
		
    void Update()
    {
		timer += Time.deltaTime;
		if(CrossPlatformInputManager.GetButtonDown("Fire1") || CrossPlatformInputManager.GetButtonDown("Fire3")){
			//lock player from waving in first few seconds of main rip scene (avoid carryover from practice scene)
			if (sceneName == "rip" && timer < 2.0f){
				Debug.Log("Pass");
			}
			//wave if not in start of rip scene
			if (sceneName != "rip"){
				anim.SetTrigger ("Wave");
				Debug.Log("Waved");
				if (HandWaved != null)
				{
					HandWaved.Invoke(this, new EventArgs());
				}
			}
			if (timer > 2.0f){
				anim.SetTrigger ("Wave");
				Debug.Log("Waved");
				if (HandWaved != null)
				{
					HandWaved.Invoke(this, new EventArgs());
				}
			}
		}

	}

}

