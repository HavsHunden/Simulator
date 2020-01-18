using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Sim1
{
    class Entity
    {
        string species;
        Point position;

        World myWorld;

        public Entity(World world)
        {
            Console.WriteLine(species);

            position = world.GetFreePosition();

            Console.WriteLine(position);

            myWorld = world;

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

        public virtual void Move()
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

        public override void Move()
        {
            List<Entity> surroundingEntities = new List<Entity>();
            surroundingEntities = this.myWorld.GetSurroundingEntities(this.GetPosition());

            if (surroundingEntities.Count == 8)
            {

            }
            else
            {
                bool redo;
                Point chosenPoint;

                do
                {
                    redo = false;
                    int choiseY = myWorld.BorrowRand().Next(2) - 1;
                    int choiseX = myWorld.BorrowRand().Next(2) - 1;
                    chosenPoint = new Point(choiseX, choiseY);

                    for (int i = 0; i < surroundingEntities.Count; i++)
                    {
                        if (surroundingEntities[i].GetPosition() == chosenPoint)
                        {
                            redo = true;
                            break;
                        }
                    }
                } while (redo == true);

                this.SetPosition(chosenPoint);

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
