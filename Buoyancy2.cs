using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Buoyancy2 : Buoyancy
{
    [SerializeField] female_animate f_animate;
    Animator anim;
    public static bool wasCalled;

    // Start is called before the first frame update
    void Start()
    {
		base.Start(); // Calls Parent class' Start function
        wasCalled = false;

        // Finding the female_animate class
        GameObject temp;
        temp = GameObject.FindGameObjectWithTag("Left"); // Find GameObject with this tag
        f_animate = temp.GetComponent<female_animate>();

        // Find Animator
        anim = GetComponent<Animator>();

        if (f_animate != null)
        {
            f_animate.HandWaved += OnHandWaved; // Register to its event: HandWaved and give it the name of the function you want it to call
        }
        else
        {
            Debug.LogError("Unable to find female_animate in Scene! Are you missing the Tag 'Left' ?");
        }

    }

	void FixedUpdate()
	{
		base.FixedUpdate(); // calls parent's FixedUpdate function
	}

    // Any function that is subscribed to the event MUST have these parameters below
    private void OnHandWaved(object sender, EventArgs e)
    {
        if (wasCalled==true)
        {
            return;
        }
        Debug.Log("Boat has seen your handwaving!");

      	anim.SetTrigger("Wave"); 
        wasCalled = true;
        OnCalled(); // Hand waved, so we call the function 'OnCalled' for calling the boat over to the player
    }

 
    /// The boat's behavior now changes to move towards the player.


	public string sceneName;
	public int transition_timer;

    public void OnCalled()
    {
        Debug.Log("Boat called!");
		Invoke("load_next_scene", transition_timer);
    }

	private void load_next_scene()
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

}
