using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Office.Interop.Word;
using System.Windows.Shapes;
using Window = System.Windows.Window;
using Microsoft.Win32;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using NETFLIX.Data;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Aspose.Words;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace NETFLIX.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExportWindow.xaml
    /// </summary>
    public partial class ExportWindow : Window
    {
        ObservableCollection<UserSeriesElement> seriesElements;
        public ExportWindow(ObservableCollection<UserSeriesElement> seriesElements)
        {
            InitializeComponent();
            this.seriesElements = seriesElements;
            CreateExportText();
        }
        string exportString = "";
        string CreateExportText()
        {
            foreach(var x in seriesElements)
            {
                exportString += WorkData.SeriesToExportText(x.currSer);
            }
            return exportString;
        }
        private void Button_Click(object sender, RoutedEventArgs e)//docx
        {
            try
            {  
                FolderBrowserDialog openwnd = new FolderBrowserDialog();
                openwnd.ShowDialog();
                ApplicationClass app = new ApplicationClass(); 
                app.Documents.Add(); //добавляет новый документ
                app.ActiveDocument.Select(); //это типо вставляет курсор в начало докумена
                app.Selection.FormattedText.Text = exportString;
                app.ActiveDocument.SaveAs2(openwnd.SelectedPath + "\\ExportFavorites"+ DateTime.Now.ToShortDateString() +".docx");
                app.ActiveDocument.Close();
                MessageBox.Show("Данные экспортированы успешно!");
            }
            catch
            {
                MessageBox.Show("Ошибка экспорта");
                return;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//pdf
        {
            try
            {
                FolderBrowserDialog openwnd = new FolderBrowserDialog();
                openwnd.ShowDialog();
                ApplicationClass app = new ApplicationClass();
                app.Documents.Add(); //добавляет новый документ
                app.ActiveDocument.Select(); //это типо вставляет курсор в начало докумена
                app.Selection.FormattedText.Text = exportString;
                app.ActiveDocument.SaveAs2(openwnd.SelectedPath + "\\ExportFavorites" + DateTime.Now.ToShortDateString() + ".docx");
                app.ActiveDocument.Close();
                var doc = new Aspose.Words.Document(openwnd.SelectedPath + "\\ExportFavorites" + DateTime.Now.ToShortDateString() + ".docx");    
                File.Delete(openwnd.SelectedPath + "\\ExportFavorites" + DateTime.Now.ToShortDateString() + ".docx");
                doc.Save(openwnd.SelectedPath + "\\ExportFavorites" + DateTime.Now.ToShortDateString() + ".pdf");
                MessageBox.Show("Данные экспортированы успешно!");
            }
            catch
            {
                MessageBox.Show("Ошибка экспорта");
                return;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//txt
        {
            try
            {
                FolderBrowserDialog openwnd = new FolderBrowserDialog();
                openwnd.ShowDialog();
                using (StreamWriter writer = new StreamWriter(openwnd.SelectedPath + "\\ExportFavorites" + DateTime.Now.ToShortDateString() + ".txt", false))
                {
                    writer.WriteLineAsync(exportString);
                }
                MessageBox.Show("Данные экспортированы успешно!");
            }
            catch
            {
                MessageBox.Show("Ошибка экспорта");
                return;
            }
        }
    }
}
