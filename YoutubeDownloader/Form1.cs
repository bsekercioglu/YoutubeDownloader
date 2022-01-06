using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace YoutubeDownloader
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

        private void DownloadVideo(string id)
        {
            bool s = true;
            using (var service = Client.For(YouTube.Default))
            {
                while (s)
                {
                    lblMesaj.Enabled = false;   
                    lblMesaj.Text="Yükleniyor...";

                    var video = service.GetVideo("https://youtube.com/watch?v=" + id);
                    
                    string folder = GetDefaultFolder();

                    string path = Path.Combine(folder, video.FullName);

                   lblMesaj.Text="Kaydediliyor...";

                    File.WriteAllBytes(path, video.GetBytes());

                    lblMesaj.Text = "Tamamlandı";
                    lblMesaj.Enabled = true;
                    s = false;
                }
            }
        }
        static string GetDefaultFolder()
        {
            var home = Environment.GetFolderPath(
                Environment.SpecialFolder.UserProfile);

            return Path.Combine(home, "Downloads");
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            string url = txtUrl.Text.Trim().Replace("www.","").Replace("https://youtube.com/watch?v=", "");

                DownloadVideo(url);
            
        }

        private void lblMesaj_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", GetDefaultFolder());
        }
    }
}
