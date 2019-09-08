using AutoComplete_Trie_Node;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoComplete_Trie_
{
    class Trie
    {
        private Node root;

        public Trie()
        {
            root = new Node('\0', default(int), "");
        }

        #region Add word

        public void Add(string key, int data)
        {
            AddNode(key, data, root);
        }

        private void AddNode(string key, int data, Node node)
        {
            if (string.IsNullOrEmpty(key))
            {
                if (!node.IsWord)
                {
                    node.Data = data;
                    node.IsWord = true;
                }
            }
            else
            {
                var symbol = key[0];
                var subnode = node.TryFind(symbol);
                if (subnode != null)
                {
                    AddNode(key.Substring(1), data, subnode);
                }
                else
                {
                    var newNode = new Node(key[0], data, node.Prefix + symbol);
                    node.SubNodes.Add(key[0], newNode);
                    AddNode(key.Substring(1), data, newNode);
                }
            }
        }

        #endregion

        #region Remove word

        public void Remove(string key)
        {
            RemoveNode(key, root);
        }

        private void RemoveNode(string key, Node node)
        {
            if (string.IsNullOrEmpty(key))
            {
                if (node.IsWord)
                {
                    node.IsWord = false;
                }
            }
            else
            {
                var subnode = node.TryFind(key[0]);
                if (subnode != null)
                {
                    RemoveNode(key.Substring(1), subnode);
                }
            }
        }

        #endregion

        #region Search

        public bool TrySearch(string key, out int value)
        {
            bool result = false;
            Node temp = SearchNode(key, root);

            if(temp != null && temp.IsWord)
            {
                value = temp.Data;
                result = true;
            }
            else
            {
                value = default(int);
            }

            return result;
        }

        private Node SearchNode(string key, Node node)
        {
            Node result = null;

            if (string.IsNullOrEmpty(key))
            {
                result = node;
            }
            else
            {
                var subnode = node.TryFind(key[0]);
                if (subnode != null)
                {
                    result = SearchNode(key.Substring(1), subnode);
                }
            }

            return result;
        }

        #endregion

        #region AutoComplete

        private List<Tuple<string, int>> AutoCompleteResult = new List<Tuple<string, int>>();

        public List<string> AutoComplete(string partKey)
        {
            Node temp = SearchNode(partKey, root);

            if (temp != null)
            {
                AutoCompleteResult.Clear();
                SearchAllNodes(temp);
            }

            var s = (from el in AutoCompleteResult
                    orderby el.Item2 descending
                    select el.Item1).Take(6).ToList();

            return s;
        }

        private void SearchAllNodes(Node node)
        {
            foreach(var subnode in node.SubNodes)
            {
                SearchAllNodes(subnode.Value);

                if (subnode.Value.IsWord)
                {
                    AutoCompleteResult.Add(new Tuple<string, int>(subnode.Value.Prefix, subnode.Value.Data));
                }
            }
        }

        #endregion
    }
}
