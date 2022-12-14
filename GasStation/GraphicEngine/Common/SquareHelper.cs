using GasStation.ConstructorEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.GraphicEngine.Common
{
    public static class SquareHelper
    {
        /// <summary>
        ///  036
        ///  147
        ///  258
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="areaSquares"></param>
        /// <param name="square"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        static public T[] GetArroundSquares<T>(T[] areaSquares, Square square, int height, int width)
            where T : Square
        {
            var squares = new T[9];
            var squareIdDes = square.Id / height;
            int k = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var index = square.Id + i * height + j;
                    if (index < width * height
                        && index >= 0
                        && index / height - squareIdDes == i)
                    {
                        squares[k] = areaSquares[index];
                    }
                    else
                    {
                        squares[k] = null;
                    }

                    k++;
                }
            }
            return squares;
        }

        static public T GetArroundSquare<T>(T[] areaSquares, Square square, int height, int width, Side side)
            where T : Square
        {
           var arroundSqaures = GetArroundSquares(areaSquares, square, height, width);

            switch (side)
            {
                case Side.Top:
                    return arroundSqaures[3];
                case Side.Right:
                    return arroundSqaures[7];
                case Side.Bottom:
                    return arroundSqaures[5];
                case Side.Left:
                    return arroundSqaures[1];
                default:
                    return null;
            }
        }
    }
}
