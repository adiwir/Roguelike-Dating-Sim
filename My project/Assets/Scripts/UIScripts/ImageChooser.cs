using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChooser : MonoBehaviour
{
    public Image oldImage;
    public Sprite icon;
    public Sprite outOfAbilties;

    public void ImageChange(int abilityNum)
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
