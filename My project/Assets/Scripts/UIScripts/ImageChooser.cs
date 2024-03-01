using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChooser : MonoBehaviour
{
    public Image NextAbility;
    public Image Shift;
    public Image Space;
    public Image Mouse2;
    public Image SpaceBorder;
    public Image ShiftBorder;
    public Image Mouse2Border;

    //public Image oldImage;
    //public Sprite icon;
    public Sprite outOfAbilties;
    
    private List<Image> imageList = new List<Image>();

    private Dictionary<string, Sprite> spriteDict = new();

    private void Awake()
    {
        ImageSetup();
        ImageListSetup();
        SpriteDictSetup();
    }

    private void Start()
    {
        //ImageSetup();
        //ImageListSetup();
        //SpriteDictSetup();
        //outOfAbilties = Resources.Load<Sprite>("Empty");
    }

    private void ImageListSetup()
    {
        imageList.Add(NextAbility);
        imageList.Add(Shift);
        imageList.Add(Space);
        imageList.Add(Mouse2);
    }

    private void ImageSetup()
    {
        Image[] images = GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            string imageName = image.gameObject.name;
            if (imageName == "NextImage")
            {
                NextAbility = image;
            }
            else if (imageName == "ShiftImage")
            {
                Shift = image;
            }
            else if (imageName == "SpaceImage")
            {
                Space = image;
            }
            else if (imageName == "Mouse2Image")
            {
                Mouse2 = image;
            }
            else if (imageName == "Border")
            {
                ShiftBorder = image; 
            }
            else if (imageName == "Border1")
            {
                SpaceBorder = image; 
            }
            else if(imageName == "Border2")
            {
                Mouse2Border = image; 
            }
        }
    }

    private void SpriteDictSetup()
    {
        spriteDict.Add("StickyBomb", Resources.Load<Sprite>("Sticky_bomb"));
    }

    //public void ImageChange(int abilityNum, Sprite icon)
    public void ImageChange(int buttonNum, string name)
    {
        if (spriteDict.ContainsKey(name))
        {
            imageList[buttonNum + 1].overrideSprite = spriteDict[name];
        }
    }

    public void ToggleImage(int abilityNum) 
    {
        //set toggled state here
    }

    public void SetOutOfAbilities(int buttonNum)
    {
        imageList[buttonNum + 1].overrideSprite = outOfAbilties;
    }

    public void ToggleBorder(string name)
    {
        switch (name)
        {
            case "Shift":
                ToggleBorderColor(ShiftBorder);
                break;
            case "Space":
                ToggleBorderColor(SpaceBorder);
                break;
            case "Mouse2":
                ToggleBorderColor(Mouse2Border);
                break;
        }
    }

    private void ToggleBorderColor(Image border)
    {
        if(border.color == Color.red)
        {
            border.color = Color.black;
        }
        else 
        {
            border.color = Color.red;
        }
    }
}
