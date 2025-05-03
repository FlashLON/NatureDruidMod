using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Unity;
using NatureDruidMod.Towers;

namespace NatureDruidMod.Upgrades
{
    public class BlizzardHowl : ModUpgrade<NatureDruid>
    {
        public override int Path => 1;
        public override int Tier => 5;
        public override int Cost => 32000;
        public override string Icon => "BlizzardHowlIcon";
        public override string DisplayName => "Blizzard Howl";
        public override string Description => "Massive icy blasts freeze or slow everything nearby. 35% freeze chance and ice explosions also slow all Bloons hit.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            foreach (var weapon in towerModel.GetWeapons())
            {
                var projectile = weapon.projectile;

                // שדרוג הסיכוי להקפאה ל־35%
                var slow = projectile.GetBehavior<SlowModel>();
                if (slow != null)
                {
                    slow.chance = 0.35f;
                    slow.lifespan = 2.5f; // יותר זמן הקפאה
                }

                // יצירת פיצוץ חזק יותר עם אפקט האטה
                var explosion = projectile.Duplicate();
                explosion.pierce = 6;
                explosion.GetDamageModel().damage = 2;

                // הוספת האטה גם לפרויקטיל של הפיצוץ
                explosion.AddBehavior(new SlowModel(
                    name: "ExplosionSlow",
                    multiplier: 0.5f, // האטה ל־50% מהירות
                    lifespan: 2f,
                    mutationId: "ExplosionSlow",
                    layers: 1,
                    overlayType: "FreezeOverlay",
                    isUnique: false,
                    dontRefreshDuration: false,
                    effectModel: null,
                    cascadeMutators: false,
                    removeMutatorIfNotMatching: false,
                    countGlueAchievement: false,
                    chance: 1f, // תמיד מאט
                    glueLevel: 0,
                    matchLayersWithDamage: false
                ));

                explosion.display = Game.instance.model.GetTowerFromId("IceMonkey-555")
                    .GetAttackModel().weapons[0].projectile.display;

                var emissionModel = new SingleEmissionModel("BigIceExplosionEmission", null);

                // שימוש בפרויקטיל להוספת פיצוץ משודרג
                projectile.AddBehavior(new CreateProjectileOnContactModel(
                    "CreateBigIceExplosion",
                    explosion,
                    emissionModel,
                    true,
                    false,
                    false
                ));
            }
        }
    }
}
