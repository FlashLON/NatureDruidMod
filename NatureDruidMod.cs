using MelonLoader;
using BTD_Mod_Helper;
using NatureDruidMod;

[assembly: MelonInfo(typeof(NatureDruidMod.NatureDruidMod), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace NatureDruidMod;

public class NatureDruidMod : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        ModHelper.Msg<NatureDruidMod>("NatureDruidMod loaded!");
    }


}