using System;

/// <summary>
/// Program shows a way how to autocomplete words 
/// or sentences considering rating of words.
/// I use data structure named Trie.
/// </summary>

namespace AutoComplete_Trie_
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie trie = new Trie();

            // argument 1 - word, 2 - number of uses of this word
            // autoComplete algorithm shows you first 6 the most used words
            trie.Add("hello", 2);
            trie.Add("hell", 3);
            trie.Add("hi", 1);
            trie.Add("helloween", 2);
            trie.Add("hero", 4);
            trie.Add("haha", 2);
            trie.Add("hachiko", 5);
            trie.Add("hachiko film", 8); 

            foreach(var i in trie.AutoComplete("h"))
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();
            foreach (var i in trie.AutoComplete("ha"))
            {
                Console.WriteLine(i);
            }

            Console.ReadKey();
        }
    }
}
