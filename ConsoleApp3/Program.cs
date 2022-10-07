using System;
using System.Collections;
using System.Collections.Generic;

namespace MethodHuffman
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, float> freq = new Dictionary<char, float>();
            string input = "";
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Введите символ: ");
                char inputChar = char.Parse(Console.ReadLine());
                input += inputChar;
                Console.WriteLine("Введите частоту сивола: ");
                float inputFreq = float.Parse(Console.ReadLine());
                freq.Add(inputChar, inputFreq);
                Console.WriteLine("Продолжить(1/0)?");
                byte key = byte.Parse(Console.ReadLine());
                if (key == 1) continue;
                else break;
            }
            Huffman huffman = new Huffman();

            // Построение дерева Хаффмана
            huffman.Build(freq);

            // Кодировка
            BitArray encoded = huffman.Encode(input);

            Console.Write("Код: ");
            foreach (bool bit in encoded)
            {
                Console.Write((bit ? 1 : 0) + "");
            }
            Console.WriteLine();

            //// Decode
            //string decoded = huffman.Decode(encoded);

            //Console.WriteLine("Decoded: " + decoded);
            Console.WriteLine("Нажмите Enter, чтобы выйти");
            Console.ReadLine();
        }
    }
}
