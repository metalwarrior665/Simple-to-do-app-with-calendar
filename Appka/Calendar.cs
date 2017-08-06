using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Appka
{
    [Serializable]
    public partial class Calendar : Form
    {
        
        
        public Calendar()
        {
            InitializeComponent();
            if(File.Exists("C:\\CalendarApp\\save.bin"))
            {
                SaveObject loadedObject = BinarySerialization.ReadFromBinaryFile<SaveObject>("C:\\CalendarApp\\save.bin");
                records = loadedObject.DictionarySave;
                monthCalendar.BoldedDates = loadedObject.BoldedDatesSave;
                GlobalQuests = loadedObject.GlobalQuestsSave;
                listViewGlobal.Items.Clear();
                foreach (string quest in GlobalQuests)
                {
                    listViewGlobal.Items.Add(quest);
                }
            }
            else
            {
                Directory.CreateDirectory("C:\\CalendarApp");
              //  File.Create("C:\\lejno\\lejno.bin");               
            }
            

            this.FormClosing += Form1_FormClosing;
        }
       
        public Dictionary<DateTime, List<string>> records = new Dictionary<DateTime, List<string>>();
        public List<string> GlobalQuests = new List<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            
            label.Text = e.Start.ToShortDateString();
            listView.Items.Clear();
            foreach (KeyValuePair< DateTime, List < string >> kvp in records)
            {
                if (kvp.Key == e.Start)
                {      
                    foreach (string record in kvp.Value)
                    {     
                        listView.Items.Add(record);
                    }
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(label.Text))
            {
                DateTime date = Convert.ToDateTime(label.Text);
                if (!records.Keys.Contains(date))
                {
                    List<string> recordsList = new List<string>();
                    recordsList.Add(textBox1.Text);
                    records.Add(date, recordsList);
                }
                else
                {

                    records[date].Add(textBox1.Text);
                }
                listView.Clear();
                foreach (KeyValuePair<DateTime, List<string>> kvp in records)
                {
                    if (kvp.Key == date)
                    {

                        foreach (string record in kvp.Value)
                        {
                            listView.Items.Add(record);
                        }
                    }
                }
                
                monthCalendar.AddBoldedDate(date);
                textBox1.Clear();
            } 
        }
        
        

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            
            DateTime date = Convert.ToDateTime(label.Text);
            foreach (ListViewItem item in listView.CheckedItems)
            {
               if (records[date].Contains(item.Text))
                {
                    records[date].Remove(item.Text);
                }
            }
            listView.Items.Clear();
            foreach (KeyValuePair<DateTime, List<string>> kvp in records)
            {
                if (kvp.Key == date)
                {
                    foreach (string record in kvp.Value)
                    {
                        listView.Items.Add(record);
                    }
                }
            }

            if (records[date].Count == 0)
            {
                monthCalendar.RemoveBoldedDate(date);
            }
        }

     

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            SaveObject saveObject = new SaveObject();
            saveObject.DictionarySave = records;
            saveObject.BoldedDatesSave = monthCalendar.BoldedDates;
            saveObject.GlobalQuestsSave = GlobalQuests;
            BinarySerialization.WriteToBinaryFile("C:\\CalendarApp\\save.bin", saveObject);

            MessageBox.Show("Calendar saved to C:\\CalendarApp\\save.bin");
        }      

       
        private void AddGlobalButton_Click(object sender, EventArgs e)
        {
          GlobalQuests.Add(textBox1.Text);
          listViewGlobal.Clear();


            foreach (string quest in GlobalQuests)
                {
                 listViewGlobal.Items.Add(quest);  
                }
            textBox1.Clear();
            
        }

        private void RemoveGlobalButton_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewGlobal.CheckedItems)
            {
                if (GlobalQuests.Contains(item.Text))
                {
                    GlobalQuests.Remove(item.Text);
                }
            }

            listViewGlobal.Clear();

            foreach (string quest in GlobalQuests)
            {
                listViewGlobal.Items.Add(quest);
            }
            
        }

        
    }
}
