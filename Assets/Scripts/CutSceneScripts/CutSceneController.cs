using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CutSceneController : MonoBehaviour
{

   
    [SerializeField] private CanvasGroup image_1 = null;

    [SerializeField] private LevelLoader scenecontroller = null;


  
    
    float timeElapsed;
    float lerpDuration = 3f;




    private void Update()
    {
        StartCoroutine("Scenes");
    }



    private void FadeOut(CanvasGroup scene, float start, float end)
    {    
        if (timeElapsed < lerpDuration)
        {
            scene.alpha = Mathf.Lerp(start, end, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
        }
    }

    private IEnumerator Scenes()
    {
        yield return new WaitForSeconds(10f);
        FadeOut(image_1, 1f, 0f);
        yield return new WaitForSeconds(8f);
        scenecontroller.LoadNextScene();
    }

 
}
