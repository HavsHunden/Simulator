using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sim1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("How many entities?");
            int n = Convert.ToInt32(Console.ReadLine());



            World world = new World(n, 100, 100);

        }
    }

    class World
    {
        public List<Entity> entities;
        private List<string> possibleTypes = new List<string>();

        public World(int n, int sizeX, int sizeY)
        {
            entities = new List<Entity>();

            possibleTypes.Add("wolf");
            possibleTypes.Add("sheep");
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                int randType = rand.Next(possibleTypes.Count);

                Entity entity = new Entity(possibleTypes[randType], this);

                entities.Add(entity);
            }
            Console.ReadKey();
        }


        public Point test()
        {
            Point answer = new Point();

            return answer;
        }

        //Finish this
        public Point GetFreePosition()
        {
            List<Point> freePoints = new List<Point>();

            for (int i = 0; i < this.entities.Count ; i++)
            {

            }

            Point position = new Point(0, 0);
            return position;
        }
    }

    class Entity
    {
        string species;
        Point position;

        public Entity(string chosenSpecies, World world)
        {
            species = chosenSpecies;
            Console.WriteLine(species);

            position = world.GetFreePosition();

            Console.WriteLine(position);

        }

        public string GetSpecies()
        {
            return species;
        }

        public Point GetPosition()
        {
            return position;
        }
    }
}
