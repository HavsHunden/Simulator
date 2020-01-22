using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            int n = 10;

            World world = new World(n, myGrid);
        }
    }

    class World
    {
        public List<Entity> entities;
        private List<string> possibleTypes = new List<string>();
        Random rand = new Random();
        Grid myGrid;
        public Label[][] gridLabels;

        public World(int n, Grid grid)
        {
            entities = new List<Entity>();
            myGrid = grid;

            ConnectGraphics();

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

            Start();

            //while (tick < 2)
            //{
            //    Tick();
            //    System.Threading.Thread.Sleep(1000);
            //    tick++;
            //}
        }

        public void Start()
        {
            int tick = 0;
            while (tick < 2)
            {
                for (int i = 0; i < entities.Count; i++)
                {
                    entities[i].Eat();
                    entities[i].Move();
                }
                tick++;
            }
        }

        public Label[][] GetLabels()
        {
            return gridLabels;
        }

        private void ConnectGraphics()
        {
            Label test;

            gridLabels = new Label[10][];

            //Initializing my jagged array gridLabels
            for (int i = 0; i < 10; i++)
            {
                gridLabels[i] = new Label[10];
            }

            //Adding all labels in grid to the jagged aarray
            for (int i = 0; i < myGrid.Children.Count; i++)
            {
                if (myGrid.Children[i] is Label)
                {
                    test = (Label)myGrid.Children[i];

                    gridLabels[Grid.GetRow(test)][Grid.GetColumn(test)] = test;
                }
            }

            Console.WriteLine("gridlables length: " + gridLabels.Length);
        }

        public Grid GetGrid()
        {
            return myGrid;
        }

        public Point GetFreePosition()
        {
            bool redo = true;
            //Random rand = new Random();

            Point position = new Point(0, 0);

            while (redo)
            {
                redo = false;

                position.X = rand.Next(10);
                position.Y = rand.Next(10);

                //Console.WriteLine("Test: " + position);

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

        public List<Point> GetSurroundingPositions(Point pointOfInterest)
        {
            List<Point> returnList = new List<Point>();

            Point testPoint = pointOfInterest;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    testPoint.Offset(i, j);
                    if(testPoint.X == 0 && testPoint.Y == 0)
                    {

                    }
                    else if (testPoint.X > 9 || testPoint.Y > 9)
                    {
                        returnList.Add(testPoint);
                    }

                }
            }

            return returnList;
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

    class Entity
    {
        protected string species;
        protected Point position;
        protected World myWorld;

        public Entity(World world)
        {
            myWorld = world;
            Console.WriteLine(species);
        }

        public virtual string GetSpecies()
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
            Point tempPoint = new Point(x, y);

            SetPosition(tempPoint);
        }

        public void SetPosition(Point point)
        {
            //Console.WriteLine(point);

            int x = (int)point.X;
            int y = (int)point.Y;

            myWorld.gridLabels[x][y].Content = species;

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
        public Wolf(World world) : base(world)
        {
            this.SetSpecies("wolf");
            Console.WriteLine("New " + species);
            this.SetPosition(myWorld.GetFreePosition());
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

        public override string GetSpecies()
        {
            return species;
        }

        //Move needs to only send these entities to valid spots (ie, not off game board)
        public override void Move()
        {
            List<Point> surroundingPoints;
            List<Entity> surroundingEntities;
            surroundingEntities = this.myWorld.GetSurroundingEntities(this.GetPosition());

            if (surroundingEntities.Count == 8)
            {

            }
            else
            {
                Point oldPosition = this.GetPosition();
                bool redo;
                Point chosenPoint;
                surroundingPoints = myWorld.GetSurroundingPositions(oldPosition);
                bool allowedMovement = false;


                do
                {
                    redo = false;
                    int choiseY = myWorld.BorrowRand().Next(2) - 1;
                    int choiseX = myWorld.BorrowRand().Next(2) - 1;
                    chosenPoint = new Point(this.GetPosition().X + choiseX, this.GetPosition().Y + choiseY);



                    for (int i = 0; i < surroundingPoints.Count; i++)
                    {
                        if (chosenPoint == surroundingPoints[i])
                        {
                            allowedMovement = true;
                        }
                    }

                    for (int i = 0; i < surroundingEntities.Count; i++)
                    {
                        if (surroundingEntities[i].GetPosition() == chosenPoint || allowedMovement == false)
                        {
                            redo = true;
                            break;
                        }
                    }
                } while (redo == true);



                this.SetPosition(chosenPoint);

                Console.WriteLine("Trying to move from" + oldPosition + "to" + this.GetPosition());

            }

        }


    }

    class Sheep : Entity
    {
        public Sheep(World world) : base(world)
        {
            this.SetSpecies("sheep");
            Console.WriteLine("New " + species);
            this.SetPosition(myWorld.GetFreePosition());
        }

        public override string GetSpecies()
        {
            return species;
        }
    }
}
