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
///     Brief Description of Challenge:  Encrypt and Decrypt a string using the rail fence method
///
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
            RailFenceCipher encryptTest = new RailFenceCipher();
            Console.WriteLine(encryptTest.Encrypt("THEQUICKBROWNFOXJUMPSOVERTHELAZYDOG", 4));
            Console.WriteLine();
            Console.WriteLine("TCNMRZHIKWFUPETAYEUBOOJSVHLDGQRXOEO");
            Console.WriteLine();
            Console.WriteLine(encryptTest.Decrypt("TCNMRZHIKWFUPETAYEUBOOJSVHLDGQRXOEO", 4));

            Console.ReadKey();
        }

        /// <summary>
        /// Simple function to illustrate how to use tests
        /// </summary>
        /// <param name="inputInteger"></param>
        /// <returns></returns>
        public static int MyTestFunction(int inputInteger)
        {
            return inputInteger;
        }
    }

    class RailFenceCipher
    {
        int counter = 1;
        int direction = -1;

        public RailFenceCipher() { }

        public string Encrypt(string textToEncrypt, int numLines)
        {
            int lineCount = 0;
            int currentLine = 0;
            string encryptedText = string.Empty;
            while (lineCount < numLines)
            {
                currentLine = 0;
                this.counter = 1;
                for (int i = 0; i < textToEncrypt.Length; i++)
                {
                    if (currentLine == lineCount)
                    {
                        encryptedText += textToEncrypt[i];
                    }
                    currentLine += this.counter;
                    if (currentLine <= 0 || currentLine >= numLines - 1)
                    {
                        this.counter *= this.direction;
                    }
                }
                lineCount++;
            }
            return encryptedText;
        }

        public string Decrypt(string textToDecrypt, int numLines)
        {
            int lineCount = 0;
            int currentLine = 0;
            string decryptedText = textToDecrypt;
            int letterStepper = 0;
            while (lineCount < numLines)
            {
                currentLine = 0;
                this.counter = 1;
                for (int i = 0; i < textToDecrypt.Length; i++)
                {
                    if (currentLine == lineCount)
                    {
                        decryptedText = decryptedText.Insert(i, textToDecrypt[letterStepper].ToString());
                        decryptedText = decryptedText.Remove(i + 1, 1);
                        letterStepper++;
                    }
                    currentLine += this.counter;
                    if (currentLine <= 0 || currentLine >= numLines - 1)
                    {
                        this.counter *= this.direction;
                    }
                }
                lineCount++;
            }
            return decryptedText;
        }

    }


#region " TEST CLASS "

    //We need to use a Data Annotation [ ] to declare that this class is a Test class
    [TestFixture]
    class Test
    {
        //Test classes are declared with a return type of void.  Test classes also need a data annotation to mark them as a Test function
        [Test]
        public void MyValidTest()
        {
            //inside of the test, we can declare any variables that we'll need to test.  Typically, we will reference a function in your main program to test.
            int result = Program.MyTestFunction(15);  // this function should return 15 if it is working correctly
            //now we test for the result.
            Assert.IsTrue(result == 15, "This is the message that displays if it does not pass");
            // The format is:
            // Assert.IsTrue(some boolean condition, "failure message");
        }

        [Test]
        public void MyInvalidTest()
        {
            int result = Program.MyTestFunction(15);
            Assert.IsFalse(result == 14);
        }
    }
#endregion
}
