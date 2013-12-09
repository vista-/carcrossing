

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CrossingSimulation
{
    class Program
    {
        const int first_line_len = 3;
        public struct Car
        {
            int inTime;
            int outTime;
            int position;
            public Car(int a, int len)
            {
                inTime = a;
                outTime = 0;
                position = (-1 * len) - 1;
            }
        }
        static void Main(string[] args)
        {
            //read the variables
            int[][] datums = new int[3][];
            datums = readInput();
            int length = (datums[0][0] * 2) + 1;
            int[] left_road = new int[length];
            int[] upper_road = new int[length];
            int left_car_count = datums[0][1];
            int upper_car_count = datums[0][2];
            //car array constructor
            Car[] left_cars = new Car[left_car_count];
            for (int i = 0; i < (left_car_count - 1) ; i++)
            {
                left_cars[i] = new Car(datums[1][i], length);
            }
            Car[] upper_cars = new Car[upper_car_count];
            for (int i = 0; i < (upper_car_count - 1); i++)
            {
               upper_cars[i] = new Car(datums[2][i], length);
            }
            
        }

        private static void writeOutput()
        {
            throw new NotImplementedException();
        }

        private static int[][] readInput()
        {
            //read the data in from file "auto.be"
            int[] init_data = new int[first_line_len];
            StreamReader reader = new StreamReader("auto.be");
            init_data = sequentialRead(reader, first_line_len - 1);

            int left_car_count = init_data[1];
            int[] left_cars = new int[left_car_count];
            left_cars = sequentialRead(reader, left_car_count - 1);

            int upper_car_count = init_data[2];
            int[] upper_cars = new int[upper_car_count];
            upper_cars = sequentialRead(reader, upper_car_count - 1);

            reader.Close();
            //prep for export
            int[][] output = new int[3][];
            output[0] = init_data;
            output[1] = left_cars;
            output[2] = upper_cars;

            return output;
        }

        private static int[] sequentialRead(StreamReader reader, int len)
        {
            int[] data = new int[len];
            for (int i = 0; i < len; i++)
            {
                data[i] = Convert.ToInt16(reader.Read());
                if (i == len) break;
                reader.Read();
            }
            return data;
        }
    }
}
