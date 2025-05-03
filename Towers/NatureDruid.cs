using MelonLoader;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.TowerSets;
using NatureDruidMod.Upgrades;

namespace NatureDruidMod.Towers
{
    public class NatureDruid : ModTower
    {
        public override string Icon => "NatureDruid-Portrait";
        public override string Portrait => "NatureDruid-Portrait";
        public override string BaseTower => TowerType.Druid;
        public override TowerSet TowerSet => TowerSet.Magic;
        public override int Cost => 500;
        public override string Description => "Summons natural forces to fight balloons.";

        public override void ModifyBaseTowerModel(TowerModel towerModel)
        {
            var weapon = towerModel.GetAttackModel().weapons[0];

            weapon.rate = 1.5f;
            weapon.projectile.pierce = 2;
            weapon.projectile.GetDamageModel().damage = 1;
        }
    }
}
