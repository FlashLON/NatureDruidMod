using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppSystem.Collections.Generic;
using System;
using UnityEngine;

namespace NatureDruidMod.Upgrades
{
    public class NatureCataclysm : ModUpgrade<Towers.NatureDruid>
    {
        public override int Path => 0;
        public override int Tier => 5;
        public override string Icon => "NaturesWrathIcon";
        public override int Cost => 100500;
        public override string DisplayName => "Nature Cataclysm";
        public override string Description =>
            "Unleashes a storm of thorns that cause controlled explosions and periodically fires empowered thorns.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var attackModel = towerModel.GetAttackModel();
            var weapon = attackModel.weapons[0];
            var projectile = weapon.projectile;

            // האטת קצב ירי למניעת עומס
            weapon.Rate *= 1.2f;

            // יצירת הפיצוץ לכל קוץ
            var explosion = projectile.Duplicate();
            explosion.pierce = 10;
            explosion.GetDamageModel().damage = 3;
            explosion.scale = 1.2f;

            var emissionModel = new ArcEmissionModel(
                "ControlledBurst", 6, 0, 360, null, false, false
            );

            projectile.AddBehavior(new CreateProjectileOnContactModel(
                "CataclysmExplosion",
                explosion,
                emissionModel,
                false,
                false,
                false
            ));

            // הוספת super-thorn כל כמה יריות
            var superThorn = projectile.Duplicate();
            superThorn.pierce = 100;
            superThorn.GetDamageModel().damage = 12;
            superThorn.scale = 1.8f;
            superThorn.display = projectile.display; // אפשר להחליף בתצוגה שונה אם תרצה

            var superEmission = new ArcEmissionModel(
                "SuperThornBurst", 8, 0, 360, null, false, false
            );

            var superBehavior = new CreateProjectileOnIntervalModel(
                "SuperThornEvery10Shots", // name
                superThorn,               // projectile
                superEmission,            // emission
                10,                       // count
                false,                    // onlyIfHasTarget
                0f,                       // intervalFrames
                "",                       // sound
                false,                    // randomRotation
                false,                    // canDuplicate
                false                     // ignoreStunned
            );



            projectile.AddBehavior(superBehavior);

            // אפשר להוסיף אפקטים גרפיים נוספים פה אם תרצה
        }
    }
}
