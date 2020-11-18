﻿using OperationsBetweenForests.Core;
using OperationsBetweenForests.Serialization;
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

namespace OperationsBetweenForests.Calculation
{
    /// <summary>
    /// Logica di interazione per Forest_union.xaml
    /// </summary>
    public partial class Forest_union : UserControl
    {
        private Dictionary<String, Forest> ProdForests { get; set; }
        private List<Forest> LocalForests { get; set; }
        //private List<Core2.Forest> LocalForests2 { get; set; }

        public Forest_union()
        {
            InitializeComponent();
            ProdForests = new Dictionary<string, Forest>(2);
            LocalForests = new List<Forest>(2);
        }

        private void ClearDict()
        {
            ProdForests.Clear();
        }

        private void LoadFirstOperandBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProdForests.Count > 1)
            {
                ClearDict();
            }
            Forest f = (Forest)FileManager.DeserializeFromJsonFile();
            //f.GeneratesParentRelationships();
            f.GeneratesChildrenRelationships();
            ProdForests.Add(f.Name, f);
            FirstOperandTextBlock.Text = f.Name;
            LocalForests.Add(f);
        }

        private void LoadSecondOperandBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProdForests.Count > 1)
            {
                ClearDict();
            }
            Forest f = (Forest)FileManager.DeserializeFromJsonFile();
            //f.GeneratesParentRelationships();
            f.GeneratesChildrenRelationships();
            ProdForests.Add(f.Name, f);
            SecondOperandTextBlock.Text = f.Name;
            LocalForests.Add(f);
        }

        private void UnionButton_Click(object sender, RoutedEventArgs e)
        {
            Forest result = ForestCalculator.Bottom(ForestCalculator.Sum(LocalForests.First(), LocalForests.Last()));
            result.Name = LocalForests.First().Name + LocalForests.Last().Name;
            ResultLabel.Content = result.Name;
            result.DestroyChildrenRelationships();
            FileManager.SaveToJsonFile(result);
            MainWindow.Forests.Add(result.Name + "Res", result);//TODO Salvare nodo singolo; controllare distribuzione etichette
        }
    }
}
