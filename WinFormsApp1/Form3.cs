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
            for(int i = 0; i < base_len; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    jaggedArray[i][j] = nomenclature_base[i].Split('/')[j];
                }
            }

            return jaggedArray;
        }
        // массив индексов товаров в товарной матрице добавленных в чек
        static int[] product = new int[0];
        // генерация товарной матрицы
        object[][] nomenclature_matrix = Arr();
        private void button1_Click(object sender, EventArgs e)
        {


            string searchinf_for = textBox1.Text;

            //label1.Text = String.Join(" ", nomenclature_matrix[0]) + "\n" + String.Join(" ", nomenclature_matrix[1]) + "\n" + String.Join(" ", nomenclature_matrix[2]) + "\n" + String.Join(" ", nomenclature_matrix[3]) + "\n" + String.Join(" ", nomenclature_matrix[4]) + "\n" + String.Join(" ", nomenclature_matrix[5]) + "\n" + String.Join(" ", nomenclature_matrix[6]);
            // поиск товара в товарной матрице и добавление в чековый массив
            for (int i = 0; i < nomenclature_base.Length; i++)
            {
                if (nomenclature_matrix[i][2].ToString() == searchinf_for)
                {
                    Array.Resize(ref product, product.Length + 1);
                    product[^1] = i;
                }
            }
            // вывод списка родуктов в чеке на экран
            Product_display();
            // вывод подсчета общей стоимости покупок
            PriceCounter();
        }
        // метод для отображения списка продуктов в чеке
        public void Product_display()
        {    
            //строка для формирования общей строки вывода
            string text_to_display = "";
            foreach(int i in product)
            {
                text_to_display += nomenclature_matrix[i][0].ToString() + "\n";
            }
            label1.Text = text_to_display;
        }
        //метод для подсчета общей стоимости покупок и вывода на экран
        public void PriceCounter()
        {
            // переменная для подсчета общей стоимости покупок
            double counter = 0;
            foreach (int i in product)
            {
                counter += Convert.ToDouble(nomenclature_matrix[i][1]);
            }
            label2.Text = counter.ToString() + " руб.";
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            
            // завершение работы приложения при закрытии окна
            this.FormClosing += new FormClosingEventHandler(Inicio_FormClosing_3);
        }
        // метод завершения работы приложения при закрытии окна
        private void Inicio_FormClosing_3(object sender, FormClosingEventArgs e)
        {
            
            Application.Exit();
        }

       
    }
}
