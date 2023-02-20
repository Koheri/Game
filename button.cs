using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    // Start is called before the first frame update
    public Button OldImage;
    public Image newImg;
    public Sprite nespr;

    public void changebutton()
    {
        OldImage.image.sprite = newImg.sprite;
    }
}
