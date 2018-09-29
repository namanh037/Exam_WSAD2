using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Data.Json;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WSADIIExam
{
    public class Person
    {
        public string Name { get; set; }
        public string PermissionToCall { get; set; }
        public List<PhoneNumber> PhoneNum { get; set; }

        public class PhoneNumber
        {
            public string Location { get; set; }
            public string Number { get; set; }
        }

       
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // make the json object
            var ps = new Person()
            {
                Name = yourName.Text,
                PermissionToCall = mycheck.IsChecked.ToString(),
                PhoneNum = new List<Person.PhoneNumber>()
                {
                    new Person.PhoneNumber(){ Location = "Home", Number = yourHomephone.Text},
                    new Person.PhoneNumber(){Location="Work",Number = yourWorkePhone.Text}
                }
            };

            var json = JsonConvert.SerializeObject(ps);

            // read number which location is home ( reading back the json)

            string homephonenumber = "";
            var data = JsonConvert.DeserializeObject<Person>(json);
            List<Person.PhoneNumber> number = data.PhoneNum;
            foreach ( Person.PhoneNumber p in number)
            {
                if (p.Location == "Home")
                {
                    homephonenumber = p.Number;
                }
            }
            // make the toast noti

            ToastTemplateType toastTem = ToastTemplateType.ToastText01;
            XmlDocument toastxml = ToastNotificationManager.GetTemplateContent(toastTem);

            XmlNodeList toastTextEle = toastxml.GetElementsByTagName("text");

            //dispaly json data
            toastTextEle[0].AppendChild(toastxml.CreateTextNode(json));

            // display homephone number
            //toastTextEle[1].AppendChild(toastxml.CreateTextNode(homephonenumber));

            // display toast
            ToastNotification toast = new ToastNotification(toastxml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);

        }
            
        
    }
}

