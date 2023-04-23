using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_List
{
    
    public partial class Form1 : Form
    {
        private List<Task> tasks = new List<Task>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task task = new Task(textBox1.Text, dateTimePicker1.Value);
            tasks.Add(task);
            listBox1.Items.Add(task);
            textBox1.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            if(selectedIndex >= 0)
            {
                tasks.RemoveAt(selectedIndex);
                listBox1.Items.RemoveAt(selectedIndex);
                textBox1.Clear();
                dateTimePicker1.Value = DateTime.Now;
            }
            else
            {
                MessageBox.Show("Please select a task first..");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            if(selectedIndex >= 0)
            {
                Task selectedTask = tasks[selectedIndex];
                dateTimePicker1.Value = selectedTask.DueDate;
                dateTimePicker2.Value = selectedTask.Reminder ?? DateTime.Now;
                textBox1.Text = selectedTask.Name;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int selectedIndex = listBox1.SelectedIndex;
            if(selectedIndex >= 0)
            {
                Task selectedTask = tasks[selectedIndex];
                selectedTask.Reminder = dateTimePicker2.Value;
            }
            else { MessageBox.Show("Please select a task first.."); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach(Task task in tasks)
            {
                if(task.Reminder != null & DateTime.Now >= task.Reminder)
                {
                    MessageBox.Show($"Reminder : {task.Name} is due");
                    task.Reminder = null;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    public class Task
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? Reminder { get; set; }
        public Task(string name, DateTime dueDate)
        {
            Name = name;
            DueDate = dueDate;
            Reminder = null;
        }
        public override string ToString()
        {
            return $"{Name} ({DueDate.ToShortDateString()})";
        }
    }
}
