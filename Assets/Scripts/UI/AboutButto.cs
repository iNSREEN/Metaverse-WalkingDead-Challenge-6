using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutButto : MonoBehaviour
{
    [Header("Main Scene")]
    public GameObject GameTitle;
    public GameObject AboutBtn;
    public GameObject PlayBtn;



    [Header("About Scene")]
    public GameObject backPlan;
    public GameObject Title;
    public GameObject Text;
    public GameObject backButton;
/*    void Start()
    {
        backPlan.SetActive(false);
        Title.SetActive(false);
        Text.SetActive(false);
        backButton.SetActive(false);
    }

*/
    public void Aboutbtn()
    {

        GameTitle.SetActive(false);
        AboutBtn.SetActive(false);
        PlayBtn.SetActive(false);
   
        backPlan.SetActive(true);
        Title.SetActive(true);
        Text.SetActive(true);
        backButton.SetActive(true);
    }

    public void Backbtn()
    {

        GameTitle.SetActive(true);
        AboutBtn.SetActive(true);
        PlayBtn.SetActive(true);

        backPlan.SetActive(false);
        Title.SetActive(false);
        Text.SetActive(false);
        backButton.SetActive(false);
    }


}
