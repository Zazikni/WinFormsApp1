namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static public string user = "";
        // �������� ������ ������ �� ��
        string[] kassir_log = File.ReadAllLines(@"login.txt" , System.Text.Encoding.Default);
        string[] kassir_pass = File.ReadAllLines(@"password.txt", System.Text.Encoding.Default);
        string[] kassir_name = File.ReadAllLines(@"name.txt", System.Text.Encoding.Default);

        private void button1_Click(object sender, EventArgs e)
        {
            // ����� ������ � ������ �������
            if (kassir_log.Contains(textBox2.Text))
            {

                // ���������� ������� ������ � ������
                int index = Array.IndexOf(kassir_log, textBox2.Text);

                // �������� ������������� ���� ������
                if (maskedTextBox1.Text != "")
                {
                    // ������������� ���������� ������ � ������� ��������������� ������� ���������� ������
                    if (kassir_pass[index] == maskedTextBox1.Text)
                    {
                        user = kassir_name[index];
                        // ����������� ����� ������ � �������
                        Form3 newForm = new Form3();
                        newForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        label1.Text = "�������� ������";
                        label1.BackColor = Color.Red; label1.ForeColor = Color.White;
                    }
                }
                else
                {
                    label1.Text = "������� ������";
                    label1.BackColor = Color.Red; label1.ForeColor = Color.White;
                }
                
            }
            else
            {
                label1.Text = "������ ��������";
                label1.BackColor = Color.Yellow; label1.ForeColor = Color.Black;
            }
        }

        // ���������� ������ ���������� ��� �������� ����
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