namespace AdventOfCode2022
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            // ctrlResult.Text = await Day1.ProcessDay1(1, 1);
            ctrlResult.Text = await Day1.ProcessDay1(3, 1);
        }
    }
}