using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUP_projekt
{
    internal class GenetskiAlgoritam
    {
        public class TSP
        {
            public List<Grad> Gradovi { get; }

            public TSP(List<Grad> gradovi)
            {
                Gradovi = gradovi;
            }
        }
        public class Chromosome : IComparable<Chromosome>
        {
            public List<Grad> Route { get; }
            public double Fitness { get; private set; }

            public Chromosome(List<Grad> route)
            {
                Route = route;
                CalculateFitness();
            }

            public void CalculateFitness()
            {
                double distance = 0;
                for (int i = 0; i < Route.Count - 1; i++)
                {
                    distance += Route[i].DistanceTo(Route[i + 1]);
                }
                distance += Route[Route.Count - 1].DistanceTo(Route[0]);
                Fitness = 1 / distance;
            }

            public void Mutate(double mutationRate)
            {
                for (int i = 0; i < Route.Count; i++)
                {
                    if (new Random().NextDouble() < mutationRate)
                    {
                        int index = new Random().Next(Route.Count);
                        Grad temp = Route[i];
                        Route[i] = Route[index];
                        Route[index] = temp;
                    }
                }
                CalculateFitness();
            }

            public int CompareTo(Chromosome other)
            {
                return other.Fitness.CompareTo(Fitness);
            }
        }
        public class GA
        {
            private List<Chromosome> _population;
            private double _mutationRate;
            private int _populationSize;
            private TSP _problem;

            public GA(TSP problem, int populationSize, double mutationRate)
            {
                _problem = problem;
                _populationSize = populationSize;
                _mutationRate = mutationRate;
                InitializePopulation();
            }

            private void InitializePopulation()
            {
                _population = new List<Chromosome>(_populationSize);
                for (int i = 0; i < _populationSize; i++)
                {
                    List<Grad> randomRoute = new List<Grad>(_problem.Gradovi);
                    randomRoute = randomRoute.OrderBy(x => new Random().Next()).ToList();
                    _population.Add(new Chromosome(randomRoute));
                }
            }

            public Chromosome Run(int generations)
            {
                for (int i = 0; i < generations; i++)
                {
                    List<Chromosome> newPopulation = new List<Chromosome>();
                    _population.Sort();

                    
                    int eliteSize = (int)(_populationSize * 0.1);
                    for (int j = 0; j < eliteSize; j++)
                    {
                        newPopulation.Add(_population[j]);
                    }

                    
                    for (int j = eliteSize; j < _populationSize; j++)
                    {
                        Chromosome parent1 = SelectParent();
                        Chromosome parent2 = SelectParent();

                        List<Grad> childRoute = Crossover(parent1, parent2);
                        Chromosome child = new Chromosome(childRoute);
                        child.Mutate(_mutationRate);
                        newPopulation.Add(child);
                    }

                    _population = newPopulation;
                }

                _population.Sort();
                return _population[0]; 
            }
            private Chromosome SelectParent()
            {
                double totalFitness = _population.Sum(ch => ch.Fitness);
                double target = new Random().NextDouble() * totalFitness;
                double currentSum = 0;

                foreach (Chromosome ch in _population)
                {
                    currentSum += ch.Fitness;
                    if (currentSum >= target)
                    {
                        return ch;
                    }
                }

                return _population[_population.Count - 1];
            }

            private List<Grad> Crossover(Chromosome parent1, Chromosome parent2)
            {
                int start = new Random().Next(parent1.Route.Count);
                int end = new Random().Next(start, parent1.Route.Count);

                List<Grad> childRoute = parent1.Route.Skip(start).Take(end - start).ToList();
                List<Grad> remainingCities = parent2.Route.Except(childRoute).ToList();

                return childRoute.Concat(remainingCities).ToList();
            }
        }
    }
}

