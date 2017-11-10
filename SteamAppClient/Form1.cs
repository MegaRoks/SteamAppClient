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

            DateTime pDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0)).AddSeconds(Convert.ToDouble(timecreated));
            
            label1.Text = Convert.ToString(id);
            label6.Text = Convert.ToString(communityvisibilitystate);
            label7.Text = Convert.ToString(personaname);
            label13.Text = Convert.ToString(realname);
            label9.Text = Convert.ToString(personastate);
            label18.Text = Convert.ToString(primaryclanid);
            label14.Text = Convert.ToString(pDate);
            label8.Text = Convert.ToString(personastateflags);
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

            string Data = "/news/add/?usersid=" + listBox1.SelectedItem.ToString();
            
            GET(Url, Data);
        }
    }
}
