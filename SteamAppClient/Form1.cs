using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace SteamAppClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class Game
        {
            public int Appid { get; set; }
            public string Name { get; set; }
            public int Playtime_2weeks { get; set; }
            public int Playtime_forever { get; set; }
            public string Img_icon_url { get; set; }
            public string Img_logo_url { get; set; }
        }

        public class Friend
        {
            public string Steamid { get; set; }
            public string Relationship { get; set; }
            public int Friend_since { get; set; }
        }

        public class FriendsList
        {
            public List<Friend> Friends { get; set; }
            public int Friendslist { get; set; }
        }

        public class GamesList
        {
            public List<Game> Games { get; set; }
        }

        public static string RequestJson(string Url)
        {
            WebRequest myRequest = WebRequest.Create(Url);
            WebResponse myResponse = myRequest.GetResponse();
            Stream stream = myResponse.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string json = sr.ReadToEnd();
            sr.Close();
            return json;
        }

        public static Bitmap RequestJsonImage(string Url)
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(Url.ToString());
            myRequest.Method = "GET";
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            Bitmap bmp = new Bitmap(myResponse.GetResponseStream());
            myResponse.Close();
            return bmp;
        }

        public void ResponseJson(string json)
        {
            var obj = JObject.Parse(json);
            var IdSteam = Convert.ToString(obj["steamid"]);
            var Avatar = Convert.ToString(obj["avatarfull"]);
            var Communityvisibilitystate = Convert.ToString(obj["communityvisibilitystate"]);
            var Personaname = Convert.ToString(obj["personaname"]);
            var Personastate = Convert.ToString(obj["ersonastate"]);
            var Realname = Convert.ToString(obj["realname"]);
            var Primaryclanid = Convert.ToString(obj["primaryclanid"]);
            var Timecreated = Convert.ToString(obj["timecreated"]);
            var Personastateflags = Convert.ToString(obj["personastateflags"]);
            var Gameextrainfo = Convert.ToString(obj["gameextrainfo"]);
            var Lastlogoff = Convert.ToString(obj["lastlogoff"]);
            var CommunityBanned = Convert.ToString(obj["CommunityBanned"]);
            var VACBanned = Convert.ToString(obj["VACBanned"]);
            var NumberOfVACBans = Convert.ToString(obj["NumberOfVACBans"]);
            var DaysSinceLastBan = Convert.ToString(obj["DaysSinceLastBan"]);
            var NumberOfGameBans = Convert.ToString(obj["NumberOfGameBans"]);
            var EconomyBan = Convert.ToString(obj["EconomyBan"]);
            var Id = Convert.ToString(obj["id"]);
            var Total_count = Convert.ToInt32(obj["total_count"]);
            var friendslist = Convert.ToInt32(obj["friendslist"]);

            DateTime last_log_off = (new DateTime(1970, 1, 1)).AddSeconds(Convert.ToDouble(Lastlogoff));
            if (Timecreated != "")
            {
                DateTime time_created = (new DateTime(1970, 1, 1)).AddSeconds(Convert.ToDouble(Timecreated));
                label14.Text = Convert.ToString(time_created);
            }

            label1.Text = Convert.ToString(IdSteam);
            label6.Text = Convert.ToString(Communityvisibilitystate);
            label7.Text = Convert.ToString(Personaname);
            label13.Text = Convert.ToString(Realname);
            label9.Text = Convert.ToString(Personastate);
            label18.Text = Convert.ToString(Gameextrainfo);
            label8.Text = Convert.ToString(last_log_off);
            label27.Text = Convert.ToString(CommunityBanned);
            label22.Text = Convert.ToString(VACBanned);
            label26.Text = Convert.ToString(NumberOfVACBans);
            label20.Text = Convert.ToString(DaysSinceLastBan);
            label21.Text = Convert.ToString(NumberOfGameBans);
            label16.Text = Convert.ToString(EconomyBan);

            pictureBox1.ImageLocation = Convert.ToString(Avatar);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            GamesList game = JsonConvert.DeserializeObject<GamesList>(json);
            DataGridView2(game);

            FriendsList friends = JsonConvert.DeserializeObject<FriendsList>(json);
            DataGridView(friends);

        }
        public void His(string Data, string json)
        {
            var obj = JObject.Parse(json);
            var Id = Convert.ToString(obj["id"]);
                listView1.View = View.Details;
                listView1.Columns.Add("ID", 115);
                listView1.Columns.Add("ID в бд", 0);

                ListViewItem newitem = new ListViewItem(Data);
                newitem.SubItems.Add(Id);
                listView1.Items.Add(newitem);         
        }

        private void DataGridView2(GamesList games)
        {
            DataTable t = new DataTable();
            t.Columns.Add(new DataColumn("Logo", typeof(Bitmap)));
            t.Columns.Add("Название");
            t.Columns.Add("За неделю");
            t.Columns.Add("Всего");


            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.AutoResizeColumns();

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            try
            {
                for (int i = 0; games.Games.Count > i; i++)
                {
                    string Url = "http://media.steampowered.com/steamcommunity/public/images/apps/" + games.Games[i].Appid + "/" + games.Games[i].Img_logo_url + ".jpg".ToString();
                    Bitmap bmp = RequestJsonImage(Url);
                    int Playtime_2weeks = Convert.ToInt16(Math.Ceiling(TimeSpan.FromSeconds(games.Games[i].Playtime_2weeks).TotalMinutes));
                    int Playtime_forever = Convert.ToInt16(Math.Ceiling(TimeSpan.FromSeconds(games.Games[i].Playtime_forever).TotalMinutes));

                    t.Rows.Add(new object[] { bmp, games.Games[i].Name, Playtime_2weeks, Playtime_forever });
                    dataGridView1.DataSource = t;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Список игр пуст");
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
            }
        }

        private void DataGridView(FriendsList friends)
        {
            DataTable t = new DataTable();
            t.Columns.Add(new DataColumn("Аватар", typeof(Bitmap)));
            t.Columns.Add("Nike");
            t.Columns.Add("Steam ID");
            t.Columns.Add("Срок дружбы");
            t.Columns.Add("Друзья");

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.AutoResizeColumns();

            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);

            try
            {
                if (friends.Friends.Count != 0)
                {
                    for (int i = 0; friends.Friends.Count > i; i++)
                    {
                        string Url = "http://project-megaroks931128.codeanyapp.com/news/add/?usersid=" + friends.Friends[i].Steamid;
                        string json = RequestJson(Url);
                        var obj1 = JObject.Parse(json);
                        var avatar = Convert.ToString(obj1["avatar"]);
                        var personaname = Convert.ToString(obj1["personaname"]);
                        var steamid = Convert.ToString(obj1["steamid"]);
                        var id = Convert.ToString(obj1["id"]);
                        Bitmap bmp = RequestJsonImage(avatar);
                        DateTime Friend_since = (new DateTime(1970, 1, 1)).AddSeconds(Convert.ToDouble(friends.Friends[i].Friend_since));
                        t.Rows.Add(new object[] { bmp, personaname, steamid, Friend_since, friends.Friends[i].Relationship });
                        dataGridView2.DataSource = t;
                        Url = "http://project-megaroks931128.codeanyapp.com/news/del/?id=" + id;
                        json = RequestJson(Url);
                    }
                }
                else
                {
                    MessageBox.Show("Список друзей пуст");
                    dataGridView2.DataSource = null;
                    dataGridView2.Rows.Clear();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Список друзей скрыт");
                dataGridView2.DataSource = null;
                dataGridView2.Rows.Clear();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string Data = Convert.ToString(textBox1.Text);
                long s = Convert.ToInt64(textBox1.Text); 
                string Url = "http://project-megaroks931128.codeanyapp.com/news/add/?usersid=" + Data;
                string json = RequestJson(Url);
                ResponseJson(json);
                His(Data, json);
                textBox1.Clear();
            }
            catch (System.FormatException)
            {
                MessageBox.Show("Вы ввели символ! Пожалуйста,введите цифрy");
                textBox1.Clear();
            }           
        }

        private void ListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string Url = "http://project-megaroks931128.codeanyapp.com/news/add/?usersid=" + listView1.SelectedItems[0].Text;
            string json = RequestJson(Url);
            ResponseJson(json);
        }

        private void DataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string Data = Convert.ToString(dataGridView2.CurrentRow.Cells[2].Value);
            string Url = "http://project-megaroks931128.codeanyapp.com/news/add/?usersid=" + Data;
            string json = RequestJson(Url);
            ResponseJson(json);
            His(Data, json);
        }

        private void УдалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection collection = listView1.SelectedIndices;
            if (collection.Count != 0)
            {
                string Url = "http://project-megaroks931128.codeanyapp.com/news/del/?id=" + listView1.SelectedItems[0].SubItems[1].Text;
                listView1.Items.RemoveAt(collection[0]);
                RequestJson(Url);
            }
        }

        private void ОчиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }
    }
}