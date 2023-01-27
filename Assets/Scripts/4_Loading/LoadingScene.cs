using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public Slider loadingSlider; 
    AsyncOperation async;

    private void Start()
    {
        loadingSlider.value = 0;
        StartCoroutine(LoadSceneAsync());
    }



    IEnumerator LoadSceneAsync()
    {
        async = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("Scene")); 
        async.allowSceneActivation = false;

        yield return new WaitForSeconds(1);

        while (!async.isDone)
        {
            loadingSlider.value = async.progress; 
            if (async.progress >= 0.9f)
            {
                loadingSlider.value = 1f; 
                async.allowSceneActivation = true; 
            }
            yield return null;
        }
    }
}
