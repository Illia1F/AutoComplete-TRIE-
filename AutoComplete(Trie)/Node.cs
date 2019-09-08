using System.Collections.Generic;

namespace AutoComplete_Trie_Node
{
    class Node
    {
        public char Symbol { get; set; }
        public int Data { get; set; }
        public bool IsWord { get; set; }
        public string Prefix { get; set; }
        public Dictionary<char, Node> SubNodes { get; set; }

        public Node(char symbol, int data, string prefix)
        {
            Symbol = symbol;
            Data = data;
            SubNodes = new Dictionary<char, Node>();
            Prefix = prefix;
        }

        public override string ToString()
        {
            return $"{Data} [{SubNodes.Count}] ({Prefix})";
        }

        public Node TryFind(char symbol)
        {
            if (SubNodes.TryGetValue(symbol, out Node value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Node item)
                return Data.Equals(item);
            else
                return false;
        }
    }
}
