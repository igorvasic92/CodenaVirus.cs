using System;

namespace CodenaVirus
{
    class Program
    {
        static void Main(string[] args)
        {
            var world = new char[][]
            {
                new char[]{'#','#','#'},
                new char[]{'#','#','#'},
                new char[]{'#','#','#'}
            };

            var firstInfected = new int[2];
            firstInfected[0] = 0;
            firstInfected[1] = 2;


            var temp = new Program();
            var result = temp.Codenavirus(world, firstInfected);
            Console.WriteLine(result[0]);
            Console.WriteLine(result[1]);
            Console.WriteLine(result[2]);
            Console.WriteLine(result[3]);

        }

        public int[] Codenavirus(char[][] world, int[] firstInfected)
        {

            var humans = new human[world.Length][];
            var humansTemp = new human[world.Length][]; // Temp data to be used before each loop cicle

            int days = 0;
            int infected = 0;
            int recovered = 0;
            int uninfected = 0;
            int NumOfInfAtStart = 0; // number of infected people at start of each loop cicle
            int NumOfInfAtEnd = 1;   // number of infected people at end of each loop cicle

            for (int i = 0; i < world.Length; i++)
            {
                humans[i] = new human[world[i].Length];
                humansTemp[i] = new human[world[i].Length];
                for (int j = 0; j < world[i].Length; j++)
                {
                    if (world[i][j] == '#')
                    {
                        humans[i][j] = new human();
                        humansTemp[i][j] = new human();
                        //world[i][j] = humans[i][j].Status();                                                 
                    }
                }
            }


            //day 1- if human is placed at firstInfected point, he gets infected.
            if (humans[firstInfected[0]][firstInfected[1]] != null)
            {
                humans[firstInfected[0]][firstInfected[1]].IsInfected = true;
            }
            days++;


            //days 2+


           


            while (NumOfInfAtStart != NumOfInfAtEnd)
            {
                //Simulation for each day
                //Console.WriteLine($"day: {days}");
                //for (int i = 0; i < world.Length; i++)
                //{
                //    Console.WriteLine("");
                //    for (int j = 0; j < world[i].Length; j++)
                //    {
                //        if (humans[i][j] != null)
                //        {
                //            Console.Write(humans[i][j].Status());
                //        }
                //        else
                //        { Console.Write("."); }
                //    }
                //}
                //Console.WriteLine("");


                days++;
                NumOfInfAtStart = NumOfInfAtEnd;
                foreach (var H in humans)
                {
                    foreach (var h in H)
                    {
                        //checking if position is empty
                        if (h != null)
                        {
                            h.DidInfectInThisLoop = false;
                            if (h.IsInfected == true)
                            {
                                h.DayOfInfection++;
                            }

                            if (h.DayOfInfection > 2)
                            {
                                h.IsRecovered = true;
                            }
                        }
                    }
                }

                for (int i = 0; i < world.Length; i++)
                {

                    for (int j = 0; j < world[i].Length; j++)
                    {
                        if (humans[i][j] != null)
                        {

                            if (humans[i][j].IsInfected == true) { humansTemp[i][j].IsInfected = true; }
                            else { humansTemp[i][j].IsInfected = false; };

                            if (humans[i][j].IsRecovered == true) { humansTemp[i][j].IsRecovered = true; }
                            else { humansTemp[i][j].IsRecovered = false; };
                        }
                    }
                }


                for (int i = 0; i < world.Length; i++)
                {
                    
                    for (int j = 0; j < world[i].Length; j++)
                    {
                        
                        //checking if position is empty
                        if (humansTemp[i][j] != null)
                        {
                            

                            //checking if human is infected and not recovered
                            if (humansTemp[i][j].IsInfected == true & humansTemp[i][j].IsRecovered == false)
                            {
                                //checking if human to the right is infected
                                if (j < (humansTemp[i].Length - 1))
                                {
                                    if (humansTemp[i][j + 1] != null)
                                    {
                                        if (humans[i][j + 1].IsInfected == false & humans[i][j].DidInfectInThisLoop == false) { humans[i][j + 1].IsInfected = true; humans[i][j].DidInfectInThisLoop = true; NumOfInfAtEnd++;  }
                                    }
                                }

                                //checking if human above is infected
                                if ((i - 1) >= 0)
                                {
                                    if (humansTemp[i - 1][j] != null)
                                    {
                                        if (humans[i - 1][j].IsInfected == false & humans[i][j].DidInfectInThisLoop == false) { humans[i - 1][j].IsInfected = true; humans[i][j].DidInfectInThisLoop = true; NumOfInfAtEnd++;  }
                                    }
                                }

                                //checking if human to the left is infected
                                if ((j - 1) >= 0)
                                {
                                    if (humansTemp[i][j - 1] != null)
                                    {
                                        if (humans[i][j - 1].IsInfected == false & humans[i][j].DidInfectInThisLoop == false) { humans[i][j - 1].IsInfected = true; humans[i][j].DidInfectInThisLoop = true; NumOfInfAtEnd++;  }
                                    }
                                }

                                //checking if human below is infected
                                if (i < (humansTemp.Length - 1))
                                {
                                    if (humansTemp[i + 1][j] != null)
                                    {
                                        if (humans[i + 1][j].IsInfected == false & humans[i][j].DidInfectInThisLoop == false) { humans[i + 1][j].IsInfected = true; humans[i][j].DidInfectInThisLoop = true; NumOfInfAtEnd++;  }
                                    }
                                }
                            }
                        }

                        
                    }

                }
                
            }

            foreach (var H in humans)
            {
                foreach (var h in H)
                {
                    //checking if position is empty
                    if (h != null)
                    {
                        if (h.IsInfected == true & h.IsRecovered == false)
                        {
                            infected++;
                        }
                        if (h.IsInfected == true & h.IsRecovered == true)
                        {
                            recovered++;
                        }
                        if (h.IsInfected == false & h.IsRecovered == false)
                        {
                            uninfected++;
                        }
                    }
                }
            }

            var result = new int[]
            {
                days,
                infected,
                recovered,
                uninfected
            };

            return result;

        }



    }
    internal class human
    {
        public bool IsInfected { get; set; }
        public bool IsRecovered { get; set; }
        public int DayOfInfection { get; set; }
        public bool DidInfectInThisLoop { get; set; }
        public char Status()
        {

            if (IsInfected == true && IsRecovered == false)
            {
                return 'I';
            }
            else
            {
                if (IsInfected == true && IsRecovered == true)
                {
                    return 'R';
 
                }
            }

            return 'H';
        }

    }
}
