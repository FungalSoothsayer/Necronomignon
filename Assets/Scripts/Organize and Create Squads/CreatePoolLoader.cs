using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Handles the beasts in the pool that can be added to a squad
 */
public class CreatePoolLoader : MonoBehaviour
{
    public BeastManager beastManager;
    public CreateManager createManager;

    public List<Image> slots; // Slots in the pool
    public List<string> summonedImages = new List<string>(); // Static image of each beast in the pool
    public List<Beast> summoned = new List<Beast>(); // Beasts that are summoned (not tier 0)
    public List<string> summonedNames = new List<string>(); // Names of beasts in pool

    public GameObject back;
    public GameObject forward;
    public Dropdown dropdown;

    public int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!beastManager.isLoaded())
        {
            beastManager.Awake();
        }

        BeastList bl = BeastManager.beastsList;

        for (int x = 0; x < bl.Beasts.Count; x++)
        {
            bl.Beasts[x].setAttacks();
            if(bl.Beasts[x].tier > 0)
            {
                summonedImages.Add(bl.Beasts[x].static_img);
                summoned.Add(bl.Beasts[x]);
                // Set up List of Beasts Names for Animations
                summonedNames.Add(bl.Beasts[x].name);
            }
        }

        SetImages();
    }

    // Fill up the image slots with your summoned beasts
    void SetImages()
    {
        for(int x = 0+ (counter * 9); x < slots.Count + (counter * 9); x++)
        {
            if (summoned.Count >= x + 1 && NotSummoned(x))
            {
                slots[x % 9].gameObject.SetActive(true);
                slots[x % 9].sprite = Resources.Load<Sprite>("Static_Images/"+summonedImages[x]);
            }
            else
            {
                print(x % 9);
                slots[x % 9].sprite = Resources.Load<Sprite>("Static_Images/EmptyRectangle");
            }
        }

        // Shows or hides the front and back page buttons depending on if there are beasts on previous pages
        if (counter == 0)
        {
            back.SetActive(false);
        }
        else if (counter > 0)
        {
            back.SetActive(true);
        }
        if ((counter * 9) + 9 >= summoned.Count)
        {
            forward.SetActive(false);
        }
        else if ((counter * 9) + 9 < summoned.Count)
        {
            forward.SetActive(true);
        }
    }

    // Sets the page forward by 1 when the front button is pressed
    public void Forward()
    {
        changeImages("Forward");
    }
    
    // Sets the page back by 1 when the back button is pressed
    public void Back()
    {
        changeImages("Back");
    }

    // Returns true if the beast exists but isn't summoned yet
    bool NotSummoned(int y)
    {
        if (y >= summoned.Count)
        {
            return true;
        }

        Beast beast = summoned[y];

        if (beast == null)
        {
            return false;
        }

        for (int x = 0; x < createManager.slots.Count; x++)
        {
            if (beast.Equals(createManager.slots[x]))
            {
                return false;
            }
        }

        return true;
    }

    // Changes the counter to show which page we are viewing beasts on
    public void changeImages(string str)
    {
        if(str == "Forward")
        {
            counter++;
        }
        else if(str == "Back")
        {
            counter--;
        }

        SetImages();
    }

    // When a beast is removed from the grid, place the image back into the pool
    public void PutImageBack()
    {
        int index = createManager.selectedIndex;

        if (NotSummoned(index))
        {
            slots[index].gameObject.SetActive(true);
            slots[index].sprite = Resources.Load<Sprite>("Static_Images/"+summonedImages[index+(counter*9)]);
        }
    }

    public void SortImagesDropdown(int value)
    {
        if (value == 0)
        {
            SortByTier();
        }
        else if (value == 1)
        {
            SortByType();
        }
        else if (value == 2)
        {
            SortByName();
        }
    }

    //Sorts the beasts by tier
    void SortByTier()
    {
        sorted.Clear();
        summonedImages.Clear();
        summonedNames.Clear();

        for (int x = 5; x >= 1; x--)
        {
            for (int y = 0; y < summoned.Count; y++)
            {
                if (summoned[y].tier == x)
                {
                    sorted.Add(summoned[y]);
                    summonedImages.Add(summoned[y].static_img);
                    summonedNames.Add(summoned[y].name);
                }
            }
        }

        SetImages();
    }

    //Sorts the beasts based on their type
    void SortByType()
    {
        sorted.Clear();
        summonedImages.Clear();
        summonedNames.Clear();

        for (int x = 0; x <= Enum.GetNames(typeof(Beast.types)).Length; x++)
        {
            for (int y = 0; y < summoned.Count; y++)
            {
                if ((int)summoned[y].type == x)
                {
                    sorted.Add(summoned[y]);
                    summonedImages.Add(summoned[y].static_img);
                    summonedNames.Add(summoned[y].name);
                }
            }
        }

        SetImages();
    }

    //Sorts the beasts in alphabetical order
    void SortByName()
    {
        sorted.Clear();

        List<string> names = new List<string>();

        foreach (Beast b in summoned)
        {
            names.Add(b.name);
        }
    }
}
