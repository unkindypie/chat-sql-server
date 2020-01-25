﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace ChatSqlServer
{
    public partial class Form1 : Form
    {
        User user;
        int lastMessagesSize = 0;
        float averageFontSize = 8;
        string linkedFileName = null;
        List<Message> messages = new List<Message>();
        public Form1()
        {
            InitializeComponent();
        }

        public Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }
        void fillTextBox(bool sizeChanged)
        { 
            //если размер сообщений остался прежним с последнего вызова, то можно ничего не рисовать
            //но только в том случае, если этот метод был вызван не для масштабирования текста
            if (!sizeChanged && lastMessagesSize == messages.Count)
            {
                return;
            }
            chatBox.Text = "";
            foreach (Message m in messages)
            {
               
                string userNameString = " <" + m.user.ToString() + ">\n";
                string messageString = " \t" + m.text + "\n";
                chatBox.AppendText(userNameString);
                chatBox.Select(chatBox.TextLength - userNameString.Length, userNameString.Length - 1);
                chatBox.SelectionFont = new Font(chatBox.Font.FontFamily, averageFontSize + 3, FontStyle.Underline);
                chatBox.SelectionColor = Color.Purple;
                chatBox.AppendText(messageString);
                chatBox.Select(chatBox.TextLength - messageString.Length, messageString.Length - 1);

                chatBox.SelectionFont = new Font(chatBox.Font.FontFamily, averageFontSize + 1, FontStyle.Regular);
                chatBox.SelectionColor = Color.Black;

                if (m.file != null)
                {
                    Debug.WriteLine(m.file.data[0]);
                    Image image = byteArrayToImage(m.file.data);
                    Clipboard.SetImage(image);
                    chatBox.Paste(DataFormats.GetFormat(DataFormats.Bitmap));
                } 
            }

            chatBox.Select(chatBox.TextLength, 1);
            chatBox.ScrollToCaret();

        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            Show();
            var loginForm = new Login();
            var result = loginForm.ShowDialog();
            //если окно логина закрыли, то и основное окно работать не должно
            if(result == DialogResult.Cancel || loginForm.user == null)
            {
                Close();
                return;
            }
            MessageBox.Show("Logined as " + loginForm.user.name);
            user = loginForm.user;
            if (user == null) Close();
            if (!Model.Self.Connect())
            {
                MessageBox.Show("No connection to server!");
                Close();
            }
            else
            {
                //получаю сообщения с сервера в первый раз
                messages = await Model.Self.getMessages(null);
                resizeFont();
                fillTextBox(true);
                
                

                timer1.Tick += async (object e_, EventArgs e__) =>
                {
                    lastMessagesSize = messages.Count;
                    //в таймере я запрашиваю с сервера только те сообщения, которых ранее не было
                    List<Message> newMessages = await Model.Self.getMessages(messages[messages.Count - 1]);
                    Debug.WriteLine(newMessages.Count);
                    //если новых сообщений не пришло, то можно не перерисовывать ничего
                    if (newMessages.Count == 1) return;

                    messages.AddRange(newMessages);
                    fillTextBox(false);
                };
                timer1.Start();

            }
    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Model.Self.Connect())
            {
                FileModel linkedFile = null;
                //если был прикреплен файл( с помощью скрепки ), то добавляю его в сообщение
                if (linkedFileName != null && File.Exists(linkedFileName))
                {
                    byte[] blob = File.ReadAllBytes(linkedFileName);
                    linkedFile = new FileModel(blob, 0);
                }

                lastMessagesSize = messages.Count;
                //отправляю сообщение + сразу же отрисовываю его, т.к. я уже знаю его содержимое и мне не нужно
                //ждать, пока сервер мне его отправит
                messages.Add(Model.Self.sendMessage(new Message(textBox2.Text, user, linkedFile)));
               
                resizeFont();

                fillTextBox(true);
                textBox2.Text = "";
                textBox2.Focus();
            }
        }

        void resizeFont()
        {
            
            //масштабирую шрифт
            float fontSize = (float)(Width * (Height * 0.8)) * (float)0.00005;
            //ограничение размеров
            if(fontSize < 12)
            {
                fontSize = 12;
            }
            else if ( fontSize > 25)
            {
                fontSize = 25;
            }

            Debug.WriteLine(fontSize);
            averageFontSize = fontSize;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
        
            resizeFont();
            fillTextBox(true);
            chatBox.Select(chatBox.TextLength, 1);
            chatBox.ScrollToCaret();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (!Model.Self.Connect()) return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //прохожусь по путям файлов, которые перетащил пользователь и для каждого пути отправляю
            //по сообщению
            foreach (string file in files) {
                Debug.WriteLine(file);
                if (File.Exists(file))
                {
                    byte[] blob = File.ReadAllBytes(file);
                    Model.Self.sendMessage(new Message(" ", user, new FileModel(blob, 0)));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog();
            var res = fd.ShowDialog();
            if(res == DialogResult.OK)
            {
                linkedFileName = fd.FileName;
            }
        }
    }
}
