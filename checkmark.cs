using UnityEngine;
using UnityEngine.SceneManagement;

public class checkmark : MonoBehaviour
{
    public bool sceneTransition; // true or false
    public string sceneName;
	public int transition_timer;

    void OnTriggerEnter(Collider other) 
    {
        if (sceneTransition && other.tag=="Player") 
        {
            Debug.Log("entered");
            Invoke("load_next_scene", transition_timer);
        }
    }

    private void Start()
    {
        printName();
    }

    public virtual void printName()
    {
        Debug.Log(sceneName);
    }

    private void load_next_scene()
	{
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}

}