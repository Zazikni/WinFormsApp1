namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // имитация данных из бд
        string[] kassir_log = File.ReadAllLines(@"C:\Users\Никита\Desktop\kursovaya\WinFormsApp1\WinFormsApp1\login.txt");
        string[] kassir_pass = File.ReadAllLines(@"C:\Users\Никита\Desktop\kursovaya\WinFormsApp1\WinFormsApp1\password.txt");
        private void button1_Click(object sender, EventArgs e)
        {
            if (kassir_log.Contains(textBox2.Text))
                {
                int index = Array.IndexOf(kassir_log, textBox2.Text);

                if (kassir_pass[index] == textBox1.Text)
                {
                    // Открывается форма работы с товаром
                    Form3 newForm = new Form3();
                    newForm.Show();
                    this.Hide();
                }
                else
                {
                    label1.Text = "ACCESS DENIED";
                    label1.BackColor = Color.Red; label1.ForeColor = Color.White;
                }
            }
            else
            {
                label1.Text = "VAS NET V SPISKE";
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
            //Things while closing
            Application.Exit();
        }
        
    }
}