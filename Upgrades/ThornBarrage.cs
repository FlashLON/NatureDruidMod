using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers;

namespace NatureDruidMod.Upgrades
{
    public class ThornBarrage : ModUpgrade<Towers.NatureDruid>
    {
        public override int Path => 0; // נתיב ראשון
        public override int Tier => 2;
        public override int Cost => 450;
        public override string Icon => "ThornBarrageIcon";
        public override string DisplayName => "Thorn Barrage";
        public override string Description => "Shoots spikes at a faster rate to pop the balloons.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            var weapon = towerModel.GetAttackModel().weapons[0];

            weapon.rate *= 0.45f; // מקצר את הזמן בין יריות משמעותית
        }
    }
}
