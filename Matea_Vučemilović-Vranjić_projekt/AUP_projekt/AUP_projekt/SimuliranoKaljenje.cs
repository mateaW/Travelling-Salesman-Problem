using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUP_projekt
{
    internal class SimuliranoKaljenje
    {
        private List<Grad> gradovi;
        private double pocetnaTemperatura;
        private double stopaHladenja;
        private Random random;

        public SimuliranoKaljenje(List<Grad> gradovi, double pocetnaTemperatura, double stopaHladenja)
        {
            this.gradovi = gradovi;
            this.pocetnaTemperatura = pocetnaTemperatura;
            this.stopaHladenja = stopaHladenja;
            random = new Random();
        }
        public List<Grad> PronadiNajkraciPut()
        {
            List<Grad> currentPath = GenerateRandomPath();
            double currentDistance = CalculateDistance(currentPath);
            double temperatura = pocetnaTemperatura;

            while (temperatura > 1)
            {
                List<Grad> newPath = GenerateNeighborPath(currentPath);
                double newDistance = CalculateDistance(newPath);
                double acceptanceProbability = AcceptanceProbability(currentDistance, newDistance, temperatura);

                if (acceptanceProbability > random.NextDouble())
                {
                    currentPath = newPath;
                    currentDistance = newDistance;
                }

                temperatura *= stopaHladenja;
            }

            return currentPath;
        }
        private List<Grad> GenerateRandomPath()
        {
            List<Grad> path = new List<Grad>(gradovi);
            Shuffle(path);
            return path;
        }

        private List<Grad> GenerateNeighborPath(List<Grad> path)
        {
            List<Grad> newPath = new List<Grad>(path);
            int index1 = random.Next(newPath.Count);
            int index2 = random.Next(newPath.Count);

            Grad city1 = newPath[index1];
            newPath[index1] = newPath[index2];
            newPath[index2] = city1;

            return newPath;
        }
        private double CalculateDistance(List<Grad> path)
        {
            double distance = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                distance += path[i].DistanceTo(path[i + 1]);
            }
            distance += path[path.Count - 1].DistanceTo(path[0]);
            return distance;
        }
        public double CalculateTotalDistance(List<Grad> path)
        {
            double totalDistance = 0;
            for (int i = 0; i < path.Count - 1; i++)
            {
                totalDistance += path[i].DistanceTo(path[i + 1]);
            }
            totalDistance += path[path.Count - 1].DistanceTo(path[0]);
            return totalDistance;
        }
        private double AcceptanceProbability(double currentDistance, double newDistance, double temperature)
        {
            if (newDistance < currentDistance)
                return 1.0;
            return Math.Exp((currentDistance - newDistance) / temperature);
        }

        private void Shuffle<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        
    }
}
