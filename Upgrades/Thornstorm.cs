using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts;
using System;

namespace NatureDruidMod.Upgrades
{
    public class Thornstorm : ModUpgrade<Towers.NatureDruid>
    {
        public override int Path => 0;
        public override int Tier => 4;
        public override int Cost => 4200;
        public override string Icon => "ThornstormIcon";
        public override string DisplayName => "Thornstorm";
        public override string Description => "Thorns explode into a swirling storm of mini-thorns that pierce and fly faster.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var weapon = towerModel.GetAttackModel().weapons[0];
            var projectile = weapon.projectile;

            // יצירת פרויקטיל הפיצוץ
            var explosion = projectile.Duplicate();
            explosion.pierce = 3;
            explosion.GetDamageModel().damage = 1;

            // יצירת מיני קוץ
            var miniThorn = projectile.Duplicate();
            miniThorn.pierce = 2;
            miniThorn.scale = 0.5f;
            miniThorn.GetDamageModel().damage = 1;
            miniThorn.display = projectile.display; // שמירה על אותו מראה

            // פליטה מעגלית של 8 מיני קוצים
            var arcEmission = new ArcEmissionModel(
                "MiniThornBurst",   // name
                8,                  // count
                0,                  // startAngle
                360,                // arcAngle
                null,               // emissionBehaviors
                false,              // randomOrder
                false               // useProjectileRotation
            );

            // כאשר הפיצוץ פוגע, הוא משחרר מיני קוצים
            explosion.AddBehavior(new CreateProjectileOnContactModel(
                "MiniThornEmitter",
                miniThorn,
                arcEmission,
                false,
                true,
                false
            ));

            // הקוץ הרגיל יוצר את הפיצוץ
            projectile.AddBehavior(new CreateProjectileOnContactModel(
                "CreateExplosion",
                explosion,
                new SingleEmissionModel("ExplosionEmission", null),
                false,
                true,
                false
            ));

            // שיפור מהירות הירי של הקוף
            weapon.Rate *= 0.85f; // ירי מהיר יותר ב-15%
        }
    }
}
