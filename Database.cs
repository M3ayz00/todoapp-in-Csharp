using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_List
{
    internal class Database
    {
        private string connectionString = "Data Source=DESKTOP-35G9OCM\\SQLEXPRESS;Initial Catalog=di2;Integrated Security=True;TrustServerCertificate=True;";

        public void InsertTask(Task task)
        {
            try 
            { 
                using (SqlConnection sqlcnx = new SqlConnection(connectionString))
                {
                    sqlcnx.Open();
                    string sqlcmd = "insert into Tasks (TaskName,DueDate,ReminderTime) values (@Name,@DueDate,@Reminder)";
                    SqlCommand cmd = new SqlCommand(sqlcmd, sqlcnx);
                    cmd.Parameters.AddWithValue("@Name", task.Name);
                    cmd.Parameters.AddWithValue("@DueDate", task.DueDate);
                    cmd.Parameters.AddWithValue("@Reminder", (object)task.Reminder ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while connecting to the database exactly in insertask function : " + ex.Message);
            }
        }

        public List<Task> GetAllTasks()
        {
            List<Task> tasks = new List<Task>();
            try 
            { 
                using(SqlConnection sqlcnx = new SqlConnection(connectionString))
                {
                    sqlcnx.Open();
                    string sqlcmd = "SELECT TaskID, TaskName, DueDate, ReminderTime, IsComplete FROM Tasks";
                    SqlCommand cmd = new SqlCommand(sqlcmd, sqlcnx);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int taskId = reader.GetOrdinal("TaskID");
                        int taskName = reader.GetOrdinal("TaskName");
                        int dueDate = reader.GetOrdinal("DueDate");
                        int reminderTime = reader.GetOrdinal("ReminderTime");
                        int isComplete = reader.GetOrdinal("IsComplete");

                        Task task = new Task(reader.GetString(taskName), reader.GetDateTime(dueDate));
                        task.TaskID = reader.GetInt32(taskId);
                        if (!reader.IsDBNull(reminderTime))
                        {
                            task.Reminder = reader.GetDateTime(reminderTime);
                        }
                        task.IsCompleted = reader.GetBoolean(isComplete);
                        tasks.Add(task);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while connecting to the database exactly in getalltasks function : " + ex.Message);
            }
            return tasks;
        }
        public void DeleteTask(int TaskID)
        {
            try 
            { 
                using(SqlConnection sqlcnx = new SqlConnection(connectionString))
                {
                    sqlcnx.Open();
                    string sqlcmd = "delete from Tasks where TaskID = @TaskID";
                    SqlCommand cmd = new SqlCommand(sqlcmd, sqlcnx);
                    cmd.Parameters.AddWithValue("@TaskID", TaskID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while connecting to the database exactly in deletetask function : " + ex.Message);
            }
        }

        public void UpdateTask(Task task)
        {
            try
            {
                using (SqlConnection sqlcnx = new SqlConnection(connectionString))
                {
                    sqlcnx.Open();
                    string sqlcmd = "update Tasks set TaskName = @Name, DueDate=@DueDate,ReminderTime=@ReminderTime,IsComplete=@IsComplete where TaskID=@TaskId";
                    SqlCommand cmd = new SqlCommand(sqlcmd, sqlcnx);
                    cmd.Parameters.AddWithValue("@Name", task.Name);
                    cmd.Parameters.AddWithValue("@DueDate", task.DueDate);
                    cmd.Parameters.AddWithValue("@ReminderTime", task.Reminder);
                    cmd.Parameters.AddWithValue("@IsComplete", task.IsCompleted);
                    cmd.Parameters.AddWithValue("@TaskID", task.TaskID);
                    cmd.ExecuteNonQuery();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Error while connecting to the database exactly in updatetask funuction : " + ex.Message);
            }
        }
    }
}
