using Albedo_Surface_Ops;
using Albedo_Surface_Ops.Units;

const int GAME_WIDTH = 16;
const int GAME_HEIGHT = 16;

TerrainTile[,] terrian = new TerrainTile[GAME_WIDTH, GAME_HEIGHT];
Unit[,] units = new Unit[GAME_WIDTH, GAME_HEIGHT];

//populate tiles
for (int y = 0; y < GAME_HEIGHT; y++)
{
    for (int x = 0; x < GAME_WIDTH; x++)
    {
        terrian[x, y] = new TerrainTile("..");
    }
}
//Create a couple units
units[2, 2] = new LightInfantry(Faction.ILR);
units[4, 2] = new LightInfantry(Faction.ILR);

units[4, 14] = new LightInfantry(Faction.EDF);

while (true)
{
    //Print out scene
    //first, the coords at the top (with a bar)
    Console.Write(" |");
    Console.ForegroundColor = ConsoleColor.Black;
    for(int i =  0; i < GAME_WIDTH; i++)
    {
        Console.BackgroundColor = i % 2 == 0 ? ConsoleColor.DarkCyan : ConsoleColor.Blue;
        Console.Write(i.ToString("D2"));
    }
    Console.ForegroundColor = ConsoleColor.White;
    Console.BackgroundColor= ConsoleColor.Black;
    Console.WriteLine("|\n ├" + new string('─', GAME_WIDTH * 2) + "┤");
    for (int y = 0; y < GAME_HEIGHT; y++)
    {
        Console.ForegroundColor = ConsoleColor.Black;
        Console.BackgroundColor = y % 2 == 0 ? ConsoleColor.DarkCyan : ConsoleColor.Blue;
        Console.Write((char)('A' + y));
        Console.ForegroundColor = ConsoleColor.White;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.Write('|');
        for (int x = 0; x < GAME_WIDTH; x++)
        {
            if (units[x, y] != null)
            {
                units[x, y].WriteToConsole();
            }
            else
            {
                terrian[x, y].WriteToConsole();
            }
        }
        Console.Write("|\n");
    }
    Console.WriteLine(" └" + new string('─', GAME_WIDTH * 2) + "┘");
    //prompt for input
    Console.Write("What are your orders, Commander?\n> ");
    Console.ReadLine();
}