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
    public partial class Form3 : Form
    {
        // считывание позиций номенклатуры товаров из файла
        static string[] nomenclature_base = File.ReadAllLines(@"C:\Users\Никита\Desktop\kursovaya\WinFormsApp1\WinFormsApp1\nomenclature_base.txt");
        //int[] basket;
        //int total_sum;

        public Form3()
        {
            InitializeComponent();


        }

        
        // массив индексов товаров в товарной матрице добавленных в чек
        public static int[] product = new int[0];
        // массив учета веса \ количества товаров добавленных в чек
        public static double[] product_counter = new double[0];
        // генерация товарной матрицы
        object[][] nomenclature_matrix = Arr();
        


      


        private void button1_Click(object sender, EventArgs e)
        {


            string searchinf_for = textBox1.Text;

            // поиск товара в товарной матрице и добавление в чековый массив
            for (int i = 0; i < nomenclature_base.Length; i++)
            {
                if (nomenclature_matrix[i][2].ToString() == searchinf_for)          
                {
                    // группирует товары в чеке
                    /*
                      
                    int pr_index = Array.IndexOf(product, i);
                    Console.WriteLine(pr_index);
                    if (pr_index != -1)
                    {
                        product_counter[i] += Convert.ToDouble(textBox2.Text);
                    }
                    else
                    {
                        Array.Resize(ref product, product.Length + 1);
                        product[^1] = i;
                        Array.Resize(ref product_counter, product_counter.Length + 1);
                        product_counter[^1] = Convert.ToDouble(textBox2.Text);
                    }

                    */


                    // не группирует товары в чеке
                    Array.Resize(ref product, product.Length + 1);
                    product[^1] = i;
                    Array.Resize(ref product_counter, product_counter.Length + 1);


                    /*
                      if (InputFix(textBox2.Text) == "")
                    {
                        product_counter[^1] = Convert.ToDouble("1");
                    }
                    else
                    {
                         product_counter[^1] = Convert.ToDouble(InputFix(textBox2.Text));
                    }
                     */


                    //при отсутствии заполнения поля для веса/количества автоматически делает его равным 1
                    if (InputFix(textBox2.Text) == "")
                    {
                        product_counter[^1] = Convert.ToDouble("1");
                    }
                    else
                    {
                        if (Convert.ToInt32(nomenclature_matrix[i][3]) == 1) // если товар весовой
                        {
                            product_counter[^1] = Convert.ToDouble(InputFix(textBox2.Text));// без удаления дробной части
                        }
                        else// в остальных случаях 
                        {
                            product_counter[^1] = Math.Truncate(Convert.ToDouble(InputFix(textBox2.Text))); // убрать дробную часть*/
                        }
                        
                    }
                    

                }
            }
            // вывод списка родуктов в чеке на экран
            ProductDisplay();
            // вывод подсчета общей стоимости покупок
            PriceCounterDisplay();
            textBox1.Text = "";
            textBox2.Text = "";
        }


        private void button2_Click(object sender, EventArgs e)
        {
            //удаление последнего товара
            LastDel();
            // вывод списка родуктов в чеке на экран
            ProductDisplay();
            // вывод подсчета общей стоимости покупок
            PriceCounterDisplay();
        }



      
        private void button3_Click(object sender, EventArgs e)
        {
            //вывод данных о последнем добавленном товаре в строки ввода
            textBox1.Text = nomenclature_matrix[product[^1]][2].ToString();
            textBox2.Text = product_counter[^1].ToString();
            //удаление последнего товара
            LastDel();
            // вывод списка родуктов в чеке на экран
            ProductDisplay();
            // вывод подсчета общей стоимости покупок
            PriceCounterDisplay();
        }


        
        private void button4_Click(object sender, EventArgs e)
        {
            // проверка наполненности чека
            if (product.Length > 0)
            {
                Form4 newForm = new Form4();
                //переменная для хранения результата проверки чека на наличие алкоголя
                bool alcohol_check = false;
                foreach (int i in product)
                {
                    if (nomenclature_matrix[i][4].ToString() == "1")
                    {
                        alcohol_check = true;
                        break;
                    }
                }
                //проверка наличии в чеке алкоголя
                if (alcohol_check)
                {

                    // проверка времени продажи алкоголя 
                    if ((DateTime.Now.Hour < new DateTime(2009, 12, 12, 23, 0, 0).Hour) & (DateTime.Now.Hour > new DateTime(2009, 12, 12, 8, 0, 0).Hour))

                    {
                        //переменная для хранения результата проверки документов
                        DialogResult result;
                        //напоминание о проверке документов для продажи алкоголя
                        result = MessageBox.Show(
                        "Покупателю есть 18 ?",
                        "В чеке присутствует спиртное! Проверьте документы.",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);

                        if (result == DialogResult.Yes) // подтверждение кассиром проверки документов
                        {
                            // Запуск формы авторизации кассира
                            newForm.label1.Text = label1.Text + "Кассир : " + label5.Text + "\n" + $"Итого :    {PriceCounter()} р."; newForm.sum_counter = PriceCounter();
                            newForm.sum_counter = PriceCounter();

                            newForm.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                        "Запрет на торговлю алкоголем с 23:00 до 8:00",
                        "Удалите алкоголь из списка покупок!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    }



                }
                else
                {
                    // Запуск формы авторизации кассира
                    newForm.label1.Text = label1.Text + "Кассир : " + label5.Text + "\n" + $"Итого :    {PriceCounter()} р."; newForm.sum_counter = PriceCounter();
                    newForm.Show();
                }
            }
            else
            {
                MessageBox.Show(
                        "Отсканируйте товары!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
            }
            
            
        }
        









        private void Form3_Load(object sender, EventArgs e)
        {
            label5.Text = Form1.user;
            // завершение работы приложения при закрытии окна
            this.FormClosing += new FormClosingEventHandler(Inicio_FormClosing_3);
        }



        // метод завершения работы приложения при закрытии окна
        private void Inicio_FormClosing_3(object sender, FormClosingEventArgs e)
        {

            Application.Exit();
        }








        // метод для удаления последнего добавленного товара
        static public void LastDel()
        {
            //удаление последнего добавленного товара
            Array.Resize(ref product, product.Length - 1);
            Array.Resize(ref product_counter, product_counter.Length - 1);
            
        }


        // метод для корректировки передачи ввода пользователя
        static public string InputFix(string input)
        {
            input = input.Replace('.', ',');
            return input;
        }





        // метод для отображения списка продуктов в чеке
        public void ProductDisplay()
        {
            //строка для формирования общей строки вывода
            string text_to_display = "";
            for (int i = 0; i < product.Length; i++)
            {
                string mass_counter = "";
                if (nomenclature_matrix[product[i]][3].ToString() == "1")
                {
                    mass_counter = "кг.";
                }
                else if (nomenclature_matrix[product[i]][3].ToString() == "0")
                {
                    mass_counter = "шт";
                }

                double sum = Math.Round(Convert.ToDouble(product_counter[i]) * Convert.ToDouble(nomenclature_matrix[product[i]][1]), 3);
                text_to_display += nomenclature_matrix[product[i]][2].ToString() + " " + nomenclature_matrix[product[i]][0].ToString() + " " + product_counter[i] + " " + mass_counter + " " + "*" + " " + nomenclature_matrix[product[i]][1].ToString() + " " + "р." + " " + "=" + " " + sum + " " + "р." + "\n";
            }

            label1.Text = text_to_display;
        }


        //метод для подсчета стоимости покупок
        public double PriceCounter()
        {
            // переменная для хранения результата подсчета общей стоимости покупок
            double counter = 0;
            for (int i = 0; i < product.Length; i++)
            {
                counter += Math.Round(Convert.ToDouble(nomenclature_matrix[product[i]][1]) * product_counter[i], 3);
            }
            return counter;
        }


        //метод для вывода на экран стоимости покупок
        public void PriceCounterDisplay()
        {
            
            label2.Text = PriceCounter().ToString() + " руб.";
        }







        // метод - генератор товарной матрицы
        static public object[][] Arr()
        {
            // переменная с количеством позиций товара в базе
            int base_len = nomenclature_base.Length;
            // генерация массива массивов
            object[][] jaggedArray = new object[base_len][];
            //задание размера массивов внутри основного массива
            for (int i = 0; i < base_len; i++)
            {
                jaggedArray[i] = new object[5];
            }
            // заполняю массив массивов товарами и их признаками
            for (int i = 0; i < base_len; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    jaggedArray[i][j] = nomenclature_base[i].Split('/')[j];
                }
            }

            return jaggedArray;
        }





        private void Form3_MouseMove(object sender, MouseEventArgs e)
        {
            // вывод списка родуктов в чеке на экран
            ProductDisplay();
            // вывод подсчета общей стоимости покупок
            PriceCounterDisplay();
        }
       


        // обработчик для ввода только цифр
        public void TextBox1KeyPress(object sender, KeyPressEventArgs e)
        {
            // переменная для хранения введенного символа
            char ch = e.KeyChar;
            //Если символ, введенный с клавы - не цифра (IsDigit)
            if (!Char.IsDigit(ch) && ch != 8) 
            {
                // то событие не обрабатывается. ch!=8 (8 - это Backspace)
                e.Handled = true;
            }
        }





        // обработчик для ввода только цифр и запятой / точки
        public void TextBox2KeyPress(object sender, KeyPressEventArgs e)
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
