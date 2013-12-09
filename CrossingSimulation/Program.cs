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
        public struct Car
        {
            int inTime;
            int outTime;
            int position;
            public Car(int a, int b, int c)
            {
                inTime = a;
                outTime = b;
                position = c;
            }
        }
        static void Main(string[] args)
        {
            int[][] datums = int[3][];
            datums = readInput();
            /*
            while (true)
                simulateRound();
            writeOutput();
            */
        }

        private static void writeOutput()
        {
            throw new NotImplementedException();
        }

        private static void simulateRound()
        {
            throw new NotImplementedException();
        }

        private static int[][] readInput()
        {
            int[] init_data = new int[3];
            StreamReader reader = new StreamReader("auto.be");
            init_data = sequentialRead(reader, 3);

            int left_car_count = init_data[1];
            int[] left_cars = new int[left_car_count];
            left_cars = sequentialRead(reader, left_car_count);

            int upper_car_count = init_data[2];
            int[] upper_cars = new int[upper_car_count];
            upper_cars = sequentialRead(reader, upper_car_count);
            reader.Close();

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
