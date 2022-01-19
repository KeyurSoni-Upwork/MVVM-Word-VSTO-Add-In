using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using Dapper;
using MemoForms.Models;
using MemoForms.ViewModel;

namespace MemoForms.DBAccess
{
    public class MemoDBAccess
    {
        public void InsertMemo(MemoViewModel objMemoModel)
        {
            string dbPath = "D:\\Peace's\\UpWork\\MVVM UI WCF\\MemoDB.db";
            string databaseSourcePath = string.Format("Data Source={0}", dbPath);

            using (SQLiteConnection connection = new SQLiteConnection(databaseSourcePath))
            {
                if(connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                StringBuilder query = new StringBuilder();
                query.Append("INSERT INTO MEMO (File_ID, Section_No, Paragraph_Text, Original_Text, Content, Memo) VALUES (@File_ID,@Section_No,@Paragraph_Text,@Original_Text,@Content,@Memo)");

                connection.Query<MemoViewModel>(query.ToString(), 
                    new { File_ID = objMemoModel.TxtBoxFile_id,
                        Section_No = objMemoModel.SectionNumber,
                        Paragraph_Text = objMemoModel.ParagraphText,
                        Original_Text = objMemoModel.OriginalSelectedText,
                        Content = objMemoModel.TxtBoxContent,
                        Memo = objMemoModel.TxtBoxMemoText
                    });

                //var command = new SQLiteCommand(query.ToString());
                //command.Parameters.Add(new SQLiteParameter("@File_ID", objMemoModel.File_id));
                //command.Parameters.Add(new SQLiteParameter("@Section_No", objMemoModel.SectionNumber));
                //command.Parameters.Add(new SQLiteParameter("@Paragraph_Text", objMemoModel.Paragraphtext));
                //command.Parameters.Add(new SQLiteParameter("@Original_Text", objMemoModel.OriginalSelectedText));
                //command.Parameters.Add(new SQLiteParameter("@Content", objMemoModel.Content));
                //command.Parameters.Add(new SQLiteParameter("@Memo", objMemoModel.MemoText));
                //command.ExecuteNonQuery();
            }
        }

        public List<MemoViewModel> SelectMemo()
        {
            string dbPath = "D:\\Peace's\\UpWork\\MVVM UI WCF\\MemoDB.db";
            string databaseSourcePath = string.Format("Data Source={0}", dbPath);
            List<MemoViewModel> lstReadMemo = new List<MemoViewModel>();

            using (SQLiteConnection connection = new SQLiteConnection(databaseSourcePath))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                StringBuilder query = new StringBuilder();
                query.Append("SELECT * FROM MEMO");

                SQLiteCommand command = new SQLiteCommand(query.ToString());
                SQLiteDataReader sqlReader = command.ExecuteReader();

                lstReadMemo = connection.Query<MemoViewModel>(query.ToString()).ToList();

                //while (sqlReader.Read())
                //{
                //    MemoViewModel objMemo = new MemoViewModel();
                //    objMemo.TxtBoxFile_id = (int)sqlReader["File_ID"];
                //    objMemo.SectionNumber = (string)sqlReader["Section_No"];
                //    objMemo.ParagraphText = (string)sqlReader["Paragraph_Text"];
                //    objMemo.OriginalSelectedText = (string)sqlReader["Original_Text"];
                //    objMemo.TxtBoxContent= (string)sqlReader["Content"];
                //    objMemo.TxtBoxMemoText = (string)sqlReader["Memo"];

                //    lstReadMemo.Add(objMemo);
                //}
            }

            return lstReadMemo;
        }

        public void UpdateMemo(MemoViewModel objMemoModel)
        {
            string dbPath = "D:\\Peace's\\UpWork\\MVVM UI WCF\\MemoDB.db";
            string databaseSourcePath = string.Format("Data Source={0}", dbPath);

            using (SQLiteConnection connection = new SQLiteConnection(databaseSourcePath))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var query = new StringBuilder();
                query.Append("UPDATE MEMO SET Section_No = @Section_No, Paragraph_Text = @Paragraph_Text, Original_Text = @Original_Text, Content = @Content, Memo = @Memo WHERE File_ID = @File_ID");

                 connection.Query<MemoViewModel>(query.ToString(),
                    new
                    {
                        File_ID = objMemoModel.TxtBoxFile_id,
                        Section_No = objMemoModel.SectionNumber,
                        Paragraph_Text = objMemoModel.ParagraphText,
                        Original_Text = objMemoModel.OriginalSelectedText,
                        Content = objMemoModel.TxtBoxContent,
                        Memo = objMemoModel.TxtBoxMemoText
                    });

                //var command = new SQLiteCommand(query.ToString());
                //command.Parameters.Add(new SQLiteParameter("@File_ID", objMemoModel.TxtBoxFile_id));
                //command.Parameters.Add(new SQLiteParameter("@Section_No", objMemoModel.SectionNumber));
                //command.Parameters.Add(new SQLiteParameter("@Paragraph_Text", objMemoModel.ParagraphText));
                //command.Parameters.Add(new SQLiteParameter("@Original_Text", objMemoModel.OriginalSelectedText));
                //command.Parameters.Add(new SQLiteParameter("@Content", objMemoModel.TxtBoxContent));
                //command.Parameters.Add(new SQLiteParameter("@Memo", objMemoModel.TxtBoxMemoText));
                //command.ExecuteNonQuery();
            }
        }

        public void DeleteMemo(MemoViewModel objMemo)
        {
            string dbPath = "D:\\Peace's\\UpWork\\MVVM UI WCF\\MemoDB.db";
            string databaseSourcePath = string.Format("Data Source={0}", dbPath);

            using (SQLiteConnection connection = new SQLiteConnection(databaseSourcePath))
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }

                var query = new StringBuilder();
                query.Append("DELETE MEMO WHERE File_ID = @File_ID");
                connection.Query<Memo>(query.ToString(), new { File_ID = objMemo.TxtBoxFile_id});

                //var command = new SQLiteCommand(query.ToString());
                //command.Parameters.Add(new SQLiteParameter("@File_ID", objMemo.TxtBoxFile_id));
                //command.ExecuteNonQuery();
            }
        }
    }
}
