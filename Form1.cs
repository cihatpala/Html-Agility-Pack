using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Net;
using System.Text.RegularExpressions;

namespace HtmlAgilityPack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        string[] donanimHeader = new string[35];
        string[] donanimAuthor = new string[35];
        string[] donanimContent = new string[35];
        string[] donanimImage = new string[35];
        string[] donanimHaberLink = new string[35];
        int[] donanimFotoId = new int[35];
        public String html;
        public Uri url;
        private void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                //VeriAlDip(donanimHaberLink[i], "//*[@id='res-112646-1']/span/a", "href", listBox5); //Haber Image
                //*[@id="kapsulDiv"]/div[1]/div[1]/div[1]/div[6]/div/a
                VeriAl("https://www.donanimhaber.com/", "//*[@id='kapsulDiv']/div[1]/div[1]/div/div[" + (i + 1) + "]/div/div/a", listBox1); //Haber Header
                
                VeriAl("https://www.donanimhaber.com/", "//*[@id='kapsulDiv']/div[1]/div[1]/div/div[" + (i + 1) + "]/div/div/div[2]", listBox2); //Haber Content
                VeriAl("https://www.donanimhaber.com/", "//*[@id='kapsulDiv']/div[1]/div[1]/div/div[" + (i + 1) + "]/div/div/div[1]", listBox3); //Haber Author
                VeriAlDip("https://www.donanimhaber.com/", "//*[@id='kapsulDiv']/div[1]/div[1]/div/div[" + (i + 1) + "]/div/div/a", "href", listBox4); //Haber Link
                donanimHaberLink[i] = listBox4.Items[i].ToString();
                VeriAlDip("https://www.donanimhaber.com/", "//*[@id='kapsulDiv']/div[1]/div[1]/div[1]/div[" + (i + 1) + "]/div/a", "data-id", listBox5); //Haber Foto id
                donanimFotoId[i] = Convert.ToInt32(listBox5.Items[i]);
                VeriAlDip(donanimHaberLink[i], "//*[@id='res-" + donanimFotoId[i] + "-1']/span/a", "href", listBox6); //Haber Image
                
                
                //string s = WebUtility.HtmlDecode("&eacute;"); // Returns é

            }
            for (int i = 0; i < 2; i++)
            {
                donanimHeader[i] = listBox1.Items[i].ToString();
                donanimHeader[i] = WebUtility.HtmlDecode(donanimHeader[i]);
                listBox1.Items.Add(donanimHeader[i]);

                donanimContent[i] = listBox2.Items[i].ToString();
                donanimContent[i] = WebUtility.HtmlDecode(donanimContent[i]);
                listBox2.Items.Add(donanimContent[i]);

                donanimAuthor[i] = listBox3.Items[i].ToString();
                donanimAuthor[i] = karakterTemizle(WebUtility.HtmlDecode(donanimAuthor[i]));
                listBox3.Items.Add(donanimAuthor[i]);

            }

            
        }
       

        public string karakterTemizle(string gelen)
        {
            string trans = gelen;
            trans = Regex.Replace(trans, "\r\n", String.Empty);
            trans = trans.TrimEnd();
            trans = trans.TrimStart();
            return trans;
        }

        public void VeriAl(String Url, String XPath, ListBox cikanSonuc)
        {
            try
            {
                url = new Uri(Url);
            }
            catch (UriFormatException)
            {
                if (MessageBox.Show("Hatalı Url", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    url = new Uri("https://www.donanimhaber.com");
                }
            }
            catch (ArgumentNullException)
            {
                if (MessageBox.Show("Boş Argüman", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    url = new Uri("https://www.donanimhaber.com");
                }
            }

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            try
            {
                html = client.DownloadString(url);
            }
            catch (WebException)
            {
                if (MessageBox.Show("Url indirilemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    html = client.DownloadString("https://www.donanimhaber.com");
                }
            }

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            try
            {
                cikanSonuc.Items.Add(doc.DocumentNode.SelectSingleNode(XPath).InnerText);
            }
            catch (NullReferenceException)
            {
                if (MessageBox.Show("Hatalı XPath Haber, İçerik, Yazar vs...", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    cikanSonuc.Items.Add("Hatasız Kul Olmaz");
                }
            }

        }
        public void VeriAlDip(String Url, String XPath, String dip, ListBox cikanSonuc)
        {
            try
            {
                url = new Uri(Url);
            }
            catch (UriFormatException)
            {
                if (MessageBox.Show("Hatalı Url", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    url = new Uri("https://www.donanimhaber.com");
                }
            }
            catch (ArgumentNullException)
            {
                if (MessageBox.Show("Boş Argüman", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    url = new Uri("https://www.donanimhaber.com");
                }
            }

            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            try
            {
                html = client.DownloadString(url);
            }
            catch (WebException)
            {
                if (MessageBox.Show("Url indirilemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    html = client.DownloadString(new Uri("https://www.donanimhaber.com"));
                }
            }
            
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(html);
            try
            {
                if (Url == "https://www.donanimhaber.com/")
                {
                    if (dip=="data-id")
                    {
                        try
                        {
                            cikanSonuc.Items.Add(doc.DocumentNode.SelectSingleNode(XPath).Attributes[dip].Value);
                        }
                        catch (Exception)
                        {
                            if (MessageBox.Show("Bozuk veri geliyor", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                            {
                                cikanSonuc.Items.Add(11111);
                            }
                        }
                        
                    }
                    else
                    {
                        cikanSonuc.Items.Add("https://www.donanimhaber.com" + doc.DocumentNode.SelectSingleNode(XPath).Attributes[dip].Value);
                    }
                }
                else if(dip=="href" || Url != "https://www.donanimhaber.com/")
                {
                    //cikanSonuc.Items.Add(doc.DocumentNode.SelectSingleNode(XPath).Attributes[dip].Value);
                    try
                    {
                        cikanSonuc.Items.Add(doc.DocumentNode.SelectSingleNode(XPath).Attributes[dip].Value);
                    }
                    catch (Exception)
                    {
                        if (MessageBox.Show("Bu linkten fotoğraf çekilemiyor!!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                        {
                            cikanSonuc.Items.Add("https://www.ekol.com/wp-content/uploads/2015/07/ekol-logotype.png");
                        }
                    }
                }
                
            }
            catch (NullReferenceException)
            {
                if (MessageBox.Show("Hatalı XPath Foto ve link", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                {
                    cikanSonuc.Items.Add("Hatasız Kul Olmaz");
                }
            }

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }
    }
}