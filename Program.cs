using Albedo_Surface_Ops;


//Game Loop
while (true)
{
    GameMaster.Instance().PrintScene();
    GameMaster.Instance().PlayerTurn();
    GameMaster.Instance().Update();
}