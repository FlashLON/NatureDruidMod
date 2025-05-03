using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Effects;
using NatureDruidMod.Towers;

public class TempestStorm : ModUpgrade<NatureDruid>
{
    public override int Path => 1;
    public override int Tier => 3;
    public override int Cost => 2200;
    public override string Icon => "TempestStormIcon";
    public override string DisplayName => "Tempest Storm";
    public override string Description => "Thorns now unleash powerful storms that knock back Bloons even further and summon lightning clouds.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var weaponModel = attackModel.weapons[0];
        var projectile = weaponModel.projectile;

        // שדרוג WindModel: הגדלת מרווחי הדחיפה ושינוי overlayType
        var wind = projectile.GetBehavior<WindModel>();
        if (wind != null)
        {
            wind.distanceMin += 2f;
            wind.distanceMax += 3f;
            wind.speedMultiplier *= 0.6f;
            wind.overlayType = "TempestOverlay";  // overlay חדש המייצג ענן סוער
        }

        // הוספת אפקט של "ענן ברק" במגע – משתמשים overload שמקבל 2 פרמטרים בלבד:
        projectile.AddBehavior(new CreateEffectOnContactModel(
            "LightningCloudEffect",
            null    // העבר כאן EffectModel מתאים אם יש – לעת עתה null
        ));

        // האטה נוספת – מנסים להכפיל את המכפיל אם SlowModel קיים
        var slow = projectile.GetBehavior<SlowModel>();
        if (slow != null)
        {
            slow.multiplier *= 0.8f;
        }

        // הגדלת הערך של pierce כך שהקוץ יפגע ביותר בלונים
        projectile.pierce += 2;
    }
}
