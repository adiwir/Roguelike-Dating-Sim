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
    public Image Discard;
    public Image SpaceBorder;
    public Image ShiftBorder;
    public Image Mouse2Border;

    //public Image oldImage;
    //public Sprite icon;
    public Sprite outOfAbilties;
    
    private List<Image> imageList = new List<Image>();
    private List<Image> borderImages = new List<Image>();

    private Dictionary<string, Sprite> spriteDict = new();

    private void Awake()
    {
        outOfAbilties = Resources.Load<Sprite>("Transparent"); 
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
        imageList.Add(Discard);
        borderImages.Add(SpaceBorder);
        borderImages.Add(ShiftBorder);
        borderImages.Add(Mouse2Border);
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
            else if (imageName == "DiscardImage")
            {
                Discard = image;
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
        spriteDict.Add("Shield", Resources.Load<Sprite>("Shield"));
        spriteDict.Add("Sniper", Resources.Load<Sprite>("Sniper"));
        spriteDict.Add("Transparent", Resources.Load<Sprite>("Transparent"));
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

    public void ToggleBorder(int spot)
    {
        switch (spot)
        {
            case 0:
                ToggleBorderColor(ShiftBorder);
                break;
            case 1:
                ToggleBorderColor(SpaceBorder);
                break;
            case 2:
                ToggleBorderColor(Mouse2Border);
                break;
        }
    }

    public void UntoggleAllBorders()
    {
        foreach (Image borderImage in borderImages)
        {
            borderImage.color = Color.black;
        }
    }

    public void AddLastAbilityIconToDiscard(string name)
    {
        if(spriteDict.ContainsKey(name))
        {
            imageList[4].overrideSprite = spriteDict[name];
        }
    }

    private void ToggleBorderColor(Image border)
    {
        foreach(Image borderImage in borderImages)
        {
            if (border == borderImage) continue;
            borderImage.color = Color.black;
        }

        if(border.color == Color.red)
        {
            border.color = Color.black;
        }
        else if (border.color == Color.black)
        {
            border.color = Color.red;
        }
    }
}
