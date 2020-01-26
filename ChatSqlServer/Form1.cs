using System;
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
        Dictionary<string, Image> emojis = new Dictionary<string, Image>();
        public Form1()
        {
            InitializeComponent();
            emojis.Add("smile", Properties.Resources.smile1_30);
            emojis.Add("angry", Properties.Resources.smile2_30);
            emojis.Add("moon", Properties.Resources.smile3_30);
            emojis.Add("monkey", Properties.Resources.smile4_30);
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
            Debug.WriteLine(lastMessagesSize);
            Debug.WriteLine(messages.Count);
            if (!sizeChanged && lastMessagesSize >= messages.Count)
            {
                return;
            }
            lastMessagesSize = messages.Count;
            chatBox.Text = "";
            foreach (Message m in messages)
            {

                string userNameString = " <" + m.user.ToString() + ">\n";
                string messageString = " \t" + m.text + "\n";
                chatBox.AppendText(userNameString);
                chatBox.Select(chatBox.TextLength - userNameString.Length, userNameString.Length - 1);
                chatBox.SelectionFont = new Font(chatBox.Font.FontFamily, averageFontSize, FontStyle.Underline);
                chatBox.SelectionColor = Color.Purple;
                chatBox.AppendText(messageString);
                chatBox.Select(chatBox.TextLength - messageString.Length, messageString.Length - 1);

                chatBox.SelectionFont = new Font(chatBox.Font.FontFamily, averageFontSize, FontStyle.Regular);
                chatBox.SelectionColor = Color.Black;

                if (m.file != null)
                {
                    //вставляю картинку(простых способ без использования Clipboard нет)
                    var b = new Bitmap(new MemoryStream(m.file.data));
                    b = new Bitmap(b, new Size(150, 150));
                    Clipboard.SetImage(b);
                    chatBox.Paste();
                    Clipboard.Clear();
                } 
            }

            chatBox.Select(chatBox.TextLength, 0);
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
                Text += " " + Model.Self.address;
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
                    linkedFileName = null;
                }

                lastMessagesSize = messages.Count;
                //отправляю сообщение + сразу же отрисовываю его, т.к. я уже знаю его содержимое и мне не нужно
                //ждать, пока сервер мне его отправит
                messages.Add(Model.Self.sendMessage(new Message(inputBox.Text, user, linkedFile)));
               
                resizeFont();

                fillTextBox(true);
                inputBox.Text = "";
                inputBox.Focus();
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
            for (int i = 0; i < chatBox.TextLength; i++)
            {
                chatBox.Select(i, 1);
                chatBox.SelectionFont = new Font(chatBox.SelectionFont.FontFamily, fontSize);

            }
            //Debug.WriteLine(fontSize);
            averageFontSize = fontSize;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
        
            resizeFont();
            //fillTextBox(true);
            Debug.WriteLine(chatBox.AutoScrollOffset);
            chatBox.Select(chatBox.TextLength, 0);
            chatBox.ScrollToCaret();
          
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
            
        }
        DateTime lastDrop = DateTime.Now;
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            //у ивента по умолчанию нет никакого ограничения по веремни, так что он срабатывает по несколько раз
            if (!Model.Self.Connect() || lastDrop + new TimeSpan(0, 0, 0, 0, 500) > DateTime.Now) return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //прохожусь по путям файлов, которые перетащил пользователь и для каждого пути отправляю
            //по сообщению
            foreach (string file in files) {
                Debug.WriteLine(file);
                if (File.Exists(file))
                {
                    byte[] blob = File.ReadAllBytes(file);
                    lastMessagesSize = messages.Count;
                    messages.Add(Model.Self.sendMessage(new Message(" ", user, new FileModel(blob, 0))));
                }
            }
            if(files.Length != 0)
            {
                fillTextBox(false);
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

        private void chatBox_Enter(object sender, EventArgs e)
        {
            //поскольку я не могу сделать richtextbox readonly, т.к. для вставки картинок используется
            //clipboard, то мне нужно как-то иначе не дать пользователю вводить сюда данные, так что я просто
            //меняю фокус на другой элемент
            button1.Focus();
        }

        void insertEmoji(RichTextBox rtb, string emojiKey)
        {
            rtb.AppendText($"zzX{emojiKey}Xzz");
        }

        private void sm1_button_Click(object sender, EventArgs e)
        {
            insertEmoji(inputBox, "smile");
        }
    }
}
