using UnityEngine;
using UnityEngine.SceneManagement;

public class rip_exit : MonoBehaviour
{
	public bool sceneTransition; // true or false
	public string sceneName;
	public int transition_timer;
	//play heavy breathing sound for fatigue
	public AudioClip heavy_breathe;
	public AudioSource audio;
	public bool isplaying = false;
	void Start()
	{

	}

	void Update()
	{
		//plays heavy breathing when stamina gets down to 20
		if (PlayerMotor2.stamina<20.0f && isplaying == false){
			audio = GetComponent<AudioSource>();
			audio.Play();
			isplaying = true;
			}
	}

	void PlayClip()
	{
		Debug.Log("works");
		audio = GetComponent<AudioSource>();
		audio.Play();
	}
		
		
	void OnTriggerExit(Collider other) 
	{
		PlayerMotor2 player = other.gameObject.GetComponent<PlayerMotor2>();
		if(player){
			player.leftRipCurrent = true;
			Invoke("load_next_scene", transition_timer);
			//
		}
		//Debug.Log("escaped rip with " + other.gameObject.name);

		//if (sceneTransition && other.tag=="Player") 
		{
			
			//SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
		}
	}
		
	private void load_next_scene()
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}
	//private void Start()
	//{
		//printName();
	//}

	//public virtual void printName()
	//{
		//Debug.Log(sceneName);
	//}

}