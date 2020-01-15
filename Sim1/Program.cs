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



                Entity entity = new Entity(possibleTypes[randType]);



                entities.Add(entity);
            }
            Console.ReadKey();
        }


        public Point test()
        {
            Point answer = new Point();

            return answer;
        }

        public static Point GetFreePosition()
        {
            List<Point> freePoints = new List<Point>();

            

            Point position = new Point(0, 0);
            return position;
        }
    }

    class Entity
    {
        string type;
        Point position;

        public Entity(string chosenType)
        {
            Console.WriteLine(chosenType);

            position = World.GetFreePosition();

        }
    }
}
