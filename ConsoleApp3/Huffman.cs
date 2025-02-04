﻿using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.IO;
using System;

namespace MethodHuffman
{
    class Huffman
    {
        private List<Node> nodes = new List<Node>();
        public Node Root { get; set; }
        public Dictionary<char, float> Frequencies = new Dictionary<char, float>();

        public void Build(Dictionary<char, float> freq)
        {
            //for (int i = 0; i < source.Length; i++)
            //{
            //    if (!Frequencies.ContainsKey(source[i]))
            //    {
            //        Frequencies.Add(source[i], 0);
            //    }
            //
            //    Frequencies[source[i]]++;
            //}
            Frequencies = freq;
            foreach (KeyValuePair<char, float> symbol in Frequencies)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
            }

            while (nodes.Count > 1)
            {
                List<Node> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

                if (orderedNodes.Count >= 2)
                {
                    // Берем первые два элемента
                    List<Node> taken = orderedNodes.Take(2).ToList<Node>();

                    // Создание родительского узла, объединив частоты
                    Node parent = new Node()
                    {
                        Symbol = '*',
                        Frequency = taken[0].Frequency + taken[1].Frequency,
                        Left = taken[0],
                        Right = taken[1]
                    };

                    nodes.Remove(taken[0]);
                    nodes.Remove(taken[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes.FirstOrDefault();

            }

        }

        public BitArray Encode(string source)
        {
            List<bool> encodedSource = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
            }

            BitArray bits = new BitArray(encodedSource.ToArray());

            return bits;
        }

        //public string Decode(BitArray bits)
        //{
        //    Node current = this.Root;
        //    string decoded = "";

        //    foreach (bool bit in bits)
        //    {
        //        if (bit)
        //        {
        //            if (current.Right != null)
        //            {
        //                current = current.Right;
        //            }
        //        }
        //        else
        //        {
        //            if (current.Left != null)
        //            {
        //                current = current.Left;
        //            }
        //        }

        //        if (IsLeaf(current))
        //        {
        //            decoded += current.Symbol;
        //            current = this.Root;
        //        }
        //    }

        //    return decoded;
        //}

        //public bool IsLeaf(Node node)
        //{
        //    return (node.Left == null && node.Right == null);
        //}
    }
}
