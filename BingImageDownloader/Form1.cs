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
using System.Xml;

namespace BingImageDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        WebClient downloadimage = new WebClient();


        public string GetImage()
        {
            WebClient wc = new WebClient();
            string resultado = wc.DownloadString("http://www.bing.com/HPImageArchive.aspx?format=xml&idx=0&n=1&mkt=en-US");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(resultado);
            return (@"http://www.bing.com/" + doc.SelectSingleNode(@"/images/image/url").InnerText);
        }

        private void DownloadImage(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Imagem salva com sucesso");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string data = string.Format("BingImage {0:dd-MM-yyyy}", DateTime.Now);

            saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Imagens |*.jpg";
            saveFileDialog1.DefaultExt = ".jpg";
            saveFileDialog1.InitialDirectory = @"C:\Users\Gilberto Correia\Downloads\";

            string nome = saveFileDialog1.FileName = data;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                downloadimage.DownloadFileCompleted += new  AsyncCompletedEventHandler(DownloadImage);
                Uri linkimage = new Uri(GetImage());
                downloadimage.DownloadFileAsync(linkimage, saveFileDialog1.FileName);
            }
        }    
    }
}
