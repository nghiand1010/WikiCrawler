using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WikiCrawler
{
    public partial class Form1 : Form
    {

        BackgroundWorker bw;
        public Form1()
        {
            InitializeComponent();
            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += Bw_DoWork;
            // bw.ProgressChanged += worker_ProgressChanged;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           // throw new NotImplementedException();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            string path = Environment.ExpandEnvironmentVariables("%LOCALAPPDATA%\\Google\\Chrome\\User Data\\X");

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("user-data-dir=" + path);
            options.AddArguments("--start-maximized");
            options.AddArguments("--disable-extensions");
            options.AddArguments("-no-sandbox");
            //if (chkHeadless.Checked)
            //{
            //    options.AddArguments("--headless");
            //}

            options.AddArgument("ignore-certificate-errors");

            using (ChromeDriver driver = new ChromeDriver(options))
            {
                var driverHelper = new ChromeDriverHelper(driver);
                driverHelper.ChromeDriver.Navigate().GoToUrl(txtUrl.Text);

                var categoryEls = driverHelper.FindElementsByCssSelector("#mw-pages .mw-content-ltr li a");
                List<string> links = new List<string>();
                foreach (var item in categoryEls)
                {
                    var link=item.GetAttribute("href");
                    links.Add(link);
                }

                foreach (var item in links)
                {
                    driverHelper.ChromeDriver.Navigate().GoToUrl(item);
                    var header = driverHelper.FindElementById("firstHeading").Text;
                    //Lấy tạm text có thể lấy HTML tùy
                    var content = driverHelper.FindElementById("bodyContent").Text;
                    SavePost(new Post { Title = header, Content = content });
                }

            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            bw.RunWorkerAsync();
        }

        List<Post> posts = new List<Post>();

        /// <summary>
        /// ĐOạn này demo cho save thích save vào mongo thì code vào đây
        /// </summary>
        /// <param name="post"></param>
        private void SavePost(Post post)
        {
            posts.Add(post);
        }
    }
}
