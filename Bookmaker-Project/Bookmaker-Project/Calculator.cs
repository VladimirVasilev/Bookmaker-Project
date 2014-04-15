using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmakerProject
{
    public static class Calculator<T>
        where T : IFormattable, IComparable, IConvertible
    {
        private static dynamic[] readyColumns;
        private static ICollection<T> coefficients;
        private static dynamic coefFromColumn;

        public static int ColumnsCount { get; private set; }

        public static decimal CalcProfitFromSingleCol(ICollection<T> coefList, decimal bet)
        {
            coefFromColumn = CalcCoefficientsFromSingleCol(coefList);
            return coefFromColumn * bet;
        }

        public static decimal CalcProfitFromCombination(ICollection<T> coefList, int[] combinations, decimal bet)
        {
            coefficients = coefList;
            ColumnsCount = 0;
            coefFromColumn = 0;

            if (combinations.Length == 1)
            {
                readyColumns = new dynamic[combinations[0]];
                Recursion(0, 0);
                return coefFromColumn * bet;
            }
            else
            {
                for (int i = 0; i < combinations.Length; i++)
                {
                    readyColumns = new dynamic[combinations[i]];
                    Recursion(0, 0);
                }
                return coefFromColumn * bet;
            }
        }

        private static decimal CalcCoefficientsFromSingleCol(ICollection<T> coefList)
        {
            dynamic addedCoefficients = 1.0m;

            foreach (var value in coefList)
            {
                addedCoefficients *= value;
            }

            return (decimal)addedCoefficients;
        }

        private static dynamic ListProduct()
        {
            dynamic result = 1;

            foreach (var item in readyColumns)
            {
                result *= item;
            }

            return result;
        }

        private static void Recursion(int indexReadyCol, int indexGames)
        {
            if (indexReadyCol == readyColumns.Length)
            {
                coefFromColumn += ListProduct();
                ColumnsCount++;
                return;
            }
            else
            {
                for (int i = indexGames; i < coefficients.Count; i++)
                {
                    readyColumns[indexReadyCol] = coefficients.ElementAt(i);
                    Recursion(indexReadyCol + 1, i + 1);
                }
            }
        }
    }
}
