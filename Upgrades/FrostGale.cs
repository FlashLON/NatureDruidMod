using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Unity;
using NatureDruidMod.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions; // נדרש ל־SingleEmissionModel

namespace NatureDruidMod.Upgrades
{
    public class FrostGaleUpgrade : ModUpgrade<NatureDruid>
    {
        public override int Path => 1;
        public override int Tier => 4;
        public override int Cost => 12000;
        public override string Icon => "FrostGaleIcon";
        public override string DisplayName => "Frost Gale Upgrade";
        public override string Description => "Adds a 15% chance to freeze Bloons for 2 seconds on every hit";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            // עבור כל נשק של הטיל, מוסיפים אפקט הקפאה עם סיכוי
            foreach (var weapon in towerModel.GetWeapons())
            {
                var projectile = weapon.projectile;
                projectile.AddBehavior(new SlowModel(
                    name: "FrostFreezeUpgrade",
                    multiplier: 0f,
                    lifespan: 2f,
                    mutationId: "Freeze",
                    layers: 1,
                    overlayType: "FreezeOverlay",
                    isUnique: true,
                    dontRefreshDuration: false,
                    effectModel: null,
                    cascadeMutators: false,
                    removeMutatorIfNotMatching: false,
                    countGlueAchievement: false,
                    chance: 0.15f,
                    glueLevel: 0,
                    matchLayersWithDamage: false
                ));

                // יצירת פרויקטיל פיצוץ חדש עם אפקט קרח
                var explosion = projectile.Duplicate();
                explosion.pierce = 3;
                explosion.GetDamageModel().damage = 1;

                // שימוש באפקט של IceMonkey לפרויקטיל
                var iceTower = Game.instance.model.GetTowerFromId("IceMonkey-555");
                var iceProjectile = iceTower.GetAttackModel().weapons[0].projectile;

                explosion.display = iceProjectile.display;

                // יצירת EmissionModel
                var emissionModel = new SingleEmissionModel("IceExplosionEmission", null);

                // גורם לפרויקטיל להוציא פרויקטיל קרח עם פגיעה
                projectile.AddBehavior(new CreateProjectileOnContactModel(
                    "CreateIceExplosion",
                    explosion,              // projectile
                    emissionModel,          // emission
                    true,                   // createOnContact
                    false,                  // createOnDestroy
                    false                   // affectAllTargets
                ));

            }
        }
    }
}
