using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChooser : MonoBehaviour
{
    public Image AbilityImage0;
    public Image AbilityImage1;
    public Image AbilityImage2;

    public Image oldImage;
    //public Sprite icon;
    public Sprite outOfAbilties;

    private void Awake()
    {
        //AbilityImage0 = GetComponent<Image>();
        //AbilityImage0 = GetComponent<Image>();
        //AbilityImage0 = GetComponent<Image>();
    }

    public void ImageChange(int abilityNum, Sprite icon)
    {
        oldImage.sprite = icon;
    }

    public void ToggleImage(int abilityNum) 
    {
        //set toggled state here
    }

    public void SetOutOfAbilities(int abilityNum)
    {
        oldImage.sprite = outOfAbilties;
    }
}
