using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers;
using NatureDruidMod.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

public class ThornVolley : ModUpgrade<NatureDruid>
{
    public override int Path => 2;
    public override int Tier => 2;
    public override int Cost => 1000;
    public override string DisplayName => "ThornVolley";
    public override string Description => "Shoots 5 thorns instead of 3 in each attack.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        foreach (var weapon in towerModel.GetWeapons())
        {
            var emission = weapon.emission;

            if (emission is ArcEmissionModel arcEmission)
            {
                arcEmission.count = 5;
            }
            else if (emission is EmissionModel)
            {
                weapon.emission = new ArcEmissionModel("ThornVolleyArc", 8, 0f, 360f, new Il2CppReferenceArray<EmissionBehaviorModel>(0), false, false)
;
            }
        }
    }
}
