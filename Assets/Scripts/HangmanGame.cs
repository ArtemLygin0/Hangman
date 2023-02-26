using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;
using Random = UnityEngine.Random;
namespace Hangman.Scripts
{
    public class HangmanGame : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _Question;
        [SerializeField] private int hp = 7;
        [SerializeField] private TextMeshProUGUI _HP;
        [SerializeField] private TextMeshProUGUI _Result;
        [SerializeField] private TextMeshProUGUI _Clue;
        [SerializeField] private TextMeshProUGUI _WrongLetter;
        public GameObject resultPanel;

        private List<char> guessedLetters = new List<char>();
        private List<char> wrongTriedLetter = new List<char>();
        
        private string[] words =
        {
            "Rain",
            "Cube",
            "Lesson",
            "Unity",
            "Bulb",
            "Electricity",
        };

        private string[] clues =
        {
            "Falling from the sky",
            "M^3",
            "Minimum study period",
            "Game development software",
            "Light in a ball",
            "Lightning in a wire",
        };

        private string wordToGuess = "";
        //private string clue = "";

        private KeyCode lastKeyPressed;

        private void Start()
        {
            var randomIndex = Random.Range(0, words.Length);
            
            wordToGuess = words[randomIndex];            
            _Clue.text = clues[randomIndex];

        }

        void OnGUI()
        {
            Event e = Event.current;
            if (e.isKey)
            {
                // Debug.Log("Detected key code: " + e.keyCode);

                if (e.keyCode != KeyCode.None && lastKeyPressed != e.keyCode)
                {
                    ProcessKey(e.keyCode);
                    //Последняя нажатая клавиша
                    lastKeyPressed = e.keyCode;
                }
            }
        }

        private void ProcessKey(KeyCode key)
        {
            print("Key Pressed: " + key);

            char pressedKeyString = key.ToString()[0];

            string wordUppercase = wordToGuess.ToUpper();

            bool wordContainsPressedKey = wordUppercase.Contains(pressedKeyString);
            bool letterWasGuessed = guessedLetters.Contains(pressedKeyString);

            if (!wordContainsPressedKey && !wrongTriedLetter.Contains(pressedKeyString))
            {
                wrongTriedLetter.Add(pressedKeyString);
                _WrongLetter.text = string.Join(", ", wrongTriedLetter);
                hp -= 1;
                _HP.text = "HP: " + hp;
                if (hp <= 0)
                {
                    resultPanel.SetActive(true);
                    print("You Lost!");
                    _Result.text = "You lost!";
                }
            }

            if (wordContainsPressedKey && !letterWasGuessed)
            {
                guessedLetters.Add(pressedKeyString);
            }

            string stringToPrint = "";
            for (int i = 0; i < wordUppercase.Length; i++)
            {
                char letterInWord = wordUppercase[i];

                if (guessedLetters.Contains(letterInWord))
                {
                    stringToPrint += letterInWord;
                }
                else
                {
                    stringToPrint += "_";
                }
            }

            if (wordUppercase == stringToPrint)
            {
                resultPanel.SetActive(true);
                //resultPanel.GetComponent<Color>() = green;
                //resultPanel.GetComponent<Renderer>().material.color = Color.green;
                //resultPanel = GetComponent<Image>();
                //var tempColor = resultPanel.color;
                //tempColor.a = 1f;
                //resultPanel.color = tempColor;

                print("You win!");
                _Result.text = "You win!";
            }

            // print(string.Join(", ", guessedLetters));
            print(stringToPrint);
            _Question.text = stringToPrint;
        }
    }
}