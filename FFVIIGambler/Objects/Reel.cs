using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFVIIGambler.Objects
{
    public class Reel : ICloneable
    {
        public Handicap[] Handicaps = new Handicap[3];

        public Reel()
        {
            for (int h = 0; h < Handicaps.Length; h++)
            {
                Handicaps[h] = Objects.Handicap.UNKNOWN;
            }
        }

        public override string ToString()
        {
            return String.Format("[{0}{1}{2}]", HandicapString(Handicaps[0]), HandicapString(Handicaps[1]), HandicapString(Handicaps[2]));
        }

        public static string HandicapString(Handicap h)
        {
            return ((HandicapStr)h).ToString().Replace("D", String.Empty).Replace('Q', '?');
        }

        public static string HandicapFilename(Handicap h)
        {
            string filename = "";

            switch (h)
            {
                case Handicap.GreenMateria:
                    filename = "MagicMateriaIsBroken-ffvii-BSreel.png";
                    break;
                case Handicap.RedMateria:
                    filename = "SummonMateriaIsBroken-ffvii-BSreel.png";
                    break;
                case Handicap.BlueMateria:
                    filename = "SupportMateriaIsBroken-ffvii-BSreel.png";
                    break;
                case Handicap.PurpleMateria:
                    filename = "IndependentMateriaIsBroken-ffvii-BSreel.png";
                    break;
                case Handicap.YellowMateria:
                    filename = "CommandMateriaIsBroken-ffvii-BSreel.png";
                    break;
                case Handicap.AllMateria:
                    filename = "AllMateriaIsBroken-ffvii-BSreel.png";
                    break;
                case Handicap.Accessory:
                    filename = "AccessoryIsBroken-ffvii-BSreel.png";
                    break;
                case Handicap.Item:
                    filename = "ItemCommandIsSealed-ffvii-BSreel.png";
                    break;
                case Handicap.Armor:
                    filename = "ArmorIsBroken-ffvii-BSreel.png";
                    break;
                case Handicap.Weapon:
                    filename = "WeaponIsBroken-ffvii-BSreel.png";
                    break;
                case Handicap.Speed:
                    filename = "1-2Speed-ffvii-BSreel.png";
                    break;
                case Handicap.Mini:
                    filename = "Minimum-ffvii-BSreel.png";
                    break;
                case Handicap.Poison:
                    filename = "Poison-ffvii-BSreel.png";
                    break;
                case Handicap.Toad:
                    filename = "Toad-ffvii-BSreel.png";
                    break;
                case Handicap.TimeDamage:
                    filename = "TimeX30Damage-ffvii-BSreel.png";
                    break;
                case Handicap.FiveLevels:
                    filename = "Down5Levels-ffvii-BSreel.png";
                    break;
                case Handicap.TenLevels:
                    filename = "Down10Levels-ffvii-BSreel.png";
                    break;
                case Handicap.HP:
                    filename = "1-2HP-ffvii-BSreel.png";
                    break;
                case Handicap.MP:
                    filename = "1-2MP-ffvii-BSreel.png";
                    break;
                case Handicap.HPMP:
                    filename = "1-2HP&MP-ffvii-BSreel.png";
                    break;
                case Handicap.ZeroMP:
                    filename = "ZeroMP-ffvii-BSreel.png";
                    break;
                case Handicap.Lucky7:
                    filename = "YesssNoHandicapp-ffvii-BSreel.png";
                    break;
                case Handicap.Cure:
                    filename = "HPRestored-ffvii-BSreel.png";
                    break;
            }

            return filename;
        }

        public object Clone()
        {
            Reel ret = new Reel();
            for (int h = 0; h < Handicaps.Length; h++)
            {
                ret.Handicaps[h] = Handicaps[h];
            }

            return ret;
        }
    }

    public enum HandicapStr : byte
    {
        G = 0x0,
        R = 0x1,
        B = 0x2,
        P = 0x3,
        Y = 0x4,
        N = 0x5,
        X = 0x6,
        I = 0x7,
        A = 0x8,
        W = 0x9,
        V = 0xA,
        U = 0xB,
        S = 0xC,
        O = 0xD,
        F = 0xE,
        T = 0xF,
        D5 = 0x10,
        D1 = 0x11,
        H = 0x12,
        M = 0x13,
        J = 0x14,
        Z = 0x15,
        D7 = 0x16,
        C = 0x17,
        Q = 0xFF
    }

    public enum Handicap : byte
    {
        GreenMateria = 0x0,
        RedMateria = 0x1,
        BlueMateria = 0x2,
        PurpleMateria = 0x3,
        YellowMateria = 0x4,
        AllMateria = 0x5,
        Accessory = 0x6,
        Item = 0x7,
        Armor = 0x8,
        Weapon = 0x9,
        Speed = 0xA,
        Accuracy = 0xB,
        Mini = 0xC,
        Poison = 0xD,
        Toad = 0xE,
        TimeDamage = 0xF,
        FiveLevels = 0x10,
        TenLevels = 0x11,
        HP = 0x12,
        MP = 0x13,
        HPMP = 0x14,
        ZeroMP = 0x15,
        Lucky7 = 0x16,
        Cure = 0x17,
        UNKNOWN = 0xFF
    }
}
