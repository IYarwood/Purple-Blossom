using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelSelectUI : MonoBehaviour
{
    private int currentScene = 0;
    private GameObject levelViewCamera;

    //Something we want to run in the background without pausing the game
    private AsyncOperation currentLoadOperation;



    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLoadOperation != null && currentLoadOperation.isDone)
        {
            currentLoadOperation = null;

            //Level View Camera has to be the cameras name
            levelViewCamera = GameObject.Find("Level View Camera");
            if (levelViewCamera == null ) 
            {
                Debug.LogError("No level view camera was found in the scene!");
            }
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("OBSTACLE COURSE");

        if (currentScene != 0)
        {
            GUILayout.Label("Currently viewing Level" + currentScene);

            //Creates button and if we click on the button inside the baracks executes
            if (GUILayout.Button("PLAY"))
            {
                PlayCurrentLevel();
            }
        }
        else
        {
            GUILayout.Label("Select a level to preview it");

            for (int i =1; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (GUILayout.Button("Level: " + i))
                {
                    //AKA havent loaded the level
                    if (currentLoadOperation == null)
                    {
                        currentLoadOperation = SceneManager.LoadSceneAsync(i);

                        currentScene = i;
                    }
                }
            }
        }
    }
    private void PlayCurrentLevel()
    {
        levelViewCamera.SetActive(false);

        var playerGobj = GameObject.Find("Player");

        if (playerGobj == null)
        {
            Debug.LogError("Couldn't find player in the level!");
        }
        else
        {
            var playerScript = playerGobj.GetComponent<Player>();

            playerScript.enabled = true;

            playerScript.cam.SetActive(true);

            Destroy(this.gameObject);

        }

    }


}
