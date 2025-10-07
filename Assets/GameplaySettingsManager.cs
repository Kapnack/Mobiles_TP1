using Systems;

public class GameplaySettingsManager : Singleton<GameplaySettingsManager>
{
    public bool isFirstLoad = true;
    public int DificultadOpcionActual = 0;
    public bool IsMultiplayer;
}