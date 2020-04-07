using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerMotor2 : PlayerMotor
{
	public bool leftRipCurrent = false;
	protected bool isRecorded = false;
	public static float stamina = 60.0f;
	//float variable to keep time, default is 60 seconds
	protected float timer = 0.0f;
	//creating timer variable
	protected string rNumber = "record number";
    protected string rDate   = "date number";
	//create audio variables
	public AudioClip Splash_sound;
	public AudioSource audio;
	//identify proper scene for rip drift
	public string sceneName;
	//set up for scene transition to main menu if drown
	public bool sceneTransition; // true or false
	public string fail_scenename;
	public int transition_timer;

	//If in rip scene, have rip current constantly move player from shore
	void Update()
	{
		if (sceneName == "rip"){
			rb.velocity = Vector3.forward * -1.25f;
		}
	}

    protected override void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            // Move rigidbody to the player position + the player's velocity
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
			// Play splashing audio sound upon movement
			audio = GetComponent<AudioSource>();
			audio.Play();

        }
		if(leftRipCurrent == false){
        // call stamina function
        	UpdateStamina();
		}
		else{
			FinalStamina();
			stamina=60.0f;
		}
		//if wave also call stamina function
		if(Buoyancy2.wasCalled == true){
			FinalStamina();
			stamina=60.0f;
		}
    }

    // change stamina function, but only if in main rip scene
    protected void UpdateStamina()
    {
		if (sceneName == "rip"){
			timer += Time.deltaTime;
			stamina -= Time.deltaTime;
        	//drops stamina down 1x per second, if player is not moving
        	if (velocity != Vector3.zero)
        	{
            	stamina -= 2 * Time.deltaTime;
            	//drops stamina down 2x if player is moving in rip current
        	}

			if (stamina <= 0.0f)
			{
				Debug.Log("Stamina out!");
				stamina=0;
				FinalStamina();
				stamina=60.0f;
				// scene transition to main menu;
				Invoke("load_next_scene", transition_timer);
			}
		}
			

    }

	//records final stamina if player escapes rip
	protected void FinalStamina()
	{
		if(isRecorded==false && sceneName == "rip")
        {
			isRecorded=true; //says that stamina has already been recorded, prevents from being recorded infinitely
			Debug.Log(stamina);
            int rNum = GetRNumber();
			string date = System.DateTime.Today.ToString("MM_dd_yy");
			string data = date + "\t" + rNum.ToString() + "\t\t" + stamina.ToString() + "\t" + timer.ToString();
			SaveData(date, data);
		}
	}

    protected int GetRNumber()
    {
        int rNum;
        string currentDate = System.DateTime.Today.ToString("MM_dd_yy");
        if (currentDate == PlayerPrefs.GetString(rDate,"none")) // if the current date is the same as the date saved
        {
            rNum = PlayerPrefs.GetInt(rNumber, 0); // increment rNum by 1
            if (rNum == 0) 
            { 
                Debug.LogError("Error Exception: rNum returned 0!"); 
            }
            rNum = rNum + 1;
            PlayerPrefs.SetInt(rNumber, rNum); // Update record with new rNum
            return rNum; // return rNum
        }
        else
        {
            rNum = 1; // default value is set to 1
            PlayerPrefs.SetString(rDate, currentDate); // Set this as the current date
            PlayerPrefs.SetInt(rNumber, rNum); // Set 1 as the current record number
            return rNum; // return default
        }

    }

	protected void SaveData(string filename, string data)
    {
		string filepath = Application.persistentDataPath + "/Data/"; // declares a filepath

		string header = "Date Stamp\tRecord\t\tStamina\t\tTime\tCondition";

		if(!Directory.Exists(filepath)) // if the folder Data does NOT exist
        {
			Directory.CreateDirectory(filepath); // make a new folder called Data
			Debug.Log("Directory Created: " + filepath);
		}
		filepath += filename + ".txt"; // C:/.../Data/jase.txt
		Debug.Log(filepath);
		if(!File.Exists(filepath))
        {
			File.WriteAllText(filepath, header); // if file does not exist, this creates a file in specified filepath and inserts header at start of file
		}


		data = "\r\n" + data; //added line break to data
		Debug.Log(data);
		File.AppendAllText(filepath, data); //actually appends data to file
		//add notes on condition
		if(Buoyancy2.wasCalled == true){
			File.AppendAllText(filepath, "\twaved");
		}
		if(stamina <= 0.0f){
			File.AppendAllText(filepath, "\tdrowned");
		}
		if((stamina > 0.0f) && (Buoyancy2.wasCalled == false)){
			File.AppendAllText(filepath, "\tescaped");
		}

	}

	//function to reset variables, may or may not be needed
	public void Reset()
	{
		leftRipCurrent = false;
		isRecorded=false;
		stamina=60.0f;
	}

	private void load_next_scene()
	{
		SceneManager.LoadScene(fail_scenename, LoadSceneMode.Single);
	}
}
