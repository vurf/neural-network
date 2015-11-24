using System;

namespace Rozenblat
{
    static class Helper
    {
        public static void WriteArray(this int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (i == 3 || i == 6 || i == 9)
                {
                    Console.WriteLine();
                }
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
