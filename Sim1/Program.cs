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

            while (true)
            {
                Tick();
                Console.ReadKey();
            }
        }

        public void Tick()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Eat();
                entities[i].Move();
            }
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

        public List<Entity> GetSurroundingEntities(Point pointOfInterest)
        {
            List<Entity> returnList = new List<Entity>();

            List<Point> allPointsOfInterest = new List<Point>();

            Point p = new Point();

            p = pointOfInterest;
            p.Offset(1, 0);
            allPointsOfInterest.Add(p);
            p = pointOfInterest;
            p.Offset(1, 1);
            allPointsOfInterest.Add(p);
            p = pointOfInterest;
            p.Offset(0, 1);
            allPointsOfInterest.Add(p);
            p = pointOfInterest;
            p.Offset(-1, 1);
            allPointsOfInterest.Add(p);
            p = pointOfInterest;
            p.Offset(-1, 0);
            allPointsOfInterest.Add(p);
            p = pointOfInterest;
            p.Offset(-1, -1);
            allPointsOfInterest.Add(p);
            p = pointOfInterest;
            p.Offset(0, -1);
            allPointsOfInterest.Add(p);
            p.Offset(1, -1);
            allPointsOfInterest.Add(p);


            for (int i = 0; i < entities.Count; i++)
            {
                for (int j = 0; j < allPointsOfInterest.Count; j++)
                {
                    if (entities[i].GetPosition() == allPointsOfInterest[j])
                    {
                        returnList.Add(entities[i]);
                    }
                }
            }

            return returnList;
        }

        public Entity GetEntityAtPosition(Point pointOfInterest)
        {

            Entity returnEntity = null;
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].GetPosition() == pointOfInterest)
                {
                    returnEntity = entities[i];
                }
            }

            return returnEntity;
        }

        public Random BorrowRand()
        {
            return rand;
        }
    }

    
}
