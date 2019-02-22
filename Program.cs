using System;
using System.Collections.Generic;

namespace URLEncoder
{
    class Program
    {
        static string urlFormatString = "https://companyserver.com/content/{0}/files/{1}/{1}Report.pdf";
        static void Main(string[] args)
        {
            Console.WriteLine("URL Encoder");

            do
            {
                Console.Write("\n Project Name: ");
                string ProjectName = GetUserInput();
                Console.Write("Activity Name: ");
                string ActivityName = GetUserInput();

                Console.WriteLine(CreateURL(ProjectName, ActivityName));

                Console.Write("Would you like to generate another URL? (y/n): ");
            } while (Console.ReadLine().ToLower().Equals("y"));
        }

        static string CreateURL(string ProjectName, string ActivityName)
        {
          return String.Format(urlFormatString, (ProjectName), Encode(ActivityName));
        }

        static string GetUserInput()
        {
            string input = "";
            do
            {
                input = Console.ReadLine();
                if (IsValid(input)) return input;
                Console.Write("This input contains invalid characters. Please enter again: ");
            } while (true);
        }

        static bool IsValid(string input)
        {
            foreach(char character in input.ToCharArray())
            {
                if(character <= 0x1F && character >= 0x00 || character == 0x7F)
                {
                    return false;
                }
            }
            return true;
        }

        static Dictionary<string, string> hex = new Dictionary<string, string>()
        {
            {"<", "%3C"}, {">", "%3E"}, {"#", "%23"}, {"%", "%25" }, 
            {"\"", "%22"}, {";", "%3B"}, {"/", "%2F"}, {"?", "%3F"}, 
            {":", "%3A"}, {"@", "%40"}, {"=", "%3D"}, {"+", "%2B"}, 
            {"$", "%24"}, {",", "%2C"}, {"{", "%7B"}, {"}", "%7D"}, 
            {"|", "%7C"}, {"\\", "%5C"}, {"^", "%5E"}, {"[", "%5B"}, 
            {"]", "%5D"}, {"`", "%60"}, {" ", "%20"}
        };

        static string Encode(string value)
        {
            string encodedValue = "";
            foreach(char character in value.ToCharArray())
            {
                string charstring = character.ToString();
                encodedValue += hex.GetValueOrDefault(charstring, charstring);
            }
            return encodedValue;
        }
    }
}

// <(3C) >(3E) #(23) %(25) "(22) ;(3B) /(2F) ?(3F) :(3A) @(40) =(3D) +(2B) $(24),( {(7B) }(7D) |(7C) \(5C) ^(5E) [(5B) ](5D) `(60)