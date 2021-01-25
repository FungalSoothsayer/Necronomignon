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

    Story s;
    List<string> questions = new List<string>();
    List<int> used = new List<int>();
    int questionNumber;
    int questionChosen = 0;

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
        s = StoryManager.storyList.Stories[x];

        //Making a copy of the beast that is chosen
        for (x = 0; x < BeastManager.beastsList.Beasts.Count; x++)
        {
            if(BeastManager.beastsList.Beasts[x].name == SummonManager.name)
            {
                b = BeastManager.beastsList.Beasts[x];
                break;
            }
        }

        //A list of the questions so they can be removed when answered already
        for (int i = 0; i < s.questions.Count; i++)
        {
            if (!s.questions[i].selected)
            {
                questions.Add(s.questions[i].question);
            }
        }

        //Randomize the question
        questionNumber = Random.Range(0, questions.Count);
        question.text = questions[questionNumber];

        while(questions[questionNumber] != s.questions[questionChosen].question)
        {
            questionChosen++;
        }
        s.questions[questionChosen].selected = true;

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
            options[y].text = s.questions[questionChosen].options[intOrder[y]];
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
            s.questions[questionNumber].selected = false;
            loadScenes.LoadSelect("Menu");
        }
    }
}
