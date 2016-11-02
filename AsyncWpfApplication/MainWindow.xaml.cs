using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System;
using System.Net.Http;

namespace AsyncWpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region multi thread
        private async void startButton_Click_1(object sender, RoutedEventArgs e)
        {
            resultsTextBox.Clear();
            await SumPageSizesAsync();
            resultsTextBox.Text += "\r\nControl returned to startButton_Click.";
        }

        private async Task SumPageSizesAsync()
        {
            IEnumerable<string> urlList = SetUpURLList();

            var total = 0;

            foreach (var url in urlList)
            {
                Task<byte[]> getContentsTask = GetURLContentsAsync(url);

                byte[] urlContents = await getContentsTask;

                DisplayResults(url, urlContents);

                total += urlContents.Length;
            }

            resultsTextBox.Text += string.Format("\r\n\r\nTotal bytes returned:  {0} \r\n", total);
        }

        private IEnumerable<string> SetUpURLList()
        {
            var urls = new List<string>
            {
                "http://google.com",
                "http://google.com.vn",
                "http://facebook.com",
                "http://24h.com.vn",
                "http://gmail.com",
                "http://yahoo.com"
            };
            return urls;
        }

        public async Task<byte[]> GetURLContentsAsync(string url)
        {
            var content = new MemoryStream();

            var webReq = (HttpWebRequest)WebRequest.Create(url);

            Task<WebResponse> responseTask = webReq.GetResponseAsync();

            using (WebResponse response = await responseTask)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    Task copyTask = responseStream.CopyToAsync(content);

                    await copyTask;
                }
            }

            return content.ToArray();
        }

        public void DisplayResults(string url, byte[] content)
        {
            var bytes = content.Length;

            var displayURL = url.Replace("http://&quot;", "");
            resultsTextBox.Text += string.Format("\n{0,-58} {1,8}", displayURL, bytes);
        }
        #endregion

        //#region single thread
        //private void startButton_Click_1(object sender, RoutedEventArgs e)
        //{
        //    resultsTextBox.Clear();
        //    SumPageSizes();
        //    resultsTextBox.Text += "\r\nControl returned to startButton_Click.";
        //}

        //private void SumPageSizes()
        //{
        //    // Make a list of web addresses.
        //    IEnumerable<string> urlList = SetUpURLList();

        //    var total = 0;
        //    foreach (var url in urlList)
        //    {
        //        // GetURLContents returns the contents of url as a byte array.
        //        byte[] urlContents = GetURLContents(url);

        //        DisplayResults(url, urlContents);

        //        // Update the total.
        //        total += urlContents.Length;
        //    }

        //    // Display the total count for all of the web addresses.
        //    resultsTextBox.Text += string.Format("\r\n\r\nTotal bytes returned:  {0}\r\n", total);
        //}


        //private IEnumerable<string> SetUpURLList()
        //{
        //    var urls = new List<string>
        //    {
        //    "http://google.com",
        //    "http://google.com.vn",
        //    "http://facebook.com",
        //    "http://24h.com.vn"
        //    };
        //    return urls;
        //}


        //private byte[] GetURLContents(string url)
        //{
        //    // The downloaded resource ends up in the variable named content.
        //    var content = new MemoryStream();

        //    // Initialize an HttpWebRequest for the current URL.
        //    var webReq = (HttpWebRequest)WebRequest.Create(url);

        //    // Send the request to the Internet resource and wait for
        //    // the response.
        //    using (var response = webReq.GetResponse())
        //    {
        //        // Get the data stream that is associated with the specified URL.
        //        using (Stream responseStream = response.GetResponseStream())
        //        {
        //            // Read the bytes in responseStream and copy them to content. 
        //            responseStream.CopyTo(content);
        //        }
        //    }

        //    // Return the result as a byte array.
        //    return content.ToArray();
        //}

        //private void DisplayResults(string url, byte[] content)
        //{
        //    // Display the length of each website. The string format
        //    // is designed to be used with a monospaced font, such as
        //    // Lucida Console or Global Monospace.
        //    var bytes = content.Length;
        //    // Strip off the "http://&quot;.
        //    var displayURL = url.Replace("http://&quot;", "");
        //    resultsTextBox.Text += string.Format("\n{0,-58} {1,8}", displayURL, bytes);
        //}
        //#endregion
    }
}
