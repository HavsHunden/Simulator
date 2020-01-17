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
        Random rand = new Random();

        public World(int n, int sizeX, int sizeY)
        {
            entities = new List<Entity>();

            possibleTypes.Add("wolf");
            possibleTypes.Add("sheep");
            //Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                int randType = rand.Next(possibleTypes.Count);

                if (randType == 0)
                {
                    Wolf entity = new Wolf(this);
                    entities.Add(entity);
                }
                if (randType == 1)
                {
                    Sheep entity = new Sheep(this);
                    entities.Add(entity);
                    entity.GetSpecies();
                }
            }


            Console.ReadKey();
        }


        public Point test()
        {
            Point answer = new Point();

            return answer;
        }

        public Point GetFreePosition()
        {
            bool redo = true;
            //Random rand = new Random();

            Point position = new Point(0, 0);

            while (redo)
            {
                redo = false;

                position.X = rand.Next(100);
                position.Y = rand.Next(100);

                //Console.WriteLine("Test");

                for (int i = 0; i < this.entities.Count; i++)
                {
                    if (this.entities[i].GetPosition() == position)
                    {
                        Console.WriteLine("Regenerating position");
                        redo = true;
                        break;
                    }
                }
            }

            return position;
        }

        public List<Entity> GetEntityAtPosition(Point pointOfInterest)
        {

            List<Entity> returnList;
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].GetPosition() == pointOfInterest)
                {
                    returnList.Add(entities[i]);
                }
            }

            return e;
        }
    }

    class Entity
    {
        string species;
        Point position
        //{
        //    get; set;
        //}
        ;

        public Entity(World world)
        {
            Console.WriteLine(species);

            position = world.GetFreePosition();

            Console.WriteLine(position);

        }

        public string GetSpecies()
        {
            return species;
        }

        public void SetSpecies(string input)
        {
            species = input;
        }

        public Point GetPosition()
        {
            return position;
        }

        public void SetPosition(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }

        public void SetPosition(Point point)
        {
            position = point;
        }
    }

    class Wolf : Entity
    {
        string species = "wolf";

        public Wolf(World world) : base(world)
        {
            this.SetPosition(world.GetFreePosition());
            this.SetSpecies(species);
            Console.WriteLine("New " + species);
        }
    }

    class Sheep : Entity
    {
        string species = "sheep";

        public Sheep(World world) : base(world)
        {
            this.SetPosition(world.GetFreePosition());
            this.SetSpecies(species);
            Console.WriteLine("New " + species);
        }
    }
}
