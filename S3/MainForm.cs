using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ImageMagick;
using Nancy.Hosting.Self;
using Newtonsoft.Json;
namespace S3
{
    public partial class MainForm : Form
    {
        private NancyHost hostg;
        public MainForm()
        {
            InitializeComponent();
            Globals.CurrentCrewUpdate = new CrewUpdate();

            Globals.CurrentCrewUpdate.Crew1 = "TESTA";
            Globals.CurrentCrewUpdate.Crew1Stocks = 16;

            Globals.CurrentCrewUpdate.Crew2 = "TESTB";
            Globals.CurrentCrewUpdate.Crew2Stocks = 16;

            Globals.CurrentCrewUpdate.A1 = new Crewmember();
            Globals.CurrentCrewUpdate.A2 = new Crewmember();
            Globals.CurrentCrewUpdate.A3 = new Crewmember();
            Globals.CurrentCrewUpdate.A4 = new Crewmember();

            Globals.CurrentCrewUpdate.B1 = new Crewmember();
            Globals.CurrentCrewUpdate.B2 = new Crewmember();
            Globals.CurrentCrewUpdate.B3 = new Crewmember();
            Globals.CurrentCrewUpdate.B4 = new Crewmember();

            Globals.CurrentCrewUpdate.A1.name = "? ? ?";
            Globals.CurrentCrewUpdate.A2.name = "? ? ?";
            Globals.CurrentCrewUpdate.A3.name = "? ? ?";
            Globals.CurrentCrewUpdate.A4.name = "? ? ?";

            Globals.CurrentCrewUpdate.B1.name = "? ? ?";
            Globals.CurrentCrewUpdate.B2.name = "? ? ?";
            Globals.CurrentCrewUpdate.B3.name = "? ? ?";
            Globals.CurrentCrewUpdate.B4.name = "? ? ?";

            Globals.bracketInfo = new BracketUpdate();
            // Singles
            // Winner's Semis
            Globals.bracketInfo.WSA = new Bracket();
            Globals.bracketInfo.WSB = new Bracket();
            // Winner's Finals
            Globals.bracketInfo.WF = new Bracket();
            // Grand Finals
            Globals.bracketInfo.GF = new Bracket();
            // Bracket Reset
            Globals.bracketInfo.BR = new Bracket();
            // Loser's Finals
            Globals.bracketInfo.LF = new Bracket();
            // Loser's Semis
            Globals.bracketInfo.LSF = new Bracket();
            // Loser's Quarters
            Globals.bracketInfo.LQA = new Bracket();
            Globals.bracketInfo.LQB = new Bracket();
            // Loser's Top8
            Globals.bracketInfo.L8A = new Bracket();
            Globals.bracketInfo.L8B = new Bracket();
            // Doubles
            // Winner's Semis
            Globals.bracketInfo.DWSA = new Bracket();
            Globals.bracketInfo.DWSB = new Bracket();
            // Winner's Finals
            Globals.bracketInfo.DWF = new Bracket();
            // Grand Finals
            Globals.bracketInfo.DGF = new Bracket();
            // Bracket Reset
            Globals.bracketInfo.DBR = new Bracket();
            // Loser's Finals
            Globals.bracketInfo.DLF = new Bracket();
            // Loser's Semis
            Globals.bracketInfo.DLSF = new Bracket();
            // Loser's Quarters
            Globals.bracketInfo.DLQA = new Bracket();
            Globals.bracketInfo.DLQB = new Bracket();

            Globals.CurrentInformationUpdate = new InformationUpdate();
            Globals.CurrentInformationUpdate.Player1 = new Player();
            Globals.CurrentInformationUpdate.Player2 = new Player();
            Globals.CurrentInformationUpdate.Player1.name = "EIREXE";
            Globals.CurrentInformationUpdate.Player2.name = "BoastingToast";
            parseComboBoxItems();
            SendUpdate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SendUpdateButton_Click(object sender, EventArgs e)
        {
            SendUpdate();
            updateBracket();
            updateCrews();
        }

        private void SendUpdate()
        {
            Globals.CurrentInformationUpdate.Player1.name = Player1Name.Text;
            Globals.CurrentInformationUpdate.Player2.name = Player2Name.Text;
            Globals.CurrentInformationUpdate.Player1.sponsor = (Sponsor)((ComboboxItem)Player1Sponsor.SelectedItem).Value;
            Globals.CurrentInformationUpdate.Player2.sponsor = (Sponsor)((ComboboxItem)Player2Sponsor.SelectedItem).Value;
            Globals.CurrentInformationUpdate.Player1.character = (Character)((ComboboxItem)Player1Character.SelectedItem).Value;
            Globals.CurrentInformationUpdate.Player2.character = (Character)((ComboboxItem)Player2Character.SelectedItem).Value;
            Globals.CurrentInformationUpdate.Player1.score = Decimal.ToInt32(Player1Score.Value);
            Globals.CurrentInformationUpdate.Player2.score = Decimal.ToInt32(Player2Score.Value);
            if (cbSwapFields.Checked == false)
            {
                Globals.CurrentInformationUpdate.tournamentName = tournamentNameTextbox.Text;
                Globals.CurrentInformationUpdate.round = RoundNameTextbox.Text;
            }
            else
            {
                Globals.CurrentInformationUpdate.tournamentName = RoundNameTextbox.Text;
                Globals.CurrentInformationUpdate.round = tournamentNameTextbox.Text;
            }

            Globals.CurrentInformationUpdate.caster = CasterTextbox.Text;
            Globals.CurrentInformationUpdate.streamer = StreamerTextbox.Text;
            Globals.CurrentInformationUpdate.Player1.flag = ((Flag)((ComboboxItem)FlagsCombo.SelectedItem).Value);
            Globals.CurrentInformationUpdate.Player2.flag = ((Flag)((ComboboxItem)FlagsComboP2.SelectedItem).Value);

            if (radioBO3.Checked == true) { Globals.CurrentInformationUpdate.matchType = "BEST OF 3"; }
            else if (radioBO5.Checked == true) { Globals.CurrentInformationUpdate.matchType = "BEST OF 5"; }
            else if (radioOther.Checked == true) { Globals.CurrentInformationUpdate.matchType = ""; }
            else { Globals.CurrentInformationUpdate.matchType = ""; }

            if (radioLTE.Checked == true) { Globals.CurrentInformationUpdate.iconStyle = "LEGACY"; }
            else if (radioLTE2HD.Checked == true) { Globals.CurrentInformationUpdate.iconStyle = "LTE2HD"; }
            else if (radioLTEStock.Checked == true) { Globals.CurrentInformationUpdate.iconStyle = "LTESTOCK"; }
            else { Globals.CurrentInformationUpdate.iconStyle = "PMSTOCK"; }
            lblStyle.Text = Globals.CurrentInformationUpdate.iconStyle;
        }
        private void updateBracket()
        {
            //// Singles
            // Winner's Semis
            Globals.bracketInfo.WSA.Player1 = tbWSA1.Text;
            Globals.bracketInfo.WSA.Player1score = Convert.ToInt16(numWSA1.Value);
            Globals.bracketInfo.WSA.Player2 = tbWSA2.Text;
            Globals.bracketInfo.WSA.Player2score = Convert.ToInt16(numWSA2.Value);

            Globals.bracketInfo.WSB.Player1 = tbWSB1.Text;
            Globals.bracketInfo.WSB.Player1score = Convert.ToInt16(numWSB1.Value);
            Globals.bracketInfo.WSB.Player2 = tbWSB2.Text;
            Globals.bracketInfo.WSB.Player2score = Convert.ToInt16(numWSB2.Value);
            // Winner's Finals
            Globals.bracketInfo.WF.Player1 = tbWF1.Text;
            Globals.bracketInfo.WF.Player1score = Convert.ToInt16(numWF1.Value);
            Globals.bracketInfo.WF.Player2 = tbWF2.Text;
            Globals.bracketInfo.WF.Player2score = Convert.ToInt16(numWF2.Value);
            // Grand Finals
            Globals.bracketInfo.GF.Player1 = tbGF1.Text;
            Globals.bracketInfo.GF.Player1score = Convert.ToInt16(numGF1.Value);
            Globals.bracketInfo.GF.Player2 = tbGF2.Text;
            Globals.bracketInfo.GF.Player2score = Convert.ToInt16(numGF2.Value);
            // Bracket Reset
            Globals.bracketInfo.BR.Player1 = tbBR1.Text;
            Globals.bracketInfo.BR.Player1score = Convert.ToInt16(numBR1.Value);
            Globals.bracketInfo.BR.Player2 = tbBR2.Text;
            Globals.bracketInfo.BR.Player2score = Convert.ToInt16(numBR2.Value);
            // Losers Finals
            Globals.bracketInfo.LF.Player1 = tbLF1.Text;
            Globals.bracketInfo.LF.Player1score = Convert.ToInt16(numLF1.Value);
            Globals.bracketInfo.LF.Player2 = tbLF2.Text;
            Globals.bracketInfo.LF.Player2score = Convert.ToInt16(numLF2.Value);
            // Losers Semis
            Globals.bracketInfo.LSF.Player1 = tbLS1.Text;
            Globals.bracketInfo.LSF.Player1score = Convert.ToInt16(numLS1.Value);
            Globals.bracketInfo.LSF.Player2 = tbLS2.Text;
            Globals.bracketInfo.LSF.Player2score = Convert.ToInt16(numLS2.Value);
            // Losers Quarters
            Globals.bracketInfo.LQA.Player1 = tbLQA1.Text;
            Globals.bracketInfo.LQA.Player1score = Convert.ToInt16(numLQA1.Value);
            Globals.bracketInfo.LQA.Player2 = tbLQA2.Text;
            Globals.bracketInfo.LQA.Player2score = Convert.ToInt16(numLQA2.Value);

            Globals.bracketInfo.LQB.Player1 = tbLQB1.Text;
            Globals.bracketInfo.LQB.Player1score = Convert.ToInt16(numLQB1.Value);
            Globals.bracketInfo.LQB.Player2 = tbLQB2.Text;
            Globals.bracketInfo.LQB.Player2score = Convert.ToInt16(numLQB2.Value);
            // Losers Top 8
            Globals.bracketInfo.L8A.Player1 = tbL8A1.Text;
            Globals.bracketInfo.L8A.Player1score = Convert.ToInt16(numL8A1.Value);
            Globals.bracketInfo.L8A.Player2 = tbL8A2.Text;
            Globals.bracketInfo.L8A.Player2score = Convert.ToInt16(numL8A2.Value);

            Globals.bracketInfo.L8B.Player1 = tbL8B1.Text;
            Globals.bracketInfo.L8B.Player1score = Convert.ToInt16(numL8B1.Value);
            Globals.bracketInfo.L8B.Player2 = tbL8B2.Text;
            Globals.bracketInfo.L8B.Player2score = Convert.ToInt16(numL8B2.Value);

            //// Doubles
            // Winner's Semis
            Globals.bracketInfo.DWSA.Player1 = tbDWSA1.Text;
            Globals.bracketInfo.DWSA.Player1score = Convert.ToInt16(numDWSA1.Value);
            Globals.bracketInfo.DWSA.Player2 = tbDWSA2.Text;
            Globals.bracketInfo.DWSA.Player2score = Convert.ToInt16(numDWSA2.Value);

            Globals.bracketInfo.DWSB.Player1 = tbDWSB1.Text;
            Globals.bracketInfo.DWSB.Player1score = Convert.ToInt16(numDWSB1.Value);
            Globals.bracketInfo.DWSB.Player2 = tbDWSB2.Text;
            Globals.bracketInfo.DWSB.Player2score = Convert.ToInt16(numDWSB2.Value);
            // Winner's Finals
            Globals.bracketInfo.DWF.Player1 = tbDWF1.Text;
            Globals.bracketInfo.DWF.Player1score = Convert.ToInt16(numDWF1.Value);
            Globals.bracketInfo.DWF.Player2 = tbDWF2.Text;
            Globals.bracketInfo.DWF.Player2score = Convert.ToInt16(numDWF2.Value);
            // Grand Finals
            Globals.bracketInfo.DGF.Player1 = tbDGF1.Text;
            Globals.bracketInfo.DGF.Player1score = Convert.ToInt16(numDGF1.Value);
            Globals.bracketInfo.DGF.Player2 = tbDGF2.Text;
            Globals.bracketInfo.DGF.Player2score = Convert.ToInt16(numDGF2.Value);
            // Bracket Reset
            Globals.bracketInfo.DBR.Player1 = tbDBR1.Text;
            Globals.bracketInfo.DBR.Player1score = Convert.ToInt16(numDBR1.Value);
            Globals.bracketInfo.DBR.Player2 = tbDBR2.Text;
            Globals.bracketInfo.DBR.Player2score = Convert.ToInt16(numDBR2.Value);
            // Losers Finals
            Globals.bracketInfo.DLF.Player1 = tbDLF1.Text;
            Globals.bracketInfo.DLF.Player1score = Convert.ToInt16(numDLF1.Value);
            Globals.bracketInfo.DLF.Player2 = tbDLF2.Text;
            Globals.bracketInfo.DLF.Player2score = Convert.ToInt16(numDLF2.Value);
            // Losers Semis
            Globals.bracketInfo.DLSF.Player1 = tbDLS1.Text;
            Globals.bracketInfo.DLSF.Player1score = Convert.ToInt16(numDLS1.Value);
            Globals.bracketInfo.DLSF.Player2 = tbDLS2.Text;
            Globals.bracketInfo.DLSF.Player2score = Convert.ToInt16(numDLS1.Value);
            // Losers Quarters
            Globals.bracketInfo.DLQA.Player1 = tbDLQA1.Text;
            Globals.bracketInfo.DLQA.Player1score = Convert.ToInt16(numDLQA1.Value);
            Globals.bracketInfo.DLQA.Player2 = tbDLQA2.Text;
            Globals.bracketInfo.DLQA.Player2score = Convert.ToInt16(numDLQA2.Value);

            Globals.bracketInfo.DLQB.Player1 = tbDLQB1.Text;
            Globals.bracketInfo.DLQB.Player1score = Convert.ToInt16(numDLQB1.Value);
            Globals.bracketInfo.DLQB.Player2 = tbDLQB2.Text;
            Globals.bracketInfo.DLQB.Player2score = Convert.ToInt16(numDLQB2.Value);
        }
        private void updateCrews()
        {
            // Grab data from globals
            Globals.CurrentCrewUpdate.iconStyle = Globals.CurrentInformationUpdate.iconStyle;
            Globals.CurrentCrewUpdate.matchType = "Crew Battle";
            Globals.CurrentCrewUpdate.streamer = Globals.CurrentInformationUpdate.streamer;
            Globals.CurrentCrewUpdate.caster = Globals.CurrentInformationUpdate.caster;
            Globals.CurrentCrewUpdate.round = Globals.CurrentInformationUpdate.round;
            Globals.CurrentCrewUpdate.tournamentName = Globals.CurrentInformationUpdate.tournamentName;

            Globals.CurrentCrewUpdate.Crew1 = tbCrewNameA.Text;
            Globals.CurrentCrewUpdate.Crew1Stocks = Convert.ToInt16(numStockA.Value);
            Globals.CurrentCrewUpdate.Crew2 = tbCrewNameB.Text;
            Globals.CurrentCrewUpdate.Crew2Stocks = Convert.ToInt16(numStockB.Value);

            Globals.CurrentCrewUpdate.A1.name = tbA1.Text;
            Globals.CurrentCrewUpdate.A1.character = (Character)((ComboboxItem)crewCharA1.SelectedItem).Value;
            Globals.CurrentCrewUpdate.A2.name = tbA2.Text;
            Globals.CurrentCrewUpdate.A2.character = (Character)((ComboboxItem)crewCharA2.SelectedItem).Value;
            Globals.CurrentCrewUpdate.A3.name = tbA3.Text;
            Globals.CurrentCrewUpdate.A3.character = (Character)((ComboboxItem)crewCharA3.SelectedItem).Value;
            Globals.CurrentCrewUpdate.A4.name = tbA4.Text;
            Globals.CurrentCrewUpdate.A4.character = (Character)((ComboboxItem)crewCharA4.SelectedItem).Value;

            Globals.CurrentCrewUpdate.B1.name = tbB1.Text;
            Globals.CurrentCrewUpdate.B1.character = (Character)((ComboboxItem)crewCharB1.SelectedItem).Value;
            Globals.CurrentCrewUpdate.B2.name = tbB2.Text;
            Globals.CurrentCrewUpdate.B2.character = (Character)((ComboboxItem)crewCharB2.SelectedItem).Value;
            Globals.CurrentCrewUpdate.B3.name = tbB3.Text;
            Globals.CurrentCrewUpdate.B3.character = (Character)((ComboboxItem)crewCharB3.SelectedItem).Value;
            Globals.CurrentCrewUpdate.B4.name = tbB4.Text;
            Globals.CurrentCrewUpdate.B4.character = (Character)((ComboboxItem)crewCharB4.SelectedItem).Value;
        }
            private void parseComboBoxItems()
        {
            string file = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "characters.json");
            string contents = File.ReadAllText(file);
            CharacterList list = JsonConvert.DeserializeObject<CharacterList>(contents);
            
            foreach(Character c in list.characters)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = c.name;
                item.Value = c;
                Player1Character.Items.Add(item);
                Player2Character.Items.Add(item);

                crewCharA1.Items.Add(item);
                crewCharA2.Items.Add(item);
                crewCharA3.Items.Add(item);
                crewCharA4.Items.Add(item);

                crewCharB1.Items.Add(item);
                crewCharB2.Items.Add(item);
                crewCharB3.Items.Add(item);
                crewCharB4.Items.Add(item);
            }
            Player1Character.SelectedIndex = 0;
            Player2Character.SelectedIndex = 0;
            Player1Character.Sorted = true;
            Player2Character.Sorted = true;

            crewCharA1.Sorted = true;
            crewCharA2.Sorted = true;
            crewCharA3.Sorted = true;
            crewCharA4.Sorted = true;

            crewCharB1.Sorted = true;
            crewCharB2.Sorted = true;
            crewCharB3.Sorted = true;
            crewCharB4.Sorted = true;

            crewCharA1.SelectedIndex = 0;
            crewCharA2.SelectedIndex = 0;
            crewCharA3.SelectedIndex = 0;
            crewCharA4.SelectedIndex = 0;

            crewCharB1.SelectedIndex = 0;
            crewCharB2.SelectedIndex = 0;
            crewCharB3.SelectedIndex = 0;
            crewCharB4.SelectedIndex = 0;

            string sponsors = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "sponsors.json");
            string sponsorsContents = File.ReadAllText(sponsors);
            SponsorList sponsorslist = JsonConvert.DeserializeObject<SponsorList>(sponsorsContents);

            foreach (Sponsor s in sponsorslist.sponsors)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = s.name;
                item.Value = s;
                Player1Sponsor.Items.Add(item);
                Player2Sponsor.Items.Add(item);

            }
            Player1Character.SelectedIndex = 0;
            Player2Character.SelectedIndex = 0;
            Player1Sponsor.SelectedIndex = 0;
            Player2Sponsor.SelectedIndex = 0;
            string flags = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "flags.json");
            string flagsContents = File.ReadAllText(flags);
            FlagList flagsList = JsonConvert.DeserializeObject<FlagList>(flagsContents);
            foreach (Flag flag in flagsList.flags)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = flag.name;
                item.Value = flag;
                FlagsCombo.Items.Add(item);
                FlagsComboP2.Items.Add(item);
            }
            FlagsCombo.SelectedIndex = 0;
            FlagsComboP2.SelectedIndex = 0;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Scoreboard Settings Data(.auboard)|*.auboard|All Files(*.*) | *.*";
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                string contents = File.ReadAllText(dialog.FileName);
                Settings settings = JsonConvert.DeserializeObject<Settings>(contents);
                Globals.settings = settings;
                updateData();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Scoreboard Settings Data(.auboard)|*.auboard|All Files(*.*) | *.*";
            Globals.settings.streamData = Globals.CurrentInformationUpdate;
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK || result == DialogResult.Yes)
            {
                File.WriteAllText(dialog.FileName,JsonConvert.SerializeObject(Globals.settings, Formatting.Indented));
            }
            
        }

        private void updateData()
        {
            Player1Name.Text = Globals.settings.streamData.Player1.name;
            Player2Name.Text = Globals.settings.streamData.Player2.name;
            Player1Sponsor.Text = Globals.settings.streamData.Player1.sponsor.name;
            Player2Sponsor.Text = Globals.settings.streamData.Player2.sponsor.name;
            Player1Character.Text = Globals.settings.streamData.Player1.character.name;
            Player2Character.Text = Globals.settings.streamData.Player2.character.name;
            Player1Score.Text = Globals.settings.streamData.Player1.score.ToString();
            Player2Score.Text = Globals.settings.streamData.Player2.score.ToString();
            FlagsCombo.Text = Globals.settings.streamData.Player1.flag.name;
            FlagsComboP2.Text = Globals.settings.streamData.Player2.flag.name;
            RoundNameTextbox.Text = Globals.settings.streamData.round;
            tournamentNameTextbox.Text = Globals.settings.streamData.tournamentName;
            StreamerTextbox.Text = Globals.settings.streamData.streamer;
            CasterTextbox.Text = Globals.settings.streamData.caster;
            SendUpdate();
        }
        private bool isServerUp = false;
        private void StartServer_Click(object sender, EventArgs e)
        {


            
            if (isServerUp)
            {
                hostg.Stop();
                StartServer.Text = "Start Server";
                isServerUp = false;
            }
            else
            {
                if (Globals.settings.tintEnabled)
                {
                    foreach (string file in Directory.GetFiles(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Content/html/img")))
                    {
                        using (MagickImage image = new MagickImage(file))
                        {
                            image.Colorize(new MagickColor(Globals.settings.tintColor), new Percentage(100));
                            image.Write(file);
                        }
                    }
                }
                isServerUp = true;
                try
                {
                    UrlLinkLabel.Text = "http://127.0.0.1:" + Globals.settings.serverPort + "/Content/html/scoreboard.html";
                    HostConfiguration config = new HostConfiguration();
                    config.UrlReservations.CreateAutomatically = true;
                    NancyHost host = new NancyHost(config, new Uri("http://127.0.0.1:" + Globals.settings.serverPort));

                    host.Start();
                    hostg = host;
                    StartServer.Text = "Stop Server";
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                    throw;
                }
            }
        }

        private void UrlLinkLabel_Click(object sender, EventArgs e)
        {
            Process.Start(UrlLinkLabel.Text);
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void radioLTE_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void UrlLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Player1Score_ValueChanged(object sender, EventArgs e)
        {
            SendUpdate();
        }

        private void Player2Score_ValueChanged(object sender, EventArgs e)
        {
            SendUpdate();
        }

        private void groupBox7_Enter(object sender, EventArgs e)
        {

        }

        private void swapChars_Click(object sender, EventArgs e)
        {
            string tempPlayerName = Player1Name.Text;
            int tempScore = Convert.ToInt16(Player1Score.Value);
            int tempSponsor = Player1Sponsor.SelectedIndex;
            int tempCharacter = Player1Character.SelectedIndex;

            Player1Name.Text = Player2Name.Text;
            Player1Score.Value = Player2Score.Value;
            Player1Sponsor.SelectedIndex = Player2Sponsor.SelectedIndex;
            Player1Character.SelectedIndex = Player2Character.SelectedIndex;

            Player2Name.Text = tempPlayerName;
            Player2Score.Value = tempScore;
            Player2Sponsor.SelectedIndex = tempSponsor;
            Player2Character.SelectedIndex = tempCharacter;
        }

        private void Player1Score_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void Player1Sponsor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioOther_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click_1(object sender, EventArgs e)
        {

        }

        private void groupBox25_Enter(object sender, EventArgs e)
        {


        }

        private void label89_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox30_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
