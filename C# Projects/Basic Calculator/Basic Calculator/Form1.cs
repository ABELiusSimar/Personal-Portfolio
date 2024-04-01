using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic_Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Variables
        private string _CalTotal;
        private string _Option;
        private int _Num1;
        private int _Num2;
        private int _Buffer;
        private int _Result;

        private void No1Btn_Click(object sender, EventArgs e)
        {
            DisplayTxt.Text += No1Btn.Text;
        }

        private void No2Btn_Click(object sender, EventArgs e)
        {
            DisplayTxt.Text += No2Btn.Text;
        }

        private void No3Btn_Click(object sender, EventArgs e)
        {
            DisplayTxt.Text += No3Btn.Text;
        }

        private void No4Btn_Click(object sender, EventArgs e)
        {
            DisplayTxt.Text += No4Btn.Text;
        }

        private void No5Btn_Click(object sender, EventArgs e)
        {
            DisplayTxt.Text += No5Btn.Text;
        }

        private void No6Btn_Click(object sender, EventArgs e)
        {
            DisplayTxt.Text += No6Btn.Text;
        }

        private void No7Btn_Click(object sender, EventArgs e)
        {
            DisplayTxt.Text += No7Btn.Text;
        }

        private void No8Btn_Click(object sender, EventArgs e)
        {
            DisplayTxt.Text += No8Btn.Text;
        }

        private void No9Btn_Click(object sender, EventArgs e)
        {
            DisplayTxt.Text += No9Btn.Text;
        }

        private void No0Btn_Click(object sender, EventArgs e)
        {
            DisplayTxt.Text += No0Btn.Text;
        }

        private void PlusBtn_Click(object sender, EventArgs e)
        {
            _Option = "+";
            _Num1 = int.Parse(DisplayTxt.Text);
            _Buffer += _Num1;
            DisplayTxt.Clear();
        }

        private void MinusBtn_Click(object sender, EventArgs e)
        {
            _Option = "-";
            _Num1 = int.Parse(DisplayTxt.Text);
            if (_Buffer == 0)
            {
                _Buffer = _Num1;
            }
            else
            {
                _Buffer -= _Num1;
            }
            DisplayTxt.Clear();
        }

        private void MulBtn_Click(object sender, EventArgs e)
        {
            _Option = "*";
            _Num1 = int.Parse(DisplayTxt.Text);
            if (_Buffer == 0)
            {
                _Buffer = 1;
                _Buffer *= _Num1;
            }
            else
            {
                _Buffer *= _Num1;
            }
            DisplayTxt.Clear();
        }

        private void DivBtn_Click(object sender, EventArgs e)
        {
            _Option = "/";
            _Num1 = int.Parse(DisplayTxt.Text);
            if (_Buffer == 0)
            {
                _Buffer = _Num1;
            }
            else
            {
                _Buffer /= _Num1;
            }
            DisplayTxt.Clear();
        }

        private void CalculateBtn_Click(object sender, EventArgs e)
        {
            _Num2 = int.Parse(DisplayTxt.Text);

            if (_Option.Equals("+"))
            {
                _Result = _Buffer + _Num2;
            }
            else if (_Option.Equals("-"))
            {
                _Result = _Buffer - _Num2;
            }
            else if (_Option.Equals("*")) 
            {
                _Result = _Buffer * _Num2;
            }
            else if( _Option.Equals("/")) 
            {
                _Result = _Buffer / _Num2;
            }

            _Num1 = 0;
            _Num2 = 0;
            _Buffer = 0;

            DisplayTxt.Text = _Result.ToString();
        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            _Num1 = 0;
            _Num2 = 0;
            _Buffer = 0;
            _Result = 0;
            DisplayTxt.Clear();
        }
    }
}
