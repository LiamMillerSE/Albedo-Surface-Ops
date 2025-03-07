using Albedo_Surface_Ops.TerrainTiles;
using Albedo_Surface_Ops.Units;
using System.ComponentModel.Design;

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
            //make sure destination isn't populated
            if (units[newX, newY] == null) 
            {
                units[newX, newY] = u;
                units[selectionx, selectiony] = null;
            }
        }
    }
}

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
#endregion

enum ViewMode
{
    OVERVIEW,
    TERRAIN_WEIGHT
}