using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<Text> options;
    public Text question;
    public int correct;
    public LoadScenes loadScenes;
    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        for (; x < StoryManager.storyList.Stories.Count; x++)
        {
            if(StoryManager.storyList.Stories[x].beast_name == SummonManager.name)
            {
                break;
            }
        }
        Story s = StoryManager.storyList.Stories[x];
        Beast b = new Beast();
        for (x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
        {
            if(BeastManager.beastsList.Beasts[x].name == SummonManager.name)
            {
                b = BeastManager.beastsList.Beasts[x];
                break;
            }
        }
        int qTier = b.tier -1;
        print(s.questions[qTier]);
        print(qTier);
        question.text = s.questions[qTier].question;
        List<string> qOrder = new List<string>();
        for(int y = 0; y < 4; y++)
        {
            qOrder.Add(null);
        }
        int ran = -1;
        x = 3;
        List<int> intOrder = new List<int>();

        while (intOrder.Count < 4)
        {
            ran = Random.Range(0, 4);
            if (!intOrder.Contains(ran))
            {
                intOrder.Add(ran);
            }
        }

        for(int y = 0; y< options.Count; y++)
        {
            options[y].text = s.questions[qTier].options[intOrder[y]];
            if(intOrder[y] == 0)
            {
                ran = y;
            }
        }

        /*while (x>=0)
        {
            ran = Random.Range(0, 4);
            if(qOrder[ran] == "")
            {
                qOrder[ran] = s.questions[qTier].options[x];
                x--;
            }
        }*/
        correct = ran;
        print(correct);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickOption(int opt)
    {
        if(opt == correct)
        {
            Beast b = new Beast();
            for (int x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
            {
                if (BeastManager.beastsList.Beasts[x].name == SummonManager.name)
                {
                    b = BeastManager.beastsList.Beasts[x];
                    break;
                }
            }
            b.tier++;
            loadScenes.LoadSelect("BeastStats");
        }
        else
        {
            loadScenes.LoadSelect("Menu");
        }
    }
}
