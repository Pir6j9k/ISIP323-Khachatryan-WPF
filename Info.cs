using ISIP323_Khachatryan_WPF.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ISIP323_Khachatryan_WPF
{
    internal class Info
    {
        static public MainWindow MW { get; set; }
        static public string ModelComboBox { get; set; } = null;
        static public string EngineComboBox { get; set; } = null;
        static public string ColorComboBox { get; set; } = null;
        static public string options { get; set; } = "";
        static public string FIO { get; set; } = null;
        static public string Phone { get; set; } = null;
        static public string Email { get; set; } = null;


        static public void Show()
        {
            MessageBox.Show($" Модель: {ModelComboBox}\n " +
                $"Двигатель: {EngineComboBox}\n " +
                $"Цвет: {ColorComboBox}\n " +
                $"{options}\n" +
                $"Имя: {FIO}\n" +
                $"Телефон: {Phone}\n " +
                $"Почта: {Email}\n " +
                $"{Price.info()}" 
                );
        }
        static public bool all_complited()
        {
            return ModelComboBox != null && EngineComboBox != null && ColorComboBox != null;
        }
    }
}
