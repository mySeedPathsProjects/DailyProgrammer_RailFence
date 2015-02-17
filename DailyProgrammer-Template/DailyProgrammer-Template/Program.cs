using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework; //test classes need to have the using statement

///     REDDIT DAILY PROGRAMMER SOLUTION TEMPLATE 
///                             http://www.reddit.com/r/dailyprogrammer
///     Your Name: Nate Stephens    
///     Challenge Name:  Rail Fence Ciper
///     Challenge #: 196
///     Challenge URL: http://www.reddit.com/r/dailyprogrammer/comments/2rnwzf/20150107_challenge_196_intermediate_rail_fence/
///     Brief Description of Challenge:  Encrypt and Decrypt a string using the rail fence method.
///     You will need to see the visualization used in the challenge description to understand the process
/// 
///
///     What was difficult about this challenge?
///     Figuring out how to decrypt the text, even though in the end it was only a small change from the encryption.
///     Even though i knew in my head what i wanted to do, for some reason i kept stumbling in the code, yet the answer was so simple.
///     
///
///     What was easier than expected about this challenge?
///     Writing the encyption function went very fast.  
///
///
///
///     BE SURE TO CREATE AT LEAST TWO TESTS FOR YOUR CODE IN THE TEST CLASS
///     One test for a valid entry, one test for an invalid entry.

namespace DailyProgrammer_Template
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    /// <summary>
    /// Encrypts and decrypts a string using the "Rail Fence Cipher" method
    /// </summary>
    class RailFenceCipher
    {
        int counter = 1;
        //used to change the direction of the counter (from adding to subtracting and back again)
        int direction = -1;

        //Constructor
        public RailFenceCipher() { }

        /// <summary>
        /// Encrypt the input string based on a number of lines (zig-zag height)
        /// </summary>
        /// <param name="textToEncrypt">text to encrypt</param>
        /// <param name="numLines">height of zig-zag</param>
        /// <returns>encrypted text</returns>
        public string Encrypt(string textToEncrypt, int numLines)
        {
            //initialize variables
            int lineInFocus = 0;
            int currentLine = 0;
            //return string to hold letters at they encrypted
            string encryptedText = string.Empty;

            //process each line in zig-zag one at a time until all have been completed
            while (lineInFocus < numLines)
            {
                //start at the first (highest) line
                currentLine = 0;
                this.counter = 1;
                //loop through every letter in the string
                for (int i = 0; i < textToEncrypt.Length; i++)
                {
                    //if letter in string exists in the current horizontal line being analyzed (lineInFocus) add it to the return string
                    if (currentLine == lineInFocus)
                    {
                        encryptedText += textToEncrypt[i];
                    }
                    //move to the next horizontal line
                    currentLine += this.counter;
                    //keep the currentLine within the height constrains of the zig-zag, change its direction up or down as needed
                    if (currentLine <= 0 || currentLine >= numLines - 1)
                    {
                        this.counter *= this.direction;
                    }
                }
                //after anazlying entire string move onto the next horizontal line to focus on and pick letters from
                lineInFocus++;
            }
            return encryptedText;
        }

        /// <summary>
        /// Decrypt the input string based on a number of lines (zig-zag height)
        /// </summary>
        /// <param name="textToDecrypt">text to decrypt</param>
        /// <param name="numLines">height of zig-zag</param>
        /// <returns>decrypted text</returns>
        public string Decrypt(string textToDecrypt, int numLines)
        {
            //initialize variables
            int lineInFocus = 0;
            int currentLine = 0;
            //return string to hold decrypted letter (needs to have length established at beginning and equal to encrypted input string)
            string decryptedText = textToDecrypt;
            //used to step through letters of encrypted input string one at a time
            int letterStepper = 0;
            //process each line in zig-zag one at a time until all have been completed
            while (lineInFocus < numLines)
            {
                //start at the first (highest) line
                currentLine = 0;
                this.counter = 1;
                //loop through every letter in the string
                for (int i = 0; i < textToDecrypt.Length; i++)
                {
                    //if letter in string exists in the current horizontal line being analyzed (lineInFocus)...
                    if (currentLine == lineInFocus)
                    {
                        //insert the current letter of encrypted input text (based on letterStepper) into the return string at the index where it exists in the zig-zag...hard to explain in words
                        decryptedText = decryptedText.Insert(i, textToDecrypt[letterStepper].ToString());
                        //using Insert pushes all letters in return string forward by one, so remove the proceeding index to maintain original length
                        decryptedText = decryptedText.Remove(i + 1, 1);
                        //advance the letterstepper to use the next letter in the encrypted input string
                        letterStepper++;
                    }
                    //move to the next horizontal line
                    currentLine += this.counter;
                    //keep the currentLine within the height constrains of the zig-zag, change its direction up or down as needed
                    if (currentLine <= 0 || currentLine >= numLines - 1)
                    {
                        this.counter *= this.direction;
                    }
                }
                //after anazlying entire string move onto the next horizontal line to focus on and pick letters from
                lineInFocus++;
            }
            return decryptedText;
        }

    }


#region " TEST CLASS "

    //We need to use a Data Annotation [ ] to declare that this class is a Test class
    [TestFixture]
    class Test
    {
        RailFenceCipher testObject = new RailFenceCipher();

        //Test classes are declared with a return type of void.  Test classes also need a data annotation to mark them as a Test function
        [Test]
        public void MyValidTest()
        {
            //inside of the test, we can declare any variables that we'll need to test.  Typically, we will reference a function in your main program to test.
            string result = testObject.Encrypt("THEQUICKBROWNFOXJUMPSOVERTHELAZYDOG", 4);  // this function should return 15 if it is working correctly
            //now we test for the result.
            Assert.IsTrue(result == "TCNMRZHIKWFUPETAYEUBOOJSVHLDGQRXOEO", "This is the message that displays if it does not pass");
            // The format is:
            // Assert.IsTrue(some boolean condition, "failure message");
        }

        [Test]
        public void MyInvalidTest()
        {
            string result = testObject.Decrypt("TCNMRZHIKWFUPETAYEUBOOJSVHLDGQRXOEO", 4);
            Assert.IsFalse(result != "THEQUICKBROWNFOXJUMPSOVERTHELAZYDOG");
        }
    }
#endregion
}
