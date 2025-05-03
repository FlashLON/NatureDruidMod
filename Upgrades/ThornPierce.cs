using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using NatureDruidMod.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Weapons;

public class ThornPierce : ModUpgrade<NatureDruid>
{
    public override int Path => 2;
    public override int Tier => 1;
    public override int Cost => 500;
    public override string DisplayName => "ThornPierce";
    public override string Description => "Can now pop Camo Bloons.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        towerModel.GetAttackModel().weapons[0].projectile.SetHitCamo(true);
    }
}
