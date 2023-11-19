using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AUP_projekt.GenetskiAlgoritam;

namespace AUP_projekt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            List<Grad> cities = new List<Grad>
            {
                new Grad(1, 0, 0),
                new Grad(2, 1, 3),
                new Grad(3, 2, 1),
                new Grad(4, 6, 6),
                new Grad(5, 4, 4)
            };

            stopwatch.Start();
            TSP problem = new TSP(cities);
            GenetskiAlgoritam.GA ga = new GenetskiAlgoritam.GA(problem, 100, 0.01);
            Chromosome bestSolution = ga.Run(1000);
            stopwatch.Stop();

            TimeSpan elapsedTimeGA = stopwatch.Elapsed;
            Console.WriteLine("Vrijeme izvršavanja genetskog algoritma: " + elapsedTimeGA.TotalMilliseconds + " ms");
            Console.WriteLine("Najkraći put pronađen Genetskim algoritmom: ");
            foreach (Grad city in bestSolution.Route)
            {
                Console.WriteLine($"Grad {city.ID} (X: {city.X}, Y: {city.Y})");
            }
            Console.WriteLine($"Grad {bestSolution.Route[0].ID} (X: {bestSolution.Route[0].X}, Y: {bestSolution.Route[0].Y})");
            Console.WriteLine($"Ukupna udaljenost: {1 / bestSolution.Fitness}");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            stopwatch.Reset();
            stopwatch.Start();
            double initialTemperature = 100;
            double coolingRate = 0.99;
            SimuliranoKaljenje saAlgorithm = new SimuliranoKaljenje(cities, initialTemperature, coolingRate);
            List<Grad> shortestPath = saAlgorithm.PronadiNajkraciPut();
            double totalDistance = saAlgorithm.CalculateTotalDistance(shortestPath);
            stopwatch.Stop();
            TimeSpan elapsedTimeSA = stopwatch.Elapsed;
            Console.WriteLine("Vrijeme izvršavanja algoritma simuliranog kaljenja: " + elapsedTimeSA.TotalMilliseconds + " ms");
            Console.WriteLine("Najkraći put pronađen algoritmom Simuliranog kaljenja:");
            int X = 1;
            string prvi = "";
            foreach (Grad city in shortestPath)
            {
                if (X == 1)
                {
                    prvi = $"Grad {city.ID} (X: {city.X}, Y: {city.Y})";
                }
                X--;
                Console.WriteLine($"Grad {city.ID} (X: {city.X}, Y: {city.Y})");
            }
            Console.WriteLine(prvi);
            Console.WriteLine($"Ukupna udaljenost: {totalDistance}");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            List<Grad> cities2 = new List<Grad>
            {
                new Grad(1, 0, 14),
                new Grad(2, 14, 363),
                new Grad(3, 233, 11),
                new Grad(4, 66, 66),
                new Grad(5, 4, 4),
                new Grad(6, 999, 444),
            };


            stopwatch.Reset();
            stopwatch.Start();
            TSP problem2 = new TSP(cities2);
            GenetskiAlgoritam.GA ga2 = new GenetskiAlgoritam.GA(problem2, 100, 0.01);
            Chromosome bestSolution2 = ga2.Run(1000);
            stopwatch.Stop();
            TimeSpan elapsedTimeGA2 = stopwatch.Elapsed;
            Console.WriteLine("Vrijeme izvršavanja genetskog algoritma: " + elapsedTimeGA2.TotalMilliseconds + " ms");
            Console.WriteLine("Najkraći put pronađen Genetskim algoritmom: ");
            foreach (Grad city in bestSolution2.Route)
            {
                Console.WriteLine($"Grad {city.ID} (X: {city.X}, Y: {city.Y})");
            }
            Console.WriteLine($"Grad {bestSolution2.Route[0].ID} (X: {bestSolution2.Route[0].X}, Y: {bestSolution2.Route[0].Y})");
            Console.WriteLine($"Ukupna udaljenost: {1 / bestSolution2.Fitness}");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            stopwatch.Reset();
            stopwatch.Start();
            double initialTemperature2 = 100;
            double coolingRate2 = 0.99;
            SimuliranoKaljenje saAlgorithm2 = new SimuliranoKaljenje(cities2, initialTemperature2, coolingRate2);
            List<Grad> shortestPath2 = saAlgorithm2.PronadiNajkraciPut();
            double totalDistance2 = saAlgorithm2.CalculateTotalDistance(shortestPath2);
            stopwatch.Stop();
            TimeSpan elapsedTimeSA2 = stopwatch.Elapsed;
            Console.WriteLine("Vrijeme izvršavanja algoritma simuliranog kaljenja: " + elapsedTimeSA.TotalMilliseconds + " ms");
            Console.WriteLine("Najkraći put pronađen algoritmom Simuliranog kaljenja:");
            int X2 = 1;
            string prvi2 = "";
            foreach (Grad city in shortestPath2)
            {
                if (X2 == 1)
                {
                    prvi2 = $"Grad {city.ID} (X: {city.X}, Y: {city.Y})";
                }
                X2--;
                Console.WriteLine($"Grad {city.ID} (X: {city.X}, Y: {city.Y})");
            }
            Console.WriteLine(prvi2);
            Console.WriteLine($"Ukupna udaljenost: {totalDistance2}");
        }
    }
}
