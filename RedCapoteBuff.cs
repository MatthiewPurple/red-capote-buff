using MelonLoader;
using Il2Cpp;
using red_capote_buff;
using HarmonyLib;

[assembly: MelonInfo(typeof(RedCapoteBuff), "Red Capote buff", "1.0.0", "Matthiew Purple")]
[assembly: MelonGame("アトラス", "smt3hd")]

namespace red_capote_buff;
public class RedCapoteBuff : MelonMod
{
    private static bool isRedCapote; // is true when the last used skill was Red Capote

    // After getting the description of a skill
    [HarmonyPatch(typeof(datSkillHelp_msg), nameof(datSkillHelp_msg.Get))]
    private class Patch
    {
        public static void Postfix(ref int id, ref string __result)
        {
            if (id == 276)
            {
                __result = "Maximizes Evasion/Hit Rate.";
            }
        }
    }

    // Before displaying the text box
    [HarmonyPatch(typeof(nbHelpProcess), nameof(nbHelpProcess.nbDispText))]
    private class Patch2
    {
        public static void Prefix(ref string text1, ref int type)
        {
            // If the text box is displaying the effect of Red Capote
            if (type == 1 && isRedCapote)
            {
                type = 0;
                text1 = "Evasion/Hit Rate maximized!";
                isRedCapote = false;
            }
        }
    }

    // Before displying a skill name in the text box
    [HarmonyPatch(typeof(nbHelpProcess), nameof(nbHelpProcess.nbDispSkillName))]
    private class Patch3
    {
        public static void Prefix(ref int id)
        {
            isRedCapote = id == 276;
        }
    }

    // When launching the game
    public override void OnInitializeMelon()
    {
        // Buffs Red Capote
        datNormalSkill.tbl[276].hojopoint = 8; // Self-Sukukaja x 8
    }
}
