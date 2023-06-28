using CsvWpf.Domain.Model;
using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace CswWpf
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();

            openFileDialog.ShowDialog();
            openFileDialog.FileName = @"employes.csv";

            Employee employee = new();
            string[] employees;

            DataTable dt = new();            
            dt.Columns.Add("id", typeof(string));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("surename", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("phone", typeof(string));

            using (var sr = new StreamReader(openFileDialog.FileName))
            {                
                while (!sr.EndOfStream)
                {
                    sr.ReadLine();
                    while (sr.Peek() != -1)
                    {
                        string? sdfs = sr.ReadLine();
                        employees = sdfs.Split(',');

                        employee.Id = employees[0];
                        employee.Name = employees[1];
                        employee.Surename = employees[2];
                        employee.Email = employees[3];
                        employee.Phone = employees[4];

                        dt.Rows.Add(employees);
                    }

                        
                }
            }
            DataView dataView = new(dt);
            dataGrid.ItemsSource = dataView;
        }
    }
}
