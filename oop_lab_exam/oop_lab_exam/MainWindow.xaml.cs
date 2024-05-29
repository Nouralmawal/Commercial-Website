using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace oop_lab_exam
{
    public partial class MainWindow : Window
    {
        DBAccess objDBAccess = new DBAccess();
        Person person = new Person();

        public MainWindow()
        {
            InitializeComponent();
            Average.Click += Average_Click;
        }

        private void Average_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string firstName = FirstName.Text;
                string lastName = lastName.Text;
                DateTime dateOfBirth = DateTime.Parse(DateOfBirthTextBox.Text);
                int mathCourse = int.Parse(Math.Text);
                int oopCourse = int.Parse(Oop.Text);
                int statisticsCourse = int.Parse(Statistics.Text);
                int javaCourse = int.Parse(Java.Text);

                // Insert the data into the database
                int rowsAffected = InsertPerson(firstName, lastName, dateOfBirth, mathCourse, oopCourse, statisticsCourse, javaCourse);

                // Check if the insertion was successful
                if (rowsAffected > 0)
                {
                    // Retrieve the inserted data and display it in the RichTextBox
                    DataTable dataTable = objDBAccess.ExecuteQuery("SELECT * FROM People");
                    DisplayDataInRichTextBox(dataTable);

                    // Calculate the average of the courses and display the result
                    double average = (mathCourse + oopCourse + statisticsCourse + javaCourse) / 4.0;
                    DisplayAverageInRichTextBox(average);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DisplayDataInRichTextBox(DataTable dataTable)
        {
            FlowDocument flowDocument = new FlowDocument();

            foreach (DataRow row in dataTable.Rows)
            {
                Paragraph paragraph = new Paragraph();

                foreach (var item in row.ItemArray)
                {
                    paragraph.Inlines.Add(new Run(item.ToString() + " | "));
                }

                flowDocument.Blocks.Add(paragraph);
            }

            RichTextBox.Document = flowDocument;
        }

        private void DisplayAverageInRichTextBox(double average)
        {
            Paragraph paragraph = new Paragraph(new Run($"Average Course Score: {average:F2}"));
            RichTextBox.Document.Blocks.Add(paragraph);
        }
    }
}
