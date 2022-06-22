using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ТФЯ_лр1
{
     
    public partial class Compil : Form
    {
        static string open_path = "";
        //static string filename1 = "";
        public Compil()
        {
            InitializeComponent();
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
           // saveFileDialog1.Filter = "Text files(*.cs)|*.cs|All files(*.*)|*.*";
     
            openFileDialog1.Filter = "Текстовые файлы (*.cs)|*.cs|Все файлы (*.*)|*.*";
            saveFileDialog1.Filter = "Text File(*.cs)|*.cs|Все файлы (*.*)|*.*";
            autocompleteMenu1.Items = System.IO.File.ReadAllLines("cs-reserv-list.dicr");
        }

    
        //public class BinaryTree<T> where T : IComparable<T>
        //{
        //    private BinaryTree<T> parent, left, right;
        //    private T val;
        //    private List<T> listForPrint = new List<T>();

        //    public BinaryTree(T val, BinaryTree<T> parent)
        //    {
        //        this.val = val;
        //        this.parent = parent;
        //    }
        //}
    
        public void copy()
        {

            if (editing.TextLength > 0)
            {
                editing.Copy();
            }

            #region Большая версия
            //string copyText = string.Empty;

            //if (editing.SelectionLength == 0)
            //    copyText = editing.Text.Replace("\n", Environment.NewLine);

            //else
            //    copyText = editing.SelectedText.Replace("\n", Environment.NewLine);

            //try
            //{
            //    Clipboard.SetDataObject(copyText, true, 3, 400);
            //}

            //catch (System.Runtime.InteropServices.ExternalException)
            //{
            //    MessageBox.Show(this, "Не удалось очистить буфер обмена. Возможно буфер обмена используется другим процессом.",
            //        "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            #endregion

        }

        public void insert()  
        {
            if (editing.TextLength > 0)
            {
                editing.Paste();
            }
        }

        public void cut()
        {
            if (editing.TextLength > 0)
            {
                editing.Cut();
            }
        }
        public void cancel()
        {
            if (editing.CanUndo == true)
            {
                editing.Undo();
                editing.ClearUndo();
            }
        }

        public void repeat()
        {
            if (editing.CanRedo == true)
            {

                if (editing.RedoActionName != "Delete")

                    editing.Redo();

            }
        }

        public void create()
        {
            
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, editing.Text);
            MessageBox.Show("Файл создан");
            open_path = filename;
            editing.Clear();
        }

        public void open()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename2 = openFileDialog1.FileName;
            string text = System.IO.File.ReadAllText(filename2);
            editing.Text = text;
            open_path = filename2;
           //string path = System.IO.File.ReadAllText("files/path.t$");

            //TabPage newTabPage = new TabPage();
            //newTabPage.Text = "1";
            //editing11.TabPages.Add(newTabPage);

            //  string path = System.IO.File.ReadAllText("files/path.t$"); 

        }

        public void save()
        {
            try
            {
               // string path = System.IO.File.ReadAllText("files/path.t$");
                if (open_path != "")
                {
                    System.IO.File.WriteAllText(open_path, editing.Text);
                    MessageBox.Show("Файл сохранен!");
                }
                else
                {
                    MessageBox.Show(
                        "Error!",
                        "Файл не сохранён",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Файл не сохранён",
                    "Ошибка!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public void saveKak()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, editing.Text);
            //string path = System.IO.File.ReadAllText("files/path.t$");
        }
        public void selectAll()
        {
            if (editing.TextLength > 0)
            {
                editing.SelectAll();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ToolTip t1 = new ToolTip();
            t1.SetToolTip(createButt, "Создать");
            ToolTip t2= new ToolTip();
            t2.SetToolTip(openButt, "Открыть");
            ToolTip t3 = new ToolTip();
            t3.SetToolTip(saveButt, "Сохранить");
            ToolTip t4 = new ToolTip();
            t4.SetToolTip(cancelButt, "Назад");
            ToolTip t5 = new ToolTip();
            t5.SetToolTip(repeatButt, "Вперед");
            ToolTip t6 = new ToolTip();
            t6.SetToolTip(copyButt, "Копировать");
            ToolTip t7 = new ToolTip();
            t7.SetToolTip(cutButt, "Вырезать");
            ToolTip t8 = new ToolTip();
            t8.SetToolTip(insertButt, "Вставить");

        }

     
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            create();
        }


        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open();
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveKak();

            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //saveFileDialog1.Filter = "Text files(*.cs)|*.cs|All files(*.*)|*.*";
            //if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            //    return;
            //// получаем выбранный файл
            //string filename = saveFileDialog1.FileName;
            //// сохраняем текст в файл
            //System.IO.File.WriteAllText(filename, editing.Text);
            //MessageBox.Show("Файл сохранен");

            
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save();
        }

        //проблемс, узнать редактировался ли до этого файл???
        private void выходToolStripMenuItem_Click(object sender, EventArgs e) 
        {
      
            DialogResult dialogResult = MessageBox.Show("Вы хотите сохранить изменения?", "Выход", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (open_path != "")
                    save();
                else saveKak();
            }
                else if (dialogResult == DialogResult.No)
                {
                    
                    Close();
                }
            
        }

        private void editing_TextChanged(object sender, EventArgs e)
        {

        }

        private void копироватьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            copy();
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editing.SelectedText = "";
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cut();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insert();
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cancel();
        }


        private void повторитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            repeat();
        }

        private void createButt_Click(object sender, EventArgs e)
        {
            create();
        }

        private void openButt_Click(object sender, EventArgs e)
        {
            open();
        }

        private void saveButt_Click(object sender, EventArgs e)
        {
            save();
        }

        private void cancelButt_Click(object sender, EventArgs e)
        {
            cancel();
        }

        private void repeatButt_Click(object sender, EventArgs e)
        {
            repeat();
        }

        private void copyButt_Click(object sender, EventArgs e)
        {
            copy();
        }

        private void cutButt_Click(object sender, EventArgs e)
        {
            cut();
        }

        private void insertButt_Click(object sender, EventArgs e)
        {
            insert();
        }

        private char[] sdvig(char[] prox)
        {
            char prev = prox[0];
            char[] Nprox= new char[prox.Length+1];

            char next;
            for (int i = 0; i < prox.Length -1; ++i)
            {
                next = prox[i + 1];
                prox[i + 1] = prev;
                prev = next;
            }
            prox[0] = '!';
            for (int i = 0; i < prox.Length ; i++)
            {
                Nprox[i] = prox[i];
            }
            Nprox[Nprox.Length-1] = prev;

           // prox[prox.Length+1]= prev;
            //prox[0] = '!';
            return Nprox;
        }

        private char[] ErrSimbol(char[] prox)
        {
            Array.Resize<Char>(ref prox, prox.Length);
            for (int i = 0; i < prox.Length; i++)
            {
                if(char.IsLetter(prox[i]) == false && char.IsDigit(prox[i]) == false && prox[i]!='!')
                {

                    Array.Clear(prox, i, 1);
                }
            }
            return prox;
        }

        private void ErrEnter(char[] prox)
        {

            int enter = 0, count = 0, n = 0;

            for (int i = 0; i < prox.Length; i++)
            {
                if ((prox[i] == '!') && (i != 0))
                {
                    enter = i;
                    count++;
                    for (int j = n; j < enter; j++)
                    {
                        result.Text += prox[j];
                        n = enter ;
                    }
                    result.Text += '\n';
                    // ErrEnter(prox,i);
                }
            }
            if (count > 0)
            {
                for (int j = enter; j < prox.Length; j++)
                {
                    result.Text += prox[j];
                }
            }
        
        }

        private void comment()
        {
            // StringBuilder str = new StringBuilder();
            char[] prox;
            bool flEr = false, fVoskl=false, fSimb=false, fEnter=false;
            int count = 0, enter=0;

            string[] str = editing.Text.Split('\n');
            for (int r = 0; r < str.Length; r++)
            {
                flEr = false;
                fVoskl = false;
                fSimb = false;
                fEnter = false;
                prox = str[r].ToCharArray();
                if (prox.Length == 0)
                {
                    continue;
                }
                if (prox[0] != '!')
                {
                    count++;
                    flEr = true;
                    fVoskl = true;
                    prox = sdvig(prox);
                    //continue;
                }
                for (int i = 1; i < prox.Length; i++)
                {
                    if(char.IsLetter(prox[i]) == false && char.IsDigit(prox[i]) == false )
                    {
                        count++;
                        if ((prox[i]=='!') && (i!=0))
                        {
                            fEnter = true;
                            //enter = i;
                            // ErrEnter(prox,i);
                        }
                        else
                        {
                           
                            fSimb = true;
                        }
                        flEr = true;
                    }

                }

                result.Text += "Строка " + r + " :";
                if (flEr == false)
                {
                    result.Text += "Ошибок не обнаружено";
                }
                else
                {
                    
                    
                    if (fVoskl == true)
                    {
                        
                        result.Text += "Нет символа '!'. ";
                    }
                    if (fSimb == true)
                    {
                        count++;
                        result.Text += "Неверные символы. ";
                        prox = ErrSimbol(prox);
                    }
  
                    if (fEnter == true)
                    {
                        count++;
                        result.Text += "Пропущен ENTER. ";
                        result.Text += "Правильно:\n";
                        ErrEnter(prox);
                    }else
                        {
                            result.Text += "Правильно:\n";
                            for (int i = 0; i < prox.Length; i++)
                            {
                                result.Text += prox[i];
                            }
                            //result.Text += "\n";
                        }

                }
                result.Text += "\n";
            }
        }

        private void editing_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void вызToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(@"..\..\help\kr.html");
                //OpenMicrosoftWord(@"C:\Users\Alina\desctop\TFLab\TFLab\help\helpKR.docx");
            }
            catch
            {
                MessageBox.Show("Не удалось открыть", "Ошибка");
            }
            //MessageBox.Show("В данной программе реальзованы следующие функции:\n\n1.Для кнопок: Копировать, Вставить, Вырезать, Отменить, Повторить использовались стандартные методы класса RichTextBoks \n\n2.Для работы с файлами использовались классы openFileDialog и saveFileDialog ", "Справка");
        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectAll();
        }


        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Данная программа написана студентом группы АВТ-912 Беккер Алиной ", "О программе");
           
        }

        private void OpenMicrosoftWord(string v)
        {
            throw new NotImplementedException();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            result.Clear();
            comment();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result.Clear();
            comment();
        }
    } 
}
