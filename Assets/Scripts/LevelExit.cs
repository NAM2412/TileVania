using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour
{
    [SerializeField] float OnNextLevelTime = 4f;
    [SerializeField] ParticleSystem finishEffect;
    [SerializeField] AudioClip finishLevelSound;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        finishEffect.Play();
        GetComponent<AudioSource>().PlayOneShot(finishLevelSound);
        StartCoroutine(LoadNextLevel());
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(OnNextLevelTime);
        int currenSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneINdex =  currenSceneIndex+1;
        if (nextSceneINdex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneINdex = 0;
        }
        SceneManager.LoadScene(nextSceneINdex);
    }
}
