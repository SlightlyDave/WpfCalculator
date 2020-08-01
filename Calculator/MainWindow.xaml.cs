using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();

            btnAC.Click += BtnAC_Click;
            btnPosNeg.Click += BtnPosNeg_Click;
            btnPercentage.Click += BtnPercentage_Click;
            btnEquals.Click += BtnEquals_Click;
            btnDecimal.Click += BtnDecimal_Click;
        }

        private void BtnDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (!lblResult.Content.ToString().Contains("."))
            {
                lblResult.Content = $"{lblResult.Content}.";
            }
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;

            if (double.TryParse(lblResult.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMaths.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimpleMaths.Subtraction(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMaths.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMaths.Division(lastNumber, newNumber);
                        break;
                    default:
                        break;
                }

                lblResult.Content = result;
            }

        }

        private void BtnPercentage_Click(object sender, RoutedEventArgs e)
        {

            double tempNumber;

            if (double.TryParse(lblResult.Content.ToString(), out tempNumber))
            {
                tempNumber = tempNumber / 100;
                if (lastNumber != 0)
                    tempNumber *= lastNumber;

                lblResult.Content = tempNumber.ToString();
            }
        }

        private void BtnPosNeg_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(lblResult.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                lblResult.Content = lastNumber.ToString();
            }
        }

        private void BtnAC_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = "0";
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {

            if (double.TryParse(lblResult.Content.ToString(), out lastNumber))
            {
                lblResult.Content = "0";
            }

            if (sender == btnMultiply)
                selectedOperator = SelectedOperator.Multiplication;
            if (sender == btnDivide)
                selectedOperator = SelectedOperator.Division;
            if (sender == btnPlus)
                selectedOperator = SelectedOperator.Addition;
            if (sender == btnMinus)
                selectedOperator = SelectedOperator.Substraction;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = 0;

            selectedValue = int.Parse(((Button)sender).Content.ToString());

            if (lblResult.Content.Equals("0"))
            {
                lblResult.Content = $"{selectedValue}";
            }
            else
            {
                lblResult.Content = $"{lblResult.Content}{selectedValue}";
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }

    public class SimpleMaths
    {
        public static double Add(double n1, double n2)
        {
            return n1 + n2;
        }

        public static double Subtraction(double n1, double n2)
        {
            return n1 - n2;
        }


        public static double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }

        public static double Division(double n1, double n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("Unable to divide by zero", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

            return n1 / n2;

        }
    }
}
