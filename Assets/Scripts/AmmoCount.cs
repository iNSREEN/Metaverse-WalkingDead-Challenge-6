using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AmmoCount : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
  
    public static AmmoCount occurance;

    private void Awake()
    {
        occurance = this;
    }

    public void UpdateAmmoText(int presentAmmo)
    {
        ammoText.text = "" + presentAmmo;
    }

}
