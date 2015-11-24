using System;
using System.Collections.Generic;

namespace Rozenblat
{
    class Neuro
    {
        public int[] x1 = new int[12] { 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 1, 1 }; //0
        public int[] x2 = new int[12] { 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 }; //1
        public int[] x3 = new int[12] { 1, 0, 1, 1, 0, 1, 1, 1, 1, 0, 0, 1 }; //4

        public int[] w1 = new int[12] { 1, 2, 1, 9, 2, 1, 1, 7, 1, 1, 1, 1 };
        public int[] w2 = new int[12] { 1, 6, 8, 4, 1, 8, 2, 1, 1, 3, 6, 1 };
        public int[] w3 = new int[12] { 3, 1, 1, 1, 3, 1, 5, 1, 7, 1, 2, 1 };

        private int[,] d = new int[3, 3] {{1, 0, 0}, {0, 1, 0}, {0, 0, 1}};

        public void Start()
        {
            var enters = new[] {x1, x2, x3};
            var weights = new[] {w1, w2, w3};
            var outers = new List<ResultModel>();

            for (int i = 0; i < enters.Length; i++)
            {
                for (int j = 0; j < weights.Length; j++)
                {
                    var error = -1;

                    var model = new ResultModel();
                    model.Weight = j;
                    model.Enter = i;
                    model.Iterations = 0;
                    model.Array = new int[weights[j].Length];

                    do
                    {
                        var array = Multiplication(enters[i], weights[j]);
                        var sum = Summation(array);

                        var y = sum <= 60 ? 0 : 1;
                        error = y;
                        var delta = d[i, j] - y;
                        for (int k = 0; k < weights[j].Length; k++)
                        {
                            weights[j][k] = weights[j][k] + 1 * delta * enters[i][k];
                        }

                        model.Iterations += 1;
                        model.Error = error;
                        weights[j].CopyTo(model.Array, 0);

                        if (error == d[i, j])
                        {
                            outers.Add(model);
                        }
                    } while (error != d[i, j]);
                }
            }

            WriteResult(outers);


        }

        private static void WriteModel(ResultModel model)
        {
            var str = string.Format("Входной образ: {0}. Вес: {1}. Кол-во итераций: {2}. Эталон: {3}.",
                model.Enter,
                model.Weight,
                model.Iterations,
                model.Error
                );
            Console.WriteLine(str);
            model.Array.WriteArray();
            Console.WriteLine();
        }

        void WriteResult(List<ResultModel> list)
        {
            foreach (var model in list)
            {
               WriteModel(model);
            }
        }


        int[] Multiplication(int[] x, int[] w)
        {
            var result = new int[12];
            for (int i = 0; i < x.Length; i++)
            {
                result[i] = x[i]*w[i];
            }
            return result;
        }

        int Summation(int[] current)
        {
            int result = 0;

            for (int i = 0; i < current.Length; i++)
            {
                result += current[i];
            }

            return result;
        }

    }
}
