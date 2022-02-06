using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordleApp
{
    public partial class WordleSolver : Form
    {
        string[] words = System.IO.File.ReadAllLines(@"C:\Users\snook\Desktop\Wordle\WordleApp\words.txt");
        List<string> incorrectLetters = new List<string>();
        Dictionary<int, string> correctLetters = new Dictionary<int, string>();
        List<List<string>> incorrectSpot = new List<List<string>>();

        public WordleSolver()
        {
            InitializeComponent();
            InitialWord();
        }

        public void FillWordle(String word)
        {

            char[] wordArray = word.ToCharArray();

            LetterOne.Text = wordArray[0].ToString();
            LetterTwo.Text = wordArray[1].ToString();
            LetterThree.Text = wordArray[2].ToString();
            LetterFour.Text = wordArray[3].ToString();
            LetterFive.Text = wordArray[4].ToString();

            ChangeLabelColor(LetterOne, 0);
            ChangeLabelColor(LetterTwo, 1);
            ChangeLabelColor(LetterThree, 2);
            ChangeLabelColor(LetterFour, 3);
            ChangeLabelColor(LetterFive, 4);

        }

        public void ChangeLabelColor(Label l, int pos)
        {
            string letter = l.Text;

            foreach (KeyValuePair<int, string> correctLetter in correctLetters)
            {
                if (correctLetter.Key == pos && correctLetter.Value == letter)
                {
                    l.ForeColor = Color.Green;
                }
            }
        }

        public void AddIncorrectSpot(int pos, string letter)
        {
            incorrectLetters.Remove(letter);

            for (int i = 0; i < incorrectSpot.Count; i++)
            {
                if (!incorrectSpot[i].Contains(letter))
                {
                    List<string> listToAdd = new List<string>();
                    listToAdd.Add(pos.ToString());
                    listToAdd.Add(letter);
                    incorrectSpot.Add(listToAdd);
                }
            }

            if (incorrectSpot.Count == 0)
            {
                List<string> listToAdd = new List<string>();
                listToAdd.Add(pos.ToString());
                listToAdd.Add(letter);
                incorrectSpot.Add(listToAdd);
            }
        }

        public void AddCorrectLetter(int pos, string letter)
        {
            for (int i = 0; i < incorrectSpot.Count; i++)
            {
                if (int.Parse(incorrectSpot[i][0]) == pos && incorrectSpot[i][1] == letter)
                {
                    incorrectSpot.RemoveAt(i);
                }
            }

            if (correctLetters.ContainsValue(letter) == false)
            {
                correctLetters.Add(pos, letter);
            }
        }

        public void InitialWord()
        {
            FillWordle("ideal");
        }

        private void NextWord_Click(object sender, EventArgs e)
        {

            if (correctLetters.Count != 0)
            {
                bool correctWord = true;

                List<string> tmpList = new List<string>();

                foreach (string word in words)
                {
                    foreach (KeyValuePair<int, string> letter in correctLetters)
                    {

                        if (word[letter.Key] == char.Parse(letter.Value))
                        {
                            correctWord = true;
                        } else
                        {
                            correctWord = false;
                            break;
                        }
                    }

                    if (correctWord)
                    {
                        tmpList.Add(word);
                    }
                }

                words = tmpList.ToArray();
            }

            if (incorrectSpot.Count != 0)
            {
                bool correctWord = true;

                List<string> tmpList = new List<string>();

                foreach (string word in words)
                {
                    for (int i = 0; i < incorrectSpot.Count; i++)
                    {

                        if (word[int.Parse(incorrectSpot[i][0])] != char.Parse(incorrectSpot[i][1]) &&
                            word.Contains(incorrectSpot[i][1]))
                        {
                            correctWord = true;
                        }
                        else
                        {
                            correctWord = false;
                            break;
                        }
                    }

                    if (correctWord)
                    {
                        tmpList.Add(word);
                    }
                }

                words = tmpList.ToArray();
            }

            // Remove words that contain incorrect letters
            foreach (string letter in incorrectLetters)
            {
                string[] tmp = Array.FindAll(words, element => !element.ToLower().Contains(letter));
                words = tmp;
            }

            Random random = new Random();
            FillWordle(words[random.Next(0, words.Length)]);
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            words = System.IO.File.ReadAllLines(@"C:\Users\snook\Desktop\Wordle\WordleApp\words.txt");
            incorrectLetters.Clear();
            incorrectSpot.Clear();
            correctLetters.Clear();

            LetterOne.ForeColor = Color.White;
            LetterTwo.ForeColor = Color.White;
            LetterThree.ForeColor = Color.White;
            LetterFour.ForeColor = Color.White;
            LetterFive.ForeColor = Color.White;

            FillWordle("ideal");
        }

        // <--- Incorrect Letter Functions --->

        private void LetterOneWrong_Click(object sender, EventArgs e)
        {
            //incorrectSpot.Remove(LetterOne.Text);

            // Ensure that a letter is not correct and incorrect
            if (!correctLetters.ContainsValue(LetterOne.Text))
            {
                if (!incorrectLetters.Contains(LetterOne.Text))
                {
                    incorrectLetters.Add(LetterOne.Text);
                }
            }
        }

        private void LetterTwoWrong_Click(object sender, EventArgs e)
        {
            //incorrectSpot.Remove(LetterTwo.Text);

            if (!correctLetters.ContainsValue(LetterTwo.Text))
            {
                if (!incorrectLetters.Contains(LetterTwo.Text))
                {
                    incorrectLetters.Add(LetterTwo.Text);
                }
            }    
        }

        private void LetterThreeWrong_Click(object sender, EventArgs e)
        {
            //incorrectSpot.Remove(LetterThree.Text);

            if (!correctLetters.ContainsValue(LetterThree.Text))
            {
                if (!incorrectLetters.Contains(LetterThree.Text))
                {
                    incorrectLetters.Add(LetterThree.Text);
                }
            }
        }

        private void LetterFourWrong_Click(object sender, EventArgs e)
        {
            //incorrectSpot.Remove(LetterFour.Text);

            if (!correctLetters.ContainsValue(LetterFour.Text))
            {
                if (!incorrectLetters.Contains(LetterFour.Text))
                {
                    incorrectLetters.Add(LetterFour.Text);
                }
            } 
        }

        private void LetterFiveWrong_Click(object sender, EventArgs e)
        {
            //incorrectSpot.Remove(LetterFive.Text);

            if (!correctLetters.ContainsValue(LetterFive.Text))
            {
                if (!incorrectLetters.Contains(LetterFive.Text))
                {
                    incorrectLetters.Add(LetterFive.Text);
                }
            } 
        }

        // <--- Correct Letter Wrong Spot Functions --->

        private void LetterOneWrongSpot_Click(object sender, EventArgs e)
        {
            int pos = 0;                    // Spot in word
            string letter = LetterOne.Text; // Correct letter

            AddIncorrectSpot(pos, letter);

            //if (!incorrectSpot.ContainsValue(LetterOne.Text))
            //{
            //    incorrectSpot.Add(pos, letter);
            //}
        }

        private void LetterTwoWrongSpot_Click(object sender, EventArgs e)
        {
            int pos = 1;                    // Spot in word
            string letter = LetterTwo.Text; // Correct letter

            AddIncorrectSpot(pos, letter);

            //if (!incorrectSpot.ContainsValue(letter))
            //{
            //    incorrectSpot.Add(pos, letter);
            //}
        }

        private void LetterThreeWrongSpot_Click(object sender, EventArgs e)
        {
            int pos = 2;                    // Spot in word
            string letter = LetterThree.Text; // Correct letter

            AddIncorrectSpot(pos, letter);

            //if (!incorrectSpot.ContainsValue(letter))
            //{
            //    incorrectSpot.Add(pos, letter);
            //}
        }

        private void LetterFourWrongSpot_Click(object sender, EventArgs e)
        {
            int pos = 3;                    // Spot in word
            string letter = LetterFour.Text; // Correct letter

            AddIncorrectSpot(pos, letter);

            //if (!incorrectSpot.ContainsValue(letter))
            //{
            //    incorrectSpot.Add(pos, letter);
            //}
        }

        private void LetterFiveWrongSpot_Click(object sender, EventArgs e)
        {
            int pos = 4;                    // Spot in word
            string letter = LetterFive.Text; // Correct letter

            AddIncorrectSpot(pos, letter);

            //if (!incorrectSpot.ContainsValue(letter))
            //{
            //    incorrectSpot.Add(pos, letter);
            //}
        }

        // <--- Correct Letter Functions --->

        private void LetterOneCorrect_Click(object sender, EventArgs e)
        {
            int pos = 0;                    // Spot in word
            string letter = LetterOne.Text; // Correct letter

            AddCorrectLetter(pos, letter);
        }

        private void LetterTwoCorrect_Click(object sender, EventArgs e)
        {
            int pos = 1;                    // Spot in word
            string letter = LetterTwo.Text; // Correct letter

            AddCorrectLetter(pos, letter);
        }

        private void LetterThreeCorrect_Click(object sender, EventArgs e)
        {
            int pos = 2;                    // Spot in word
            string letter = LetterThree.Text; // Correct letter

            AddCorrectLetter(pos, letter);
        }

        private void LetterFourCorrect_Click(object sender, EventArgs e)
        {
            int pos = 3;                    // Spot in word
            string letter = LetterFour.Text; // Correct letter

            AddCorrectLetter(pos, letter);
        }

        private void LetterFiveCorrect_Click(object sender, EventArgs e)
        {
            int pos = 4;                    // Spot in word
            string letter = LetterFive.Text; // Correct letter

            AddCorrectLetter(pos, letter);
        }
    }
}
