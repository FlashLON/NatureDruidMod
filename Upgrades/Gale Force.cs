using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers;
using NatureDruidMod.Towers;
using BTD_Mod_Helper.Extensions;

public class GaleForce : ModUpgrade<NatureDruid>
{
    public override int Path => 1;
    public override int Tier => 2;
    public override int Cost => 850;
    public override string DisplayName => "Gale Force";
    public override string Description => "Thorns now push Bloons farther back and slow them down.";

    public override void ApplyUpgrade(TowerModel towerModel)
    {
        var attackModel = towerModel.GetAttackModel();
        var weaponModel = attackModel.weapons[0];
        var projectile = weaponModel.projectile;

        // Add Wind push behavior (same as in Tier 1)
        projectile.AddBehavior(new WindModel(
            name: "GaleForce",
            distanceMin: 4f,             // Thorns push further
            distanceMax: 10f,            // Push even further
            chance: 1f,                  // Always applies
            affectMoab: false,           // Doesn't affect Moabs
            overlayType: "GaleEffect",   // Add a visual effect for Gale Force (could be an effect you define)
            distanceScaleForTags: 1f,
            distanceScaleForTagsTags: "",
            speedMultiplier: 0.7f        // Slows down the Bloons
        ));

        // Create the SlowModel with the corrected constructor
        var slowEffect = new SlowModel(
            name: "SlowWind",
            multiplier: 0.5f,            // Slow down effect
            lifespan: 1f,                // Duration of the slow
            mutationId: "SlowWindEffect",// Mutation ID (you can change this if needed)
            layers: 1,                   // Number of layers affected
            overlayType: "SlowWindEffect",// Overlay effect name
            isUnique: true,              // Unique slow effect
            dontRefreshDuration: false,  // Refresh duration or not
            effectModel: null,           // You can set an effect model if needed
            cascadeMutators: false,      // Cascade mutators setting (set to true if you want cascading effects)
            removeMutatorIfNotMatching: false,  // Remove mutator if conditions don't match
            countGlueAchievement: false, // Count for glue achievement
            chance: 1f,                  // 100% chance
            glueLevel: 0,                // Glue level (not relevant here)
            matchLayersWithDamage: false // Match layers with damage (optional)
        );

        // Add the slow effect to the projectile instead of the weapon.
        projectile.AddBehavior(slowEffect);
    }
}
