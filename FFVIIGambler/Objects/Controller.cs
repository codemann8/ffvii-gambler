using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FFVIIGambler.Objects
{
    public class Controller
    {
        public ReelSet[] ReelSets = new ReelSet[128];
        public List<ReelSet> ReelSetsUnique = new List<ReelSet>();

        private const string FILENAME = "ff7battlesquareslotreel.bin";

        public Controller()
        {
            LoadReels();
        }

        private void LoadReels()
        {
            for (int s = 0; s < ReelSets.Length; s++)
            {
                ReelSets[s] = new ReelSet();
            }

            FileStream fs = File.OpenRead(String.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, FILENAME));
            try
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                uint offset = 2, count = 0;

                while (offset < bytes.Length)
                {
                    ReelSets[count / 21].Reels[count % 7].Handicaps[(count / 7) % 3] = (Handicap)bytes[offset];
                    offset += 4;
                    count++;
                }
            }
            finally
            {
                fs.Close();
            }

            ReelSetsUnique = new List<ReelSet>();

            foreach (ReelSet set in ReelSets)
            {
                bool match = false;

                foreach (ReelSet setUnique in ReelSetsUnique)
                {
                    if (setUnique.ToString() == set.ToString())
                    {
                        match = true;
                    }
                }

                if (!match)
                {
                    ReelSetsUnique.Add(set);
                }
            }
        }
    }
}
