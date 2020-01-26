using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace ChatSqlServer
{
    class Model
    {
        SqlConnection connection = new SqlConnection();
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        public string address = "";
        static Model self = null;
        protected Model()
        {
            //builder.DataSource = @"DESKTOP-6DIF51U\SQL_S2";
            builder.DataSource = @"DESKTOP-CJ4FN0M";
            builder.InitialCatalog = @"chat";
            builder.UserID = @"sa";
            builder.Password = @"baddev02";
            connection.ConnectionString = builder.ConnectionString;
            address = builder.DataSource.ToString();
        }
        public static Model Self
        {
            get
            {
                if (self == null)
                {
                    self = new Model();
                }
                return self;
            }
        }
        public async Task<List<Message>> getMessages(Message theLastOne)
        {
            //theLastOne - последнее собщение,если оно есть, то запрашиваются только сообщения после него
            SqlCommand cmd;
            if(theLastOne == null)
            {
                cmd = new SqlCommand($"select messages.id as id, text, userId, time, name, data, imgId from messages join users on users.id = messages.userId full join images on images.id = imgId", connection);
            }
            else
            {
                cmd = new SqlCommand($"select messages.id as id, text, userId, time, name, data, imgId from messages join users on users.id = messages.userId full join images on images.id = imgId where time > '{theLastOne.time}'", connection);
            }
         
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            var table = new DataTable();
            table.Load(reader);
            var rows = table.Rows;
            var messages = new List<Message>();
            for (int i = 0; i < rows.Count; i++)
            {
                var m = new Message();
                var u = new User();
             
                m.id = int.Parse(rows[i]["id"].ToString());
                
                u.id = int.Parse(rows[i]["userId"].ToString());
                u.name = rows[i]["name"].ToString();
                m.text = rows[i]["text"].ToString();
                m.time = rows[i]["time"].ToString();
                try
                {
                    m.file = new FileModel((byte[])rows[i]["data"], int.Parse(rows[i]["imgId"].ToString()));
                }
                catch { }
             

                m.user = u;
                messages.Add(m);
            }
            return messages;
        }
        public User login(string username)
        {
            try
            {
            
                SqlCommand cmd = new SqlCommand($"select * from users where name = '{username}'", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                var table = new DataTable();

                table.Load(reader);
                var rows = table.Rows;

                if (rows.Count == 0)
                {

                    cmd = new SqlCommand($"insert into users values ('{username}')", connection);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand($"select * from users where name = '{username}'", connection);
                    reader = cmd.ExecuteReader();
                    table = new DataTable();

                    table.Load(reader);
                    rows = table.Rows;
                }

                var user = new User();
                user.name = rows[0]["name"].ToString();
                user.id =  int.Parse(rows[0]["id"].ToString());

                return user;
            } catch(Exception e)
            {
                
                MessageBox.Show(e.ToString());
            }
            return null;

        }
        public Message sendMessage(Message m)
        {
            try
            {
                int imgId = -1;
                if (m.file != null)
                {
                    SqlCommand cmd_ = new SqlCommand($"insert into images values (@File)", connection);
                    cmd_.Parameters.AddWithValue("@File", SqlDbType.VarBinary).Value = m.file.data;
                    cmd_.ExecuteNonQuery();
                    cmd_ = new SqlCommand($"select max(id) from images", connection);
                    imgId = int.Parse(cmd_.ExecuteScalar().ToString());
                }
                SqlCommand cmd = new SqlCommand($"insert into messages values ('{m.text}', getdate(), {m.user.id}, {(imgId != -1 ? imgId.ToString() : "null")})", connection);
                cmd.ExecuteNonQuery();

                if(m.file != null)
                {
                    cmd = new SqlCommand($"select messages.id as id, text, userId, time, name, data, imgId from messages join images on messages.imgId = images.id"
                    + " join users on users.id = messages.userId where messages.id = (select max(messages.id) from messages)", connection);
                }
                else
                {
                    cmd = new SqlCommand($"select messages.id as id, text, userId, time, name from messages "
                   + "join users on users.id = messages.userId where messages.id = (select max(messages.id) from messages)", connection);
                }
                SqlDataReader reader = cmd.ExecuteReader();
                var table = new DataTable();
                table.Load(reader);
                var rows = table.Rows;

                var m2 = new Message();
                var u = new User();
                m2.id = int.Parse(rows[0]["id"].ToString());

                u.id = int.Parse(rows[0]["userId"].ToString());
                u.name = rows[0]["name"].ToString();
                m2.text = rows[0]["text"].ToString();
                m2.time = rows[0]["time"].ToString();
                m2.user = u;

                try
                {
                    if (rows[0]["data"] != null)
                    {
                        m2.file = new FileModel((byte[])rows[0]["data"], int.Parse(rows[0]["imgId"].ToString()));
                    }
                }
                catch { };
          

                return m2;
  

            }
            catch (Exception e)
            {

                MessageBox.Show(e.ToString());
            }
            return null;
        }
        public bool Connect()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            if (connection.State != System.Data.ConnectionState.Open)
            {
                return false;
            }
            return true;

        }

        public bool Disconect()
        {
            try
            {
                connection.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
