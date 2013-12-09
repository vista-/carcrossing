

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
            public int inTime;
            public int outTime;
            public int position;
            public Car(int a, int len)
            {
                inTime = a;
                outTime = 0;
                position = (len / -2) - 2 ;
            }
        }
        static void Main(string[] args)
        {
            //read the variables
            int[][] data = new int[3][];
            data = readInput();
            int length = (data[0][0] * 2) + 1;
            int[] left_road = new int[length];
            int[] upper_road = new int[length];
            int left_car_count = data[0][1];
            int upper_car_count = data[0][2];
            Console.WriteLine("Data read, beginning object creation...");
            //car array constructor
            Car[] left_cars = new Car[left_car_count];
            for (int i = 0; i < (left_car_count - 1) ; i++)
            {
                left_cars[i] = new Car(data[1][i], length);
            }
            Car[] upper_cars = new Car[upper_car_count];
            for (int i = 0; i < (upper_car_count - 1); i++)
            {
               upper_cars[i] = new Car(data[2][i], length);
            }
            Console.WriteLine("Total {0} cars generated, {1} coming from left, {2} coming from above.", left_car_count + upper_car_count, left_car_count, upper_car_count);
            //now to simulate this entire thing
            int time = 0;
            while ((left_cars[left_car_count - 1].outTime == 0) || (upper_cars[upper_car_count - 1].outTime == 0))
            {
                time++;
                for (int i = 0; i < left_car_count; i++)
                {
                    if (i == 0)
                    {
                        if (left_cars[0].outTime >= time)
                        {
                            if (!(left_cars[0].position == -1 || left_cars[1].position == -2) || passesCrossing(upper_cars, true))
                            {
                                left_cars[0].position += 1;
                                if (left_cars[0].position == length + 1)
                                {
                                    left_cars[0].outTime = time;
                                    Console.WriteLine("Left car no.{0} has left at {1}", i + 1, time);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (left_cars[i].outTime >= time && i != 1)
                        {
                            int distance = Math.Abs(left_cars[i].position - left_cars[i - 1].position);
                            if (distance >= 2 || distance == 0 || left_cars[i - 1].position == length + 1)
                            {
                                if (!(left_cars[i].position == -1 || left_cars[i].position == -2) || passesCrossing(upper_cars, true))
                                {
                                    left_cars[i].position += 1;
                                    if (left_cars[i].position == length + 1)
                                    {
                                        left_cars[i].outTime = time;
                                        Console.WriteLine("Left car no.{0} has left at {1}", i + 1, time);
                                    }
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < upper_car_count; i++)
                {
                    if (i == 0)
                    {
                        if (upper_cars[0].outTime >= time)
                        {
                            if (!(upper_cars[0].position == -1 || upper_cars[0].position == -2) || passesCrossing(left_cars, false))
                            {
                                upper_cars[0].position += 1;
                                if (upper_cars[0].position == length + 1)
                                {
                                    upper_cars[0].outTime = time;
                                    Console.WriteLine("Upper car no.{0} has left at {1}", i + 1, time);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (upper_cars[i].outTime >= time && i != 1)
                        {
                            int distance = Math.Abs(upper_cars[i].position - upper_cars[i - 1].position);
                            if (distance >= 2 || distance == 0 || upper_cars[i - 1].position == length + 1)
                            {
                                if (!(upper_cars[i].position == -1 || upper_cars[i].position == -2) || passesCrossing(left_cars, false))
                                {
                                    upper_cars[i].position += 1;
                                    if (upper_cars[i].position == length + 1)
                                    {
                                        upper_cars[i].outTime = time;
                                        Console.WriteLine("Upper car no.{0} has left at {1}", i + 1, time);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //writing the output
            writeOutput(left_cars, upper_cars);

            Console.ReadLine();
        }

        private static bool passesCrossing(Car[] cars, bool priority)
        {
            bool passes = true;
                for (int i = 0; i < cars.Length + 1; i++)
                {
                    if(cars[i].position == -1 || (cars[i].position == -2 && !priority))
                    {
                        passes = false;
                    }
                }
           return passes;
        }

        private static void writeOutput(Car[] data1, Car[] data2)
        {
            //write data to file "auto.ki"
            StreamWriter writer = new StreamWriter("auto.ki");
            sequentialWrite(writer, data1);
            writer.WriteLine();
            sequentialWrite(writer, data2);

            writer.Close();
        }

        private static void sequentialWrite(StreamWriter writer, Car[] data)
        {
            for (int i = 0; i < data.Length + 1; i++)
            {
                writer.Write(Convert.ToString(data[i].outTime));
                if(i == data.Length)
                {
                    Console.Write("Written {0} out times.", i);
                    break;
                }
                writer.Write(" ");
            }
        }

        private static int[][] readInput()
        {
            //read the data in from file "auto.be"
            int[] init_data = new int[first_line_len];
            StreamReader reader = new StreamReader("auto.be");
            init_data = sequentialRead(reader, first_line_len);

            int left_car_count = init_data[1];
            int[] left_cars = new int[left_car_count];
            left_cars = sequentialRead(reader, left_car_count);

            int upper_car_count = init_data[2];
            int[] upper_cars = new int[upper_car_count];
            upper_cars = sequentialRead(reader, upper_car_count);

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
                if (i == (len - 1)) 
                {
                    Console.WriteLine("{0} datum read", i);
                    break;
                }
                reader.Read();
            }
            return data;
        }

    }
}
