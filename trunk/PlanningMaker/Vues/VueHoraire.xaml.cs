﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PlanningMaker.Modele;

namespace PlanningMaker
{
	/// <summary>
	/// Interaction logic for VueHoraire.xaml
	/// </summary>
	public partial class VueHoraire
	{
        private Horaire horaire;

		public VueHoraire()
		{
			this.InitializeComponent();
            horaire = new Horaire();
            DataContext = horaire;
		}
	}
}