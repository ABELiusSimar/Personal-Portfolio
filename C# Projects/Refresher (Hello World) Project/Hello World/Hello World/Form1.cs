namespace Hello_World
{
    public partial class HelloWorld : Form
    {
        public HelloWorld()
        {
            InitializeComponent();
        }

        private void HelloWorldBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Hello World");
        }
    }
}