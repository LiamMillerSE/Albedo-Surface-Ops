﻿using Albedo_Surface_Ops.Commands;
using Albedo_Surface_Ops.TerrainTiles;
using Albedo_Surface_Ops.Units;
using System.Drawing;

#region Instantiation
//Game Bounds
const int GAME_WIDTH = 16;
const int GAME_HEIGHT = 16;

//Cursor
int selectionx = 0;
int selectiony = 0;
bool showCursor = true;

//Terrain
TerrainTile[,] terrian = new TerrainTile[GAME_WIDTH, GAME_HEIGHT];
Unit[,] units = new Unit[GAME_WIDTH, GAME_HEIGHT];

//Info and misc variable declaration
string infoString = "";
Random rand = new Random();
ViewMode viewMode = ViewMode.OVERVIEW;

//populate tiles
for (int y = 0; y < GAME_HEIGHT; y++)
{
    for (int x = 0; x < GAME_WIDTH; x++)
    {
        int t = rand.Next(20);
        if (t == 0)
        {
            terrian[x, y] = new Rubble();
        }
        else if(t <= 3)
        {
            terrian[x, y] = new Buildings();
        }
        else
        {
            terrian[x, y] = new Empty();
        }
    }
}
//Create a couple units
units[2, 2] = new LightInfantry(Faction.ILR);
units[4, 2] = new LightInfantry(Faction.ILR);

units[4, 14] = new LightInfantry(Faction.EDF);

#endregion

while (true)
{
    //Print out scene
    //first, the coords at the top (with a bar)
    Console.Write(" |");
    for(int i =  0; i < GAME_WIDTH; i++)
    {
        SetCheckerboardColor(i);
        Console.Write(i.ToString("D2"));
    }
    ResetConsoleColors();
    Console.WriteLine("|\n ├" + new string('─', GAME_WIDTH * 2) + "┤");
    for (int y = 0; y < GAME_HEIGHT; y++)
    {
        SetCheckerboardColor(y);
        Console.Write((char)('A' + y));
        ResetConsoleColors();
        Console.Write('|');
        for (int x = 0; x < GAME_WIDTH; x++)
        {
            if (showCursor && (x == selectionx || y == selectiony))
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
            }
            switch (viewMode)
            {
                case ViewMode.OVERVIEW:
                    if (units[x, y] != null)
                    {
                        units[x, y].WriteToConsole();
                    }
                    else
                    {
                        terrian[x, y].WriteToConsole();
                    }
                    break;
                case ViewMode.TERRAIN_WEIGHT:
                    terrian[x, y].WriteWeightToConsole();
                    break;
            }
            ResetConsoleColors();
        }
        Console.Write("|\n");
    }
    Console.WriteLine(" └" + new string('─', GAME_WIDTH * 2) + "┘");
    //Provide any information from last turn
    if(infoString.Length > 0)
    {
        Console.WriteLine(infoString);
        infoString = "";
    }
    //prompt for input
    Console.Write("What are your orders, Commander?\n> ");
    string[] orders = Console.ReadLine().ToLower().Split(" ");

    //TODO: break out parser
    if (orders[0] == "select")
    {
        //TODO: add protection
        selectionx = int.Parse(orders[1]);
        selectiony = orders[2][0] - 'a';
    }
    else if (orders[0] == "info")
    {
        infoString = "TERRAIN INFO: " + terrian[selectionx, selectiony].ToString() +
            "\nUNIT INFO: " + units[selectionx, selectiony]?.ToString();
    }
    else if (orders[0] == "weight")
    {
        viewMode = ViewMode.TERRAIN_WEIGHT;
    }
    else if (orders[0] == "overview")
    {
        viewMode = ViewMode.OVERVIEW;
    }
    else if (orders[0] == "move")
    {
        Unit u = units[selectionx, selectiony];
        int newX = int.Parse(orders[1]);
        int newY = orders[2][0] - 'a';
        //Make sure unit exists at selection and is friendly
        if (u != null && u.GetFaction() == Faction.EDF)
        {
            PathToNode(u, selectionx, selectiony, newX, newY);
        }
    }
    else if (orders[0] =="commands")
    {
        //longass message goes here :P
        #region commands_string
        infoString = "COMMANDS LIST:\n\n" +

                    "info\n\t" +
                    "Provides information about the currently selected tile (no args)\n\n" +

                    "move [x] [y]\n\t" +
                    "Moves the currently selected unit to the new coordinates specified\n\t" +
                    "Example: move 12 C\n\n" +

                    "overview\n\t" +
                    "Changes the view mode to default overview mode (no arguments)\n\n" +

                    "select [x] [y]\n\t" +
                    "Moves the cursor to the coordinate entered\n\t" +
                    "Example: select 14 N\n\n" +

                    "weight\n\t" +
                    "Changes the view mode to show terrain weight (no arguments)";

        #endregion 
    }
    //update every unit
    foreach(Unit u in units)
    {
        u?.Update();
    }
}

#region Pathfinding

bool PathToNode(Unit unit, int startx, int starty, int destX, int destY) 
{
    int curx = startx;
    int cury = starty;
    //bool[][] visited = Enumerable.Repeat(Enumerable.Repeat(false, GAME_HEIGHT).ToArray(), GAME_WIDTH).ToArray();
    List<Point> unvisited = new List<Point>();
    double[,] weights = new double[GAME_WIDTH, GAME_HEIGHT];
    for (int x = 0; x < GAME_WIDTH; x++)
    {
        for (int y = 0; y < GAME_HEIGHT; y++)
        {
            unvisited.Add(new Point(x, y));
            weights[x, y] = double.MaxValue;
        }
    }
    bool keepLooking = true;
        weights[curx, cury] = 0;
    do
    {
        unvisited.Remove(new Point(curx, cury));
        //calculate ornithagonal movement only
        if (curx + 1 < GAME_WIDTH)
        {
            ComparePathValues(ref weights, unvisited, curx, cury, curx + 1, cury);
        }
        if (curx - 1 >= 0)
        {
            ComparePathValues(ref weights, unvisited, curx, cury, curx - 1, cury);
        }
        if (cury + 1 < GAME_HEIGHT)
        {
            ComparePathValues(ref weights, unvisited, curx, cury, curx, cury + 1);
        }
        if (cury - 1 >= 0)
        {
            ComparePathValues(ref weights, unvisited, curx, cury, curx, cury - 1);
        }
        Point nextPoint = new Point(-1, -1);
        double lowestVal = double.MaxValue;
        //select next point
        foreach(Point p in unvisited)
        {
            if (weights[p.X, p.Y] < lowestVal)
            {
                nextPoint = p;
                lowestVal = weights[p.X, p.Y];
            }
        }
        curx = nextPoint.X;
        cury = nextPoint.Y;
        if(nextPoint == new Point(-1, -1))
        {
            keepLooking = false;
        }
    } while (keepLooking);

    //weight graph populated, now we track in reverse back to the start
    Stack<MoveCommand> steps = new Stack<MoveCommand>();
    curx = destX;
    cury = destY;
    while(!(curx == startx && cury == starty))
    {
        Point nextPoint = new Point();
        double minval = double.MaxValue;
        Direction dir = Direction.none;
        if (curx - 1 >= 0 && weights[curx - 1, cury] < minval)
        {
            minval = weights[curx - 1, cury];
            dir = Direction.EAST;
            nextPoint = new Point(curx - 1, cury);
        }
        if (curx + 1 < GAME_WIDTH && weights[curx + 1, cury] < minval)
        {
            minval = weights[curx + 1, cury];
            dir = Direction.WEST;
            nextPoint = new Point(curx + 1, cury);
        }
        if (cury - 1 >= 0 && weights[curx, cury - 1] < minval)
        {
            minval = weights[curx, cury - 1];
            dir = Direction.SOUTH;
            nextPoint = new Point(curx, cury - 1);
        }
        if (cury + 1 < GAME_HEIGHT && weights[curx, cury + 1] < minval)
        {
            minval = weights[curx, cury + 1];
            dir = Direction.NORTH;
            nextPoint = new Point(curx, cury + 1);
        }
        if (dir != Direction.none)
        {
            steps.Push(new MoveCommand(dir));
        }
        curx = nextPoint.X;
        cury = nextPoint.Y;
    }
    //queue all the steps to the unit
    while (steps.Count > 0)
    {
        unit.IssueCommand(steps.Pop());
    }
    //returns true if path was found succesfully and command was issued
    return true;
}

void ComparePathValues(ref double[,] weights, List<Point> unvisited, int startx,  int starty, int x, int y)
{
    /*if (unvisited.Contains(new Point(x, y)))*/
    {
        double newWeight = weights[startx, starty] + terrian[x, y].GetTravelWeight();
        if (weights[x, y] > newWeight)
        {
            weights[x, y] = newWeight;
        }
    }
}

#endregion

#region color formatting
void ResetConsoleColors()
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor = ConsoleColor.Black;
}
void SetCheckerboardColor(int n)
{
    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = n % 2 == 0 ? ConsoleColor.DarkCyan : ConsoleColor.Blue;
}

enum ViewMode
{
    OVERVIEW,
    TERRAIN_WEIGHT
}

#endregion