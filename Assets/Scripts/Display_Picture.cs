using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Display_Picture : MonoBehaviour
{
    [SerializeField] GameObject container;

    public List<Sprite> listOfPictures;
    public static Sprite displayPicture;

    private void Start()
    {
        // Sets a default picture
        if (displayPicture == null)
        {
            displayPicture = listOfPictures[0]; // We can default to a random picture instead
        }

        GameObject.Find("PictureButton").GetComponent<Image>().sprite = displayPicture;
    }

    // Sets current display picture to the chosen one and then updates the list
    public void ChangeDisplayPicture(int pictureNumber)
    {
        displayPicture = listOfPictures[pictureNumber];
        GameObject.Find("PictureButton").GetComponent<Image>().sprite = listOfPictures[pictureNumber];
        UpdatePictureList();
    }

    // Opens the list of possible display pictures to pick
    public void OpenAvailablePictures()
    {
        container.SetActive(!container.activeSelf);
        if (container.activeInHierarchy)
        {
            UpdatePictureList();
        }
    }

    // Updates the available pictures in the list to not include the currently chosen one
    void UpdatePictureList()
    {
        for (int x = 0; x < listOfPictures.Count; x++)
        {
            GameObject.Find("Content").transform.GetChild(x).gameObject.SetActive(true);
        }

        for (int x = 0; x < listOfPictures.Count; x++)
        {
            if (displayPicture == listOfPictures[x])
            {
                GameObject.Find("Content").transform.GetChild(x).gameObject.SetActive(false);
            }
        }
    }
}
