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
        /// Структура "узел дерева"
        /// </summary>
        public struct huffmannTreeNode
        {
            /// <summary>
            /// Текст
            /// </summary>
            public string text;
            /// <summary>
            /// Двоичный код
            /// </summary>
            public string code;
            /// <summary>
            /// Частота встречаемости
            /// </summary>
            public float frequency;

            public huffmannTreeNode(string t, string c, float f)
            {
                text = t;
                code = c;
                frequency = f;
            }
        };

        /// <summary>
        /// Частота встречаемости отдельных символов алфавита
        /// </summary>
        static Dictionary<char, float> freqs = new Dictionary<char, float>();

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
            string text = "";
            bool flag = true;
            while (flag)
            {
                Console.Write("Введите символ: ");
                char inputChar = char.Parse(Console.ReadLine());
                text += inputChar;
                Console.Write("Введите частоту сивола: ");
                float inputFreq = float.Parse(Console.ReadLine());
                freqs.Add(inputChar, inputFreq);
                Console.Write("Продолжить(1/0)? ");
                byte key = byte.Parse(Console.ReadLine());
                if (key == 1) continue;
                else break;
            }

            //Начальное заполнение деревьев
            foreach (KeyValuePair<char, float> Pair in freqs)
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
                        newRes[index] = new huffmannTreeNode(newRes[index].text, "1" + newRes[index].code, newRes[index].frequency);
                    }
                    else if (tree[tree.Count - 1].text.Contains(source[index].text))
                    {
                        newRes[index] = new huffmannTreeNode(newRes[index].text, "0" + newRes[index].code, newRes[index].frequency);
                    }
                }

                tree[tree.Count - 2] = new huffmannTreeNode(tree[tree.Count - 2].text + tree[tree.Count - 1].text, "",
                    tree[tree.Count - 2].frequency + tree[tree.Count - 1].frequency);
                tree.RemoveAt(tree.Count - 1);
            }

            //Выводим алфавит на экран
            var sortNewRes = from r in newRes
                             orderby r.frequency descending
                             select r;
            Console.WriteLine("\n|Символ|Частота|Код|");
            foreach (var r in sortNewRes)
            {
                Console.WriteLine($"|  " + r.text + "   |" + r.frequency + "|" + r.code + "|");
            }
            //for (int index = 0; index < source.Count; index++)
            //{
            //    Console.WriteLine("|  "+ sortNewRes[index].text +"   |" + sortNewRes[index].frequency + "|" + newRes[index].code + "|");
            //}

            Console.WriteLine("Нажмите Enter, чтобы выйти");
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
