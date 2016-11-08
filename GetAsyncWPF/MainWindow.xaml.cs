using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GetAsyncWPF
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

        public void getClick(object sender, EventArgs e)
        {
            textbox.Clear();
            try
            {
                using (var client = new HttpClient())
                {
                    var response = client.GetAsync("http://google.com.vn").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        // by calling .Result you are performing a synchronous call
                        var responseContent = response.Content;

                        // by calling .Result you are synchronously reading the result
                        string responseString = responseContent.ReadAsStringAsync().Result;
                        if (!string.IsNullOrEmpty(responseString))
                        {
                            textbox.Text = string.Format("{0} \n\n GetAsync complete!", responseString);
                        }
                        else
                        {
                            textbox.Text = ("GetAsync fail!");
                        }

                    }
                }
            }
            catch (Exception g)
            {
                Console.WriteLine(g);
            }

        }

        public void postClick(object sender, EventArgs e)
        {
            textbox.Clear();
            try
            {
                using(var client = new HttpClient())
                {
                    var stringContent = new StringContent("Kien");
                    var response = client.PostAsync("http://localhost", stringContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        textbox.Text = (response.ReasonPhrase + "\n Success!");
                    }
                    else
                    {
                        textbox.Text = (response.ReasonPhrase + "\n Fail!");
                    }
                }
            }
            catch(Exception g)
            {
                Console.WriteLine(g);
            }
        }

        public void putClick(object sender, EventArgs e)
        {
            textbox.Clear();
            try
            {

            }
            catch (Exception g)
            {
                Console.WriteLine(g);
            }
        }
    }
}
