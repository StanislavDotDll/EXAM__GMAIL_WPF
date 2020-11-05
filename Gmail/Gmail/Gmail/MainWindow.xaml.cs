using System;
using System.Windows;
using System.Net.Mail;
using System.Net;
using Microsoft.Win32;
using System.IO;

namespace Gmail
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public string filePath { get; set; }


        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            LogInForm.Visibility = Visibility.Hidden;
            MainForm.Visibility = Visibility.Visible;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(usersEmai.Text, userPassword.Password),
                EnableSsl = true
            };
            MailMessage message = new MailMessage();
            message.From = new MailAddress(usersEmai.Text);
            message.To.Add(recipient.Text);
            message.Subject = headlin.Text;
            message.Body = Message.Text;
            if (filePath != null)
            {
                Attachment attachment;
                attachment = new Attachment(filePath);
                message.Attachments.Add(attachment);
            }

            headlin.Text = filePath;

            client.Send(message);

            Letters.Items.Add("Я: " + headlin.Text + " " + Message.Text);
            headlin.Text = "";
            Message.Text = "";
            recipient.Text = "";
            filePath = null;
            SendForm.Visibility = Visibility.Hidden;
            MainForm.Visibility = Visibility.Visible;

        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SendForm.Visibility = Visibility.Visible;
            MainForm.Visibility = Visibility.Hidden;
        }
    }


}


