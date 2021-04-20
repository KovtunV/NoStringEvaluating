using System.Collections.Generic;
using System.Linq;
using NoStringEvaluatingTests.Model;

namespace NoStringEvaluatingTests.Formulas
{
    public static partial class FormulasContainer
    {
        public static IEnumerable<FormulaModel[]> GetWordFormulas()
        {
            var numberList = new[] { 1d, 2d, 3d }.ToList();
            var wordList = new[] { "one", "Two" }.ToList();
            var word = "one two kovtun loves painting two";

            // Word
            yield return CreateTestModel("\"my word\"", "my word");

            // Contat
            yield return CreateTestModel("Concat('world'; 5)", "world5");
            yield return CreateTestModel("Concat(5; 6; 7)", "567");
            yield return CreateTestModel("Concat('one'; 'two')", "onetwo");
            yield return CreateTestModel("Concat(myList; ' *')", "123 *", ("myList", numberList));
            yield return CreateTestModel("Concat(myList; ' * '; myList2)", "123 * oneTwo", ("myList", numberList), ("myList2", wordList));

            // Implode
            yield return CreateTestModel("Implode(1; 5; \"-\")", "1-5");
            yield return CreateTestModel("Implode(wordList)", "one Two", ("wordList", wordList));
            yield return CreateTestModel("Implode(wordList; ' * ')", "one * Two", ("wordList", wordList));
            yield return CreateTestModel("Implode(wordList; 5; ' * ')", "one * Two * 5", ("wordList", wordList));
            yield return CreateTestModel("Implode(1; wordList; 5; ' * ')", "1 * one * Two * 5", ("wordList", wordList));
            yield return CreateTestModel("Implode(numberList; ' * ')", "1 * 2 * 3", ("numberList", numberList));

            // Left
            yield return CreateTestModel("Left('Hello world')", "H");
            yield return CreateTestModel("Left('Hello world'; 4)", "Hell");
            yield return CreateTestModel("Left('Hello world'; -4)", "");
            yield return CreateTestModel("Left('Hello world'; 40)", "Hello world");
            yield return CreateTestModel("Left('Hello world'; 'elHo w')", "Hello wo");

            // Lower
            yield return CreateTestModel("Lower('HellO')", "hello");
            yield return CreateTestModel("Lower('HE')", "he");
            yield return CreateTestModel("Lower('ONE tWo')", "one two");
            yield return CreateTestModel("Lower(Concat(wordList))", "onetwo", ("wordList", wordList));

            // Middle
            yield return CreateTestModel("Middle(word; 0; 3)", "one", ("word", word));
            yield return CreateTestModel("Middle(word; 4; 3)", "two", ("word", word));
            yield return CreateTestModel("Middle(word; -56; 3)", "", ("word", word));
            yield return CreateTestModel("Middle(word; 56; 3)", "", ("word", word));
            yield return CreateTestModel("Middle(word; 4; 30)", "two kovtun loves painting two", ("word", word));
            yield return CreateTestModel("Middle(word; 8; 'two')", "kovtun loves painting ", ("word", word));
            yield return CreateTestModel("Middle(word; 'two'; 7)", " kovtun", ("word", word));
            yield return CreateTestModel("Middle(word; 'two '; ' two')", "kovtun loves painting", ("word", word));

            // Proper
            yield return CreateTestModel("Proper(word)", "One Two Kovtun Loves Painting Two", ("word", word));
            yield return CreateTestModel("Proper('i am Crazy about you')", "I Am Crazy About You", ("word", word));

            // Replace
            yield return CreateTestModel("Replace(word; 'two'; 'Table')", "one Table kovtun loves painting Table", ("word", word));
            yield return CreateTestModel("Replace('rttttr'; 't'; 'ol')", "rololololr");

            // Right
            yield return CreateTestModel("Right('Hello world')", "d");
            yield return CreateTestModel("Right('Hello world'; 4)", "orld");
            yield return CreateTestModel("Right('Hello world'; -4)", "");
            yield return CreateTestModel("Right('Hello world'; 40)", "Hello world");
            yield return CreateTestModel("Right('Hello world'; 'wo rld')", "llo world");

            // Text
            yield return CreateTestModel("Text('Hello world')", "Hello world");
            yield return CreateTestModel("Text(5+6)", "11");
            yield return CreateTestModel("Text(ToDateTime('01.01.2021'))", "01/01/2021 00:00:00");

            // Upper
            yield return CreateTestModel("Upper('HellO')", "HELLO");
            yield return CreateTestModel("Upper('he')", "HE");
            yield return CreateTestModel("Upper('ONE tWo')", "ONE TWO");
            yield return CreateTestModel("Upper(Concat(wordList))", "ONETWO", ("wordList", wordList));
        }

        public static IEnumerable<FormulaModel[]> GetWordAsNumberFormulas()
        {
            // Len
            yield return CreateTestModel("Len('my word')", 7);
            yield return CreateTestModel("Len('my' + 'ddd')", 5);
        }
    }
}
