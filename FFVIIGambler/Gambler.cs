using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFVIIGambler
{
    public partial class Gambler : Form
    {
        public Objects.Controller Controller;
        public Objects.ReelSet CurrentReelSet;
        public int CountHandicaps;

        public List<Objects.ReelSet> SearchResults;

        public Gambler()
        {
            InitializeComponent();
        }

        private void Gambler_Load(object sender, EventArgs e)
        {
            Controller = new Objects.Controller();

            ResetReel();

            btnReset.Select();
        }

        private void Gambler_KeyDown(object sender, KeyEventArgs e)
        {
            Objects.Handicap handicap = Objects.Handicap.UNKNOWN;

            switch (e.KeyCode)
            {
                case Keys.G:
                    handicap = Objects.Handicap.GreenMateria;
                    break;
                case Keys.R:
                    handicap = Objects.Handicap.RedMateria;
                    break;
                case Keys.B:
                    handicap = Objects.Handicap.BlueMateria;
                    break;
                case Keys.P:
                    handicap = Objects.Handicap.PurpleMateria;
                    break;
                case Keys.Y:
                    handicap = Objects.Handicap.YellowMateria;
                    break;
                case Keys.N:
                    handicap = Objects.Handicap.AllMateria;
                    break;
                case Keys.X:
                    handicap = Objects.Handicap.Accessory;
                    break;
                case Keys.I:
                    handicap = Objects.Handicap.Item;
                    break;
                case Keys.A:
                    handicap = Objects.Handicap.Armor;
                    break;
                case Keys.W:
                    handicap = Objects.Handicap.Weapon;
                    break;
                case Keys.V:
                    handicap = Objects.Handicap.Speed;
                    break;
                //case Keys.U:
                //    handicap = Objects.Handicap.Accuracy;
                //    break;
                case Keys.S:
                    handicap = Objects.Handicap.Mini;
                    break;
                case Keys.O:
                    handicap = Objects.Handicap.Poison;
                    break;
                case Keys.F:
                    handicap = Objects.Handicap.Toad;
                    break;
                case Keys.T:
                    handicap = Objects.Handicap.TimeDamage;
                    break;
                case Keys.D5:
                    handicap = Objects.Handicap.FiveLevels;
                    break;
                case Keys.D1:
                    handicap = Objects.Handicap.TenLevels;
                    break;
                case Keys.H:
                    handicap = Objects.Handicap.HP;
                    break;
                case Keys.M:
                    handicap = Objects.Handicap.MP;
                    break;
                case Keys.J:
                    handicap = Objects.Handicap.HPMP;
                    break;
                case Keys.Z:
                    handicap = Objects.Handicap.ZeroMP;
                    break;
                case Keys.D7:
                    handicap = Objects.Handicap.Lucky7;
                    break;
                case Keys.C:
                    handicap = Objects.Handicap.Cure;
                    break;
                default:
                    //neither
                    break;
            }

            if (handicap != Objects.Handicap.UNKNOWN && (SearchResults.Count > 0 || CountHandicaps == 0))
            {
                bool inserted = false;

                if (CountHandicaps < 21 && CurrentReelSet.Reels[CountHandicaps / 3].Handicaps[CountHandicaps % 3] == Objects.Handicap.UNKNOWN)
                {
                    CurrentReelSet.Reels[CountHandicaps / 3].Handicaps[CountHandicaps % 3] = handicap;
                    ((PictureBox)this.Controls["pictureBox" + (CountHandicaps + 1)]).Image = Image.FromFile(String.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, Objects.Reel.HandicapFilename(handicap)));
                    inserted = true;

                    CountHandicaps++;
                }

                if (inserted)
                {
                    if (CountHandicaps == 1)
                    {
                        foreach (Objects.ReelSet set in Controller.ReelSetsUnique)
                        {
                            if (set.Reels[0].Handicaps.Contains(handicap))
                            {
                                SearchResults.Add(set);
                            }
                        }
                    }
                    else
                    {
                        foreach (Objects.ReelSet set in SearchResults.ToList())
                        {
                            if (!set.Reels[(CountHandicaps - 1) / 3].Handicaps.Contains(handicap))
                            {
                                SearchResults.Remove(set);
                            }
                        }
                    }

                    if (SearchResults.Count == 1)
                    {
                        CountHandicaps = 21;
                    }

                    if (SearchResults.Count == 0)
                    {
                        MessageBox.Show("Invalid reel combination");
                    }
                }
            }

            UpdateReels();
        }

        private void UpdateReels()
        {
            if (SearchResults.Count == 1)
            {
                for (int p = 1; p <= 21; p++)
                {
                    ((PictureBox)this.Controls["pictureBox" + p]).Image = Image.FromFile(String.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, Objects.Reel.HandicapFilename(SearchResults[0].Reels[(p - 1) / 3].Handicaps[(p - 1) % 3])));
                }
            }
            else if (CountHandicaps == 0)
            {
                for (int p = 1; p <= 21; p++)
                {
                    if (((PictureBox)this.Controls["pictureBox" + p]).Image != null)
                    {
                        ((PictureBox)this.Controls["pictureBox" + p]).Image.Dispose();
                        ((PictureBox)this.Controls["pictureBox" + p]).Image = null;
                    }
                }
            }
        }

        private void ResetReel()
        {
            CurrentReelSet = new Objects.ReelSet();
            CountHandicaps = 0;
            SearchResults = new List<Objects.ReelSet>();

            UpdateReels();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetReel();
        }

        private void btnMagic_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.G));
        }

        private void btnSummon_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.R));
        }

        private void btnSupport_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.B));
        }

        private void btnIndependent_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.P));
        }

        private void btnCommand_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.Y));
        }

        private void btnMateria_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.N));
        }

        private void btnAccessory_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.X));
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.I));
        }

        private void btnArmor_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.A));
        }

        private void btnWeapon_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.W));
        }

        private void btnSpeed_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.V));
        }

        private void btnAccuracy_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.U));
        }

        private void btnMini_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.S));
        }

        private void btnPoison_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.O));
        }

        private void btnToad_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.F));
        }

        private void btnTime_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.T));
        }

        private void btnFiveLevels_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.D5));
        }

        private void btnTenLevels_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.D1));
        }

        private void btnHP_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.H));
        }

        private void btnMP_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.M));
        }

        private void btnHPMP_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.J));
        }

        private void btnZeroMP_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.Z));
        }

        private void btnLucky_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.D7));
        }

        private void btnCure_Click(object sender, EventArgs e)
        {
            Gambler_KeyDown(this, new KeyEventArgs(Keys.C));
        }
    }
}
