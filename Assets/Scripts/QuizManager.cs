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

    Beast b = new Beast();

    // Start is called before the first frame update
    void Start()
    {
        int x = 0;
        //Getting the stories that correspond to the chosen beast
        for (; x < StoryManager.storyList.Stories.Count; x++)
        {
            if(StoryManager.storyList.Stories[x].beast_name == SummonManager.name)
            {
                break;
            }
        }
        Story s = StoryManager.storyList.Stories[x];

        //Making a copy of the beast that is chosen
        for (x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
        {
            if(BeastManager.beastsList.Beasts[x].name == SummonManager.name)
            {
                b = BeastManager.beastsList.Beasts[x];
                break;
            }
        }

        int qTier = b.tier -1;

        //Randomize the question
        int questionNumber = Random.Range(0, s.questions.Count);
        question.text = s.questions[questionNumber].question;

        int ran = -1;
        List<int> intOrder = new List<int>();

        //Randomize the answer options
        while (intOrder.Count < 4)
        {
            ran = Random.Range(0, 4);
            if (!intOrder.Contains(ran))
            {
                intOrder.Add(ran);
            }
        }

        //Randomize the answer options
        for (int y = 0; y < options.Count; y++)
        {
            options[y].text = s.questions[questionNumber].options[intOrder[y]];
            if(intOrder[y] == 0)
            {
                ran = y;
            }
        }

        correct = ran;
        print(correct);
    }

    //Raises tier if the correct answer is chosen and returns to Menu if it isn't
    public void clickOption(int opt)
    {
        if(opt == correct)
        {
            b.tier++;
            loadScenes.LoadSelect("BeastStats");
        }
        else
        {
            loadScenes.LoadSelect("Menu");
        }
    }
}
