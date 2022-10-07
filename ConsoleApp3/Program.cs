using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MethodHuffman
{
    class Program
    {
        /// <summary>
        /// Структура "узел H-дерева"
        /// </summary>
        public struct huffmannTreeNode
        {
            /// <summary>
            /// Текст
            /// </summary>
            public String text;
            /// <summary>
            /// Двоичный код
            /// </summary>
            public String code;
            /// <summary>
            /// Частота встречаемости
            /// </summary>
            public int frequency;

            public huffmannTreeNode(String t, String c, int f)
            {
                text = t;
                code = c;
                frequency = f;
            }
        };

        /// <summary>
        /// Частота встречаемости отдельных символов алфавита
        /// </summary>
        static Dictionary<char, int> freqs = new Dictionary<char, int>();
        /// <summary>
        /// Исходное дерево
        /// </summary>
        static List<huffmannTreeNode> source = new List<huffmannTreeNode>();
        /// <summary>
        /// Вспомогательное дерево
        /// </summary>
        static List<huffmannTreeNode> newRes = new List<huffmannTreeNode>();
        /// <summary>
        /// Еще какое-то дерево
        /// </summary>
        static List<huffmannTreeNode> tree = new List<huffmannTreeNode>();

        static void Main(string[] args)
        {
            string text = Console.ReadLine();

            //Считаем частоту
            for (int index = 0; index < text.Length; index++)
            {
                if (freqs.Keys.Contains(text[index]))
                {
                    freqs[text[index]]++;
                }
                else
                {
                    freqs.Add(text[index], 1);
                }
            }

            //Начальное заполнение деревьев
            foreach (KeyValuePair<char, int> Pair in freqs)
            {
                source.Add(new huffmannTreeNode(Pair.Key.ToString(), "", Pair.Value));
                tree.Add(new huffmannTreeNode(Pair.Key.ToString(), "", Pair.Value));
                newRes.Add(new huffmannTreeNode(Pair.Key.ToString(), "", Pair.Value));
            }

            //Переводим в биты
            string textAsBytes = "";
            BitArray btr = new BitArray(Encoding.ASCII.GetBytes(text));

            for (int index = 0; index < btr.Count; index++)
            {
                textAsBytes += (btr[index] ? "1" : "0");
            }

            //Строим дерево
            while (tree.Count > 1)
            {
                sortTree();

                for (int index = 0; index < source.Count; index++)
                {
                    if (tree[tree.Count - 2].text.Contains(source[index].text))
                    {
                        newRes[index] = new huffmannTreeNode(newRes[index].text, "0" + newRes[index].code, newRes[index].frequency);
                    }
                    else if (tree[tree.Count - 1].text.Contains(source[index].text))
                    {
                        newRes[index] = new huffmannTreeNode(newRes[index].text, "1" + newRes[index].code, newRes[index].frequency);
                    }
                }

                tree[tree.Count - 2] = new huffmannTreeNode(tree[tree.Count - 2].text + tree[tree.Count - 1].text, "",
                    tree[tree.Count - 2].frequency + tree[tree.Count - 1].frequency);
                tree.RemoveAt(tree.Count - 1);
            }

            //Выводим алфавит на экран
            for (int index = 0; index < source.Count; index++)
            {
                Console.WriteLine(newRes[index].text + " (" + newRes[index].code + ")");
            }
            string text2 = "";
            //Битовая последовательность с новыми кодами
            for (int index = 0; index < text.Length; index++)
            {
                foreach (huffmannTreeNode htn in newRes)
                {
                    if (htn.text == text[index].ToString())
                    {
                        text2 += htn.code;
                    }
                }
            }
            Console.WriteLine(text2);
            Console.ReadLine();
        }

        /// <summary>
        /// Сортировка узлов дерева по убыванию
        /// </summary>
        static void sortTree()
        {
            for (int index = 0; index < tree.Count - 1; index++)
            {
                for (int index2 = index; index2 < tree.Count; index2++)
                {
                    if (tree[index].frequency < tree[index2].frequency)
                    {
                        huffmannTreeNode buf = tree[index];
                        tree[index] = tree[index2];
                        tree[index2] = buf;
                    }
                }
            }
        }
    }
}
