using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamAppClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string GET(string Url, string Data)
        {
            WebRequest req = WebRequest.Create(Url + Data);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();

            var obj = JObject.Parse(Out);
            var id = Convert.ToString(obj["steamid"]);
            var img = Convert.ToString(obj["avatarfull"]);
            var communityvisibilitystate = Convert.ToString(obj["communityvisibilitystate"]);
            var personaname = Convert.ToString(obj["personaname"]);
            var personastate = Convert.ToString(obj["personastate"]);
            var realname = Convert.ToString(obj["realname"]);
            var primaryclanid = Convert.ToString(obj["primaryclanid"]);
            var timecreated = Convert.ToString(obj["timecreated"]);
            var personastateflags = Convert.ToString(obj["personastateflags"]);
            var gameextrainfo = Convert.ToString(obj["gameextrainfo"]);
            var lastlogoff = Convert.ToString(obj["lastlogoff"]);
            var CommunityBanned = Convert.ToString(obj["CommunityBanned"]);
            var VACBanned = Convert.ToString(obj["VACBanned"]);
            var NumberOfVACBans = Convert.ToString(obj["NumberOfVACBans"]);
            var DaysSinceLastBan = Convert.ToString(obj["DaysSinceLastBan"]);
            var NumberOfGameBans = Convert.ToString(obj["NumberOfGameBans"]);
            var EconomyBan = Convert.ToString(obj["EconomyBan"]);
            var total_count = Convert.ToInt32(obj["total_count"]);
            var games = Convert.ToInt32(obj["games.name"]);

            DateTime last_log_off = (new DateTime(1970, 1, 1)).AddSeconds(Convert.ToDouble(lastlogoff));
            DateTime time_created = (new DateTime(1970, 1, 1)).AddSeconds(Convert.ToDouble(timecreated));

            if(CommunityBanned == "true")
            {
                CommunityBanned = "Да";
            }
            else
            {
                CommunityBanned = "Нет";
            }

            if (VACBanned == "true")
            {
                VACBanned = "Да";
            }
            else
            {
                VACBanned = "Нет";
            }

            if (EconomyBan == "true")
            {
                EconomyBan = "Да";
            }
            else
            {
                EconomyBan = "Нет";
            }

            switch (Convert.ToInt32(personastate))
            {
                case 0:
                    personastate = "Не в сети";
                    break;
                case 1:
                    personastate = "В сети";
                    break;
                case 2:
                    personastate = "Не беспокоить";
                    break;
                case 3:
                    personastate = "Нет на месте";
                    break;
                case 4:
                    personastate = "Спит";
                    break;
                case 5:
                    personastate = "Хочет обменяться";
                    break;
                case 6:
                    personastate = "Хочет играть";
                    break;
                default:
                    Console.WriteLine("Ошибка");
                    break;
            }

            if (realname == "")
            {
                realname = "Не указано";
            }

            if (communityvisibilitystate == "3")
            {
                communityvisibilitystate = "Публичный";
            }
            else
            {
                communityvisibilitystate = "Приватный";
            }

            if(gameextrainfo == "")
            {
                gameextrainfo = "Нет";
            }

            for (int i = 0; total_count > i; i++)
            {
                listView1.Items.Add(Convert.ToString(total_count));

            }

            label1.Text = Convert.ToString(id);
            label6.Text = Convert.ToString(communityvisibilitystate);
            label7.Text = Convert.ToString(personaname);
            label13.Text = Convert.ToString(realname);
            label9.Text = Convert.ToString(personastate);
            label18.Text = Convert.ToString(gameextrainfo);
            label14.Text = Convert.ToString(time_created);
            label8.Text = Convert.ToString(last_log_off);
            label27.Text = Convert.ToString(CommunityBanned);
            label22.Text = Convert.ToString(VACBanned);
            label26.Text = Convert.ToString(NumberOfVACBans);
            label20.Text = Convert.ToString(DaysSinceLastBan);
            label21.Text = Convert.ToString(NumberOfGameBans);
            label16.Text = Convert.ToString(EconomyBan);
            pictureBox1.ImageLocation = Convert.ToString(img);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
          
            return Out;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Url = "http://project-megaroks931128.codeanyapp.com";
            string His = textBox1.Text;

            if (listBox1.Items.Count < 10)
            {
                listBox1.Items.Add(His);
            }
            else
            {
                listBox1.Items.RemoveAt(0);
                listBox1.Items.Add(His);
            }
           
            string Data = "/news/add/?usersid=" + Convert.ToString(textBox1.Text);
            GET(Url, Data);

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void groupBox2_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string Url = "http://project-megaroks931128.codeanyapp.com";
            string His = listBox1.SelectedItem.ToString();
            textBox1.Text = listBox1.SelectedItem.ToString();

            if (listBox1.Items.Count > 10)
            {
                listBox1.Items.RemoveAt(0);
            }

            string Data = "/news/add/?usersid=" + listBox1.SelectedItem.ToString();
            
            GET(Url, Data);
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
