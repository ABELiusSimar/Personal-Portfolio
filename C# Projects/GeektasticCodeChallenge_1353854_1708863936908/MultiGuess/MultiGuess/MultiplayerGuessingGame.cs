using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/* Core-loop:
*1. Start:
*   1.a. A game is initialized with a list of words of the same length in set order
*   1.b. The words in the list are partially hidden with '*'
*   1.c. Words are displayed to the players to see
*2. Gameplay:
*   2.a. Multiple players can play the game
*   2.b. Players guess the hidden letters of the word by submitting a word of the same length
*       2.b.i. Valid submissions are:
*             - Same length
*             - Matches the shown letters in the same order
*             - Existing English word (Can be check using Exists() in Vocabulary Checker)
*   2.c. Letters that are matched between submission and hidden are revealed
*   2.d. Players are awarded with points based on the number of letters revealed in each word
*       2.d.i. If an exact match, immediately scores ten points and letters on the other words are not revealed, only the matching word
*       2.d.ii. No extra points are awarded for submitting a revealed word
*3. Finish:
*   3.a. Scores are totaled
*   3.b. Player with the higher score wins
*/

namespace MultiGuess
{
    internal class MultiPlayerGuessingGame : IMultiplayerGuessingGame
    {
        // Variables
        public bool IsStart = false;
        public bool IsEnd = false;
        public IList<string> GameWords = new List<string>();
        public IList<string> NonHiddenWords = new List<string>();
        public int FinalScore;

        public void Main(string[] args)
        {
            // Main loop
            Start();
            FinalScore = SubmitGuess("Player 1", "absent"); // Technically this function should keep looping until the game ends, but I did not see an end requirement in the description
            End();

            // Display final score
            Console.WriteLine($"Player 1 score is: {FinalScore}");
        }

        // Function to initialize the game
        public void Start()
        {
            // Game is starting
            IsStart = true;
            IsEnd = false;
            // Get all the words
            GameWords = GetGameStrings();
            // Reveal to players
            for (int i = 0; i < GameWords.Count; i++)
            {
                Console.WriteLine(GameWords[i]);
            }
        }

        // Function to handle the end of the game
        public void End()
        {
            // Game is finishing
            IsStart = false;
            IsEnd = true;
            // Get all the words again
            GameWords = GetGameStrings();
            // Reveal the words again
            for (int i = 0; i < GameWords.Count; i++)
            {
                Console.WriteLine(GameWords[i]);
            }
        }

        /// <summary>
        /// Returns a list of partially or completely revealed game words.
        /// </summary>
        /// <returns>The game words.</returns>
        public IList<string> GetGameStrings()
        {
            List<string> tempWords = new List<string>();

            // If game not start, initialized the game by grabbing words from the word list
            if (IsStart == true)
            {
                // Generate two randoms, one for how many words and one for how many letters
                int manyWords = new Random().Next(5);
                int manyLetters = new Random().Next(5);
                // Read the word list and add words to the "tempWords" list
                StreamReader? reader = null;
                try
                {
                    reader = new StreamReader(new FileStream("wordlist.txt", FileMode.OpenOrCreate));

                    var content = reader.ReadToEndAsync();

                    List<string> stringList = content.Result.Split('\n').ToList();

                    // Add words into the "tempWords" list
                    for (int i = 0; i < manyWords; i++) 
                    {
                        // Only add words of matching letter length
                        if (stringList[i].Length == manyLetters)
                        {
                            tempWords.Add(stringList[i]);
                        }
                    }

                    // Store the non hidden words into a buffer for referencing
                    NonHiddenWords = tempWords;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    reader?.Dispose();
                }

                // Now hide some of the letters.
                // To do
            }
            // Game is finished, retrieve the words
            else if (IsEnd == true)
            {
                for (int i = 0; i < GameWords.Count; i++)
                {
                    tempWords.Add(GameWords[i]);
                }
            }
            return tempWords;
        }

        /// <summary>
        /// Submits a guess against the game words.
        /// </summary>
        /// <param name="playerName">The player name.</param>
        /// <param name="submission">The guess.</param>
        /// <returns>The score that the guess produced.</returns>
        public int SubmitGuess(string playerName, string submission)
        {
            // Read through the submitted word and check the letters
            char[] letter = submission.ToCharArray();
            char[] matchedLetters = new char[letter.Length];
            // A value to increment score based on letters revealed
            int score = 0;
            // Read through the words list and see if any of the letters matched
            for (int i = 0; i < NonHiddenWords.Count; i++)
            {
                // In case that submission is an exact match, immediately gain 10 points
                if (submission == NonHiddenWords[i])
                {
                    score = 10;
                    break;
                }
                // Read the letters of each word
                for (int j = 0; j < NonHiddenWords[i].Length; j++)
                {
                    char[] temp = NonHiddenWords[i].ToCharArray();
                    // The same letter is found
                    if (temp[j] == letter[j])
                    {
                        // Save the letters
                        matchedLetters[j] = letter[j];
                        score++;
                    }
                    // Reveal the letter(s)
                    // To Do

                    // Update the words
                    for (int z = 0; z < GameWords.Count; z++)
                    {
                        Console.WriteLine(GameWords[z]);
                    }
                }
            }
            return score;
        }
    }
}
