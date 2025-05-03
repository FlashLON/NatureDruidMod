using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using BTD_Mod_Helper.Extensions;
using UnityEngine.Assertions;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;  // הוספתי את הייבוא הנדרש עבור EmissionModel
using System;
using Il2CppAssets.Scripts.Unity;

namespace NatureDruidMod.Upgrades
{
    public class ThornExplosion : ModUpgrade<Towers.NatureDruid>
    {
        public override int Path => 0;
        public override int Tier => 3;
        public override int Cost => 1250;
        public override string Icon => "ThornExplosionIcon";
        public override string DisplayName => "Thorn Explosion";
        public override string Description => "Each thorn explodes on impact, hitting nearby Bloons with sharp fragments.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var weapon = towerModel.GetAttackModel().weapons[0];
            var projectile = weapon.projectile;

            // יצירת פרויקטיל פיצוץ חדש (פוגע בבלונים)
            var explosion = projectile.Duplicate();
            explosion.pierce = 3;
            explosion.GetDamageModel().damage = 1;

            // יצירת אפקט פיצוץ מתוך BombShooter
            var bombTower = Game.instance.model.GetTowerFromId("BombShooter-515");
            var bombExplosionProjectile = bombTower.GetAttackModel().weapons[0].projectile;

            // הוספת אפקט פיצוץ קיים לאפקט של הפרויקטיל החדש
            explosion.display = bombExplosionProjectile.display;

            // יצירת EmissionModel (מודל פליטה)
            var emissionModel = new SingleEmissionModel("ExplosionEmission", null);

            // התנהגות של יצירת פיצוץ רק בעת פגיעה (לא בעת הירי)
            projectile.AddBehavior(new CreateProjectileOnContactModel(
                "CreateExplosion",
                explosion,   // פרויקטיל הפיצוץ
                emissionModel, // פליטת הפיצוץ
                false,       // לא יירו את הפיצוץ מיד
                true,        // יקרה רק כשהקוץ פוגע
                false        // לא יפגע בבלון בכפל (אחד לכל פיצוץ)
            ));
        }




    }
}
