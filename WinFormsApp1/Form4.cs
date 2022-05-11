using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }





        public double sum_counter;
        private void button1_Click(object sender, EventArgs e)
        {
            // проверка размера внесенной суммы
            if (Convert.ToDouble(Form3.InputFix(textBox1.Text)) >= sum_counter) 
            {
                // формирование сообщениея о сдаче
                label1.Text = label1.Text + $"\nВнесено : {Form3.InputFix(textBox1.Text)} р."+ $"\nСдача : {Convert.ToDouble(Form3.InputFix(textBox1.Text)) - sum_counter} р.";
                MessageBox.Show(
                    $"Выдать сдачу {Convert.ToDouble(Form3.InputFix(textBox1.Text)) - sum_counter} р.",
                    "Сдача",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly
                    );
                //внесение денег в кассу
                InsertMoney();
                //обнуление списков товаров для работы с новым чеком
                Form3.product = new int[0];
                Form3.product_counter = new double[0];
                //блокировка активных кнопок на форме оплаты
                button1.Enabled = false;
                textBox1.Enabled = false;
                // кнопка возврата к формированию нового чека
                button2.Text = "Новый чек";
                
            }
        }

        // метод записи информации о внесенных деньгах в кассу
        public void InsertMoney()
        {
            int current_sum = Convert.ToInt32(File.ReadAllLines(@"C:\Users\Никита\Desktop\kursovaya\WinFormsApp1\WinFormsApp1\cash_holder.txt")[0]);
            if (current_sum >= 200000)
            {
                MessageBox.Show(
                    $"Сумма выручки в кассе превысила 200000 р. перед обслуживанием очередного клиента необходимо сдать выручку менеджеру.",
                    "Внимание!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly
                    );
            }
            File.WriteAllText(@"C:\Users\Никита\Desktop\kursovaya\WinFormsApp1\WinFormsApp1\cash_holder.txt", $"{current_sum + sum_counter}", System.Text.Encoding.Default);
        }

        



        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }






        private void Form4_Load(object sender, EventArgs e)
        {

        }








        // обработчик для ввода только цифр и запятой / точки
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // переменная для хранения введенного символа
            char ch = e.KeyChar;
            //Если символ, введенный с клавы - не цифра (IsDigit)
            if (!Char.IsDigit(ch) && ch != 8 && !((e.KeyChar == ',') | (e.KeyChar == '.')))
            {
                // то событие не обрабатывается. ch!=8 (8 - это Backspace)
                e.Handled = true;
            }
        }
    }
}
