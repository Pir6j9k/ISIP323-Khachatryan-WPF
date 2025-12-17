using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ISIP323_Khachatryan_WPF
{
    internal class Price
    {
        static public int price_ = 0;
        static public int procent = 20;
        static public int TotalPrice { get { return price_; } set { price_ = value; } }
        static public List<CheckBox> PriceList { get; set; } = null;
        static private double r = 20;
        static public int n;
        static double A;
        static public void price()
        {
            switch (Info.ModelComboBox)
            {
                case "Toyota Land Cruiser Prado (2 000 000 ₽)":
                    TotalPrice = 2000000;
                    break;
                case "Toyota Hilux (6 000 000 ₽)":
                    TotalPrice = 6000000;
                    break;
                case "Mercedes-Benz C-Class (W206) (6 000 000 ₽)":
                    TotalPrice = 6000000;
                    break;
                case "Mercedes-Benz C-Class Coupe (3 000 000 ₽)":
                    TotalPrice = 3000000;
                    break;
                case "Mercedes-Benz GLA (5 000 000 ₽)":
                    TotalPrice = 5000000;
                    break;
            }
            switch (Info.EngineComboBox)
            {
                case "Бензиновый (+175 000 ₽)":
                    TotalPrice += 175000;
                    break;
                case "Дизельный (+500 000 ₽)":
                    TotalPrice += 500000;
                    break;
                case "Электрический  (+178 000 ₽)":
                    TotalPrice += 178000;
                    break;
                case "Гибридный (+3 000 000 ₽)":
                    TotalPrice += 3000000;
                    break;
            }
            switch (Info.ColorComboBox)
            {
                case "Белый (+0 ₽)":
                    TotalPrice += 0;
                    break;
                case "Черный (+70 000 ₽)":
                    TotalPrice += 70000;
                    break;
                case "Синий (+150 000 ₽)":
                    TotalPrice += +150000;
                    break;
                case "Серый (+90 000 ₽)":
                    TotalPrice += 90000;
                    break;
                case "Металический (+75 000 ₽)":
                    TotalPrice +=+75000;
                    break;
            }
            Info.MW.PriceTextBlock.Text += "Стоимость: " + TotalPrice.ToString();
        }
        static public string options()
        {
            price();
            string s = "Выбранные опции:\n";
            for (int i = 0; i < Price.PriceList.Count; i++)
            {

                CheckBox cb = Price.PriceList[i];
                if (cb.IsChecked == true)
                {
                    s += cb.Content.ToString() + "\n";
                    if (i == 0)
                    {
                        TotalPrice += 200000;
                    }
                    if (i == 1)
                    {
                        TotalPrice += 30000;
                    }
                    if (i == 2)
                    {
                        TotalPrice += 30000;
                    }
                    if (i == 3)
                    {
                        TotalPrice += 50000;
                    }
                    if (i == 4)
                    {
                        TotalPrice += 40000;
                    }
                }
            }
            Info.MW.PriceTextBlock.Text = "Стоимость: " + TotalPrice.ToString();
            return s;
        }
        static public void credit(TextBlock sum_cred)
        {
            int P = TotalPrice * procent / 100;
            int S = TotalPrice - P;
            double i = r / 100 / 12;
            A = S * (i * Math.Pow((1 + i), n)) / (Math.Pow((1 + i), n) - 1);

            sum_cred.Text = "Сумма кредита: " + S.ToString();
        }
        static public string info()
        {
            return $"\nЕжемесячный платеж = {A} \n Срок = {n} месяцев";
        }

    }
}
