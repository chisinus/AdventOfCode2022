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
            // ctrlResult.Text = await Day1.ProcessDay1(3, 1);

            //ctrlResult.Text = await Day2.Process_1();
            //ctrlResult.Text = await Day2.Process_2();

            // ctrlResult.Text = await Day3.Process_1();
            // ctrlResult.Text = await Day3.Process_2();

            // ctrlResult.Text = await Day4.Process_1();
            // ctrlResult.Text = await Day4.Process_2();

            // ctrlResult.Text = await Day5.Process(true);
            ctrlResult.Text = await Day5.Process(false);
        }
    }
}