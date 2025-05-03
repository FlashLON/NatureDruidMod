using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

namespace NatureDruidMod.Upgrades
{
    public class ThornShot : ModUpgrade<Towers.NatureDruid>
    {
        public override int Path => 0;
        public override int Tier => 1;
        public override int Cost => 200;
        public override string Icon => "ThornShotIcon";
        public override string DisplayName => "Thorn Shot";
        public override string Description => "Thorns are more penetrating and more dangerous.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var weapon = towerModel.GetAttackModel().weapons[0];
            var projectile = weapon.projectile;

            projectile.pierce += 3;
            projectile.GetDamageModel().damage += 1;
        }
    }
}