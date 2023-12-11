using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManger : MonoBehaviour
{
  /*  [SerializeField] Animator anim;*/
    public void LoadScene(string sceneName)
    {

        SceneManager.LoadScene(sceneName);

    }


/*    public void PlayBTN(string sceneName)
    {
        anim.SetTrigger("Trigger");
        StartCoroutine(ChangeScene(sceneName));

    }


    IEnumerator ChangeScene(string sceneName)
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }
*/
}
