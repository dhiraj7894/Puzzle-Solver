using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float transitionTime = 0f;
    // Start is called before the first frame update
    
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadScene(currentSceneIndex + 1));
    }

    private IEnumerator LoadScene(int sceneIdx)
    {
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneIdx);
      //  Debug.Log("Done");
    }

    public void LoadFirstScene()
    {
        StartCoroutine(LoadScene(0));
    }
}
