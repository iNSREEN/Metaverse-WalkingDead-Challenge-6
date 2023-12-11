using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneTrigger : MonoBehaviour
{
    public int SceneIndex;
    [SerializeField] Animator anim;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Trigger");
            StartCoroutine(ChangeScene());

        }
    }


   
    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1f);
       
        SceneManager.LoadScene(SceneIndex);
    }
}
