using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFVIIGambler.Objects
{
    public class ReelSet : ICloneable
    {
        public Reel[] Reels = new Reel[7];

        public ReelSet()
        {
            for (int r = 0; r < Reels.Length; r++)
            {
                Reels[r] = new Reel();
            }
        }

        public override string ToString()
        {
            return String.Format("( {0} {1} {2} {3} {4} {5} {6} )", Reels[0], Reels[1], Reels[2], Reels[3], Reels[4], Reels[5], Reels[6]);
        }

        public object Clone()
        {
            ReelSet ret = new ReelSet();
            for (int r = 0; r < Reels.Length; r++)
            {
                ret.Reels[r] = (Reel)Reels[r].Clone();
            }

            return ret;
        }
    }
}
