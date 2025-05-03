using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using NatureDruidMod.Towers;
using BTD_Mod_Helper.Extensions;


public class TidalVolley : ModUpgrade<NatureDruid>
{
    public override int Path => 2;
    public override int Tier => 3;
    public override int Cost => 8500;
    public override string DisplayName => "Tidal Volley";
    public override string Description => "Occasionally launches a water burst that slows Bloons.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var slow = new SlowModel("WaterSlow", 0.7f, 2f, "Water", 1, "", true, false, null, false, false, false, 1f, 0, false);
        foreach (var weapon in towerModel.GetWeapons())
        {
            var waterProjectile = weapon.projectile.Duplicate();
            waterProjectile.pierce = 3;
            waterProjectile.AddBehavior(slow);

            weapon.projectile.AddBehavior(new CreateProjectileOnContactModel(
                "CreateWaterSplash", waterProjectile,
                new SingleEmissionModel("EmitOneWater", null),
                true, false, false));
        }
    }
}
