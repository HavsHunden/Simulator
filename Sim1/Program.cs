﻿using System;
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


            Console.ReadKey();
        }


        public Point test()
        {
            Point answer = new Point();

            return answer;
        }

        public void Tick()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].Eat();
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

        //Finish this

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

        public virtual void Eat()
        {

        }

        public void Move()
        {
            
        }
    }

    class Wolf : Entity
    {
        string species = "wolf";

        World myWorld;

        public Wolf(World world) : base(world)
        {
            myWorld = world;
            this.SetPosition(world.GetFreePosition());
            this.SetSpecies(species);
            Console.WriteLine("New " + species);
        }

        public override void Eat()
        {
            //Console.WriteLine("eatmethod");

            List<Entity> nearbyEntities = new List<Entity>();

            nearbyEntities = myWorld.GetSurroundingEntities(this.GetPosition());


            //If there are no nearby entities, do nothing, otherwise...
            if (nearbyEntities.Count == 0)
            {

            }
            else
            {
                //Look at all nearby entities - if they are sheep, remove them from the world's list
                for (int i = 0; i < nearbyEntities.Count; i++)
                {
                    if (nearbyEntities[i].GetSpecies() == "sheep")
                    {
                        myWorld.entities.Remove(nearbyEntities[i]);

                        Console.WriteLine("A sheep was eaten at " + this.GetPosition() + "!!!");
                    }
                }
            }
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
