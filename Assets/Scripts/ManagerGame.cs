using System.Collections.Generic;

public class BuildingInfo
{
    public int Amount { get; set; }
    public float Def { get; set; }
    public float TimeOfHealing { get; set; }
}
public class ManagerGame
{
    public static float percentOfUnit { get; set; }
    public static readonly Dictionary<int, BuildingInfo> Buildings = new Dictionary<int, BuildingInfo>
    {
        {1, new BuildingInfo { Amount = 10, Def = 1f, TimeOfHealing = 1.25f }},
        {2, new BuildingInfo { Amount = 20, Def = 2f, TimeOfHealing = 1f }},
        {3, new BuildingInfo { Amount = 30, Def = 3f, TimeOfHealing = 0.85f }},
        {4, new BuildingInfo { Amount = 40, Def = 4f, TimeOfHealing = 0.65f }},
        {5, new BuildingInfo { Amount = 50, Def = 5f, TimeOfHealing = 0.5f }}
    };
}
