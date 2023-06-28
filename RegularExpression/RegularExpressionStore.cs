using System.Text.Json;
using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Data.SqlTypes;

namespace RegularExpression
{
    public static class RegularExpressionStore
    {
        // should return a bool indicating whether the input string is
        // a valid team international email address: firstName.lastName@domain (serhii.mykhailov@teaminternational.com etc.)
        // address cannot contain numbers
        // address cannot contain spaces inside, but can contain spaces at the beginning and end of the string
        public static bool Method1(string input)
        {

            string pattern = @"\s*[a-zA-Z]+.[a-zA-Z]+@(teaminternational).(com)\s*$";


            return Regex.IsMatch(input, pattern);

        }

        // the method should return a collection of field names from the json input
        public static IEnumerable<string> Method2(string inputJson)
        {

            string pattern = @"(?<={\""|,\"")(F|_)\w+";

            Console.WriteLine(inputJson);

            IEnumerable<string> fieldNames = Regex.Matches(inputJson, pattern)
                .Cast<Match>()
                .Select(match => match.Value);

            return fieldNames;
        }

        // the method should return a collection of field values from the json input
        public static IEnumerable<string> Method3(string inputJson)
        {

            string pattern = @"(?<=\"":|\"":\"")[\w]+";

            Console.WriteLine(inputJson);

            IEnumerable<string> fieldValues = Regex.Matches(inputJson, pattern)
                .Cast<Match>()
                .Select(match => match.Value);
      
            return fieldValues;
        }

        // the method should return a collection of field names from the xml input
        public static IEnumerable<string> Method4(string inputXml)
        {

            string pattern = @"(?<=<)(F|_)\w*";

            IEnumerable<string> fieldNames = Regex.Matches(inputXml, pattern)
                 .Cast<Match>()
                 .Select(match => match.Value);


          
            return fieldNames;
        }

        // the method should return a collection of field values from the input xml
        // omit null values
        public static IEnumerable<string> Method5(string inputXml)
        {
            string pattern = @"(?<=>)\w+";

            IEnumerable<string> fieldValues = Regex.Matches(inputXml, pattern)
                 .Cast<Match>()
                 .Select(match => match.Value);

     

            return fieldValues;
        }

        // read from the input string and return Ukrainian phone numbers written in the formats of 0671234567 | +380671234567 | (067)1234567 | (067) - 123 - 45 - 67
        // +38 - optional Ukrainian country code
        // (067)-123-45-67 | 067-123-45-67 | 38 067 123 45 67 | 067.123.45.67 etc.
        // make a decision for operators 067, 068, 095 and any subscriber part.
        // numbers can be separated by symbols , | ; /

        public static IEnumerable<string> Method6(string input)
        {
           
            string[] phoneNumbers = input.Split(new char[] { ',', '|',';' });
          
            string pattern = @"^(38)\s(06[7-8])\s(\d{3})\s(\d{2})\s(\d{2})";
            string replacement = "+38 $2 $3 $4 $5";


            string[] resultArray = phoneNumbers.Select(input => Regex.IsMatch(input, pattern) ? Regex.Replace(input, pattern, "+$1 $2 $3 $4 $5") : input).ToArray();

            string pattern2 = @"^(0|\+|\(|\S)(95|06[6-8]\)|67-|67\.|68|38)(\s-\s|-|\.|\s|)(06[6-8]|)(\d{3}-\d{2}-\d{2}|\s-\s\d{3}\s-\s\d{2}\s-\s\d{2}|\s\d{3}\s\d{2}\s\d{2}|\d{3}\.\d{2}\.\d{2}|\d{4}\s\d{3}|\d{7})";
            IEnumerable<string> result = resultArray
                                                  .SelectMany(item => Regex.Matches(item, pattern2)
                                                  .Cast<Match>())
                                                  .Select(match => match.Value)
                                                  .ToList();   
            return result;
        }


    }
}
