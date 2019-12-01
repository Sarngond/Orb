using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public float waitTime = 3f;
    public int sceneIndex = 1;

    // Start is called before the first frame update
    void Start() {

    }

    private void Awake() {
        Invoke("LoadScene", waitTime);
    }

    // Update is called once per frame
    void Update() {

    }

    public void LoadScene() {
        Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(sceneIndex);
    }
}
