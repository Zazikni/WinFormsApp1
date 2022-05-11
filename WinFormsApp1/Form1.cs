namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static public string user = "";
        // имитация чтения данных из бд
        string[] kassir_log = File.ReadAllLines(@"C:\Users\Никита\Desktop\kursovaya\WinFormsApp1\WinFormsApp1\login.txt" , System.Text.Encoding.Default);
        string[] kassir_pass = File.ReadAllLines(@"C:\Users\Никита\Desktop\kursovaya\WinFormsApp1\WinFormsApp1\password.txt", System.Text.Encoding.Default);
        private void button1_Click(object sender, EventArgs e)
        {
            // поиск логина в списке логинов
            if (kassir_log.Contains(textBox2.Text))
            {

                // вычесление индекса логина с списке
                int index = Array.IndexOf(kassir_log, textBox2.Text);

                // проверка заполненности поля пароля
                if (maskedTextBox1.Text != "")
                {
                    // сопоставление введенного пароля с паролем соответствующем индексу введенного логина
                    if (kassir_pass[index] == maskedTextBox1.Text)
                    {
                        user = textBox2.Text;
                        // Открывается форма работы с товаром
                        Form3 newForm = new Form3();
                        newForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        label1.Text = "НЕВЕРНЫЙ ПАРОЛЬ";
                        label1.BackColor = Color.Red; label1.ForeColor = Color.White;
                    }
                }
                else
                {
                    label1.Text = "ВВЕДИТЕ ПАРОЛЬ";
                    label1.BackColor = Color.Red; label1.ForeColor = Color.White;
                }
                
            }
            else
            {
                label1.Text = "ДОСТУП ЗАПРЕЩЕН";
                label1.BackColor = Color.Yellow; label1.ForeColor = Color.Black;
            }
        }

        // завершение работы приложения при закрытии окна
        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(Inicio_FormClosing_1);
        }

        private void Inicio_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        
    }
}