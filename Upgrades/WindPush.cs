using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using NatureDruidMod.Towers;

namespace NatureDruidMod.Upgrades
{
    public class WindPush : ModUpgrade<NatureDruid>
    {
        public override int Path => 1;
        public override int Tier => 1;
        public override int Cost => 450;
        public override string Icon => "WindPushIcon";
        public override string DisplayName => "Wind Push";
        public override string Description => "Each thorn slightly pushes Bloons backwards.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var projectile = towerModel.GetAttackModel().weapons[0].projectile;

            projectile.AddBehavior(new WindModel(
                name: "WindPush",
                distanceMin: 2f,
                distanceMax: 6f,
                chance: 1f,
                affectMoab: false,
                overlayType: "WindEffect",               // יכול להיות "WindEffect" אם יש לך אפקט
                distanceScaleForTags: 1f,
                distanceScaleForTagsTags: "",
                speedMultiplier: 1f
            ));
        }
    }


}
