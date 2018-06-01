using PokeAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamaDex
{
	public partial class MainPage : ContentPage
	{
        static Entry pkmnText;
        static Button searchButton;
        static Image pkmnImage;
        static int pkmnId;
        static string pkmnName;
        static string pkmnNameC;
        static Label pkmnLabel;
        static Label pkmnGenera;
        static Label pkmnFlavorText;
        static Label pkmnStatsTitle;
        static Label pkmnStatHP;
        static Label pkmnStatAtk;
        static Label pkmnStatDef;
        static Label pkmnStatSpA;
        static Label pkmnStatSpD;
        static Label pkmnStatSpe;
        static Label pkmnBaseStatTotal;

        public MainPage()
		{
            InitializeComponent();
            this.Padding = new Thickness(20, 20, 20, 20);

            StackLayout panel = new StackLayout
            {
                Spacing = 15
            };

            panel.Children.Add(new Label
            {
                Text = "Ingresa el nombre de un Pokemon:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(pkmnText = new Entry
            {
                Text = "Pikachu",
            });

            panel.Children.Add(searchButton = new Button
            {
                Text = "Buscar"
            });

            panel.Children.Add(pkmnImage = new Image
            {
                Source = ImageSource.FromUri(new Uri("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/" + pkmnId + ".png"))
            });

            panel.Children.Add(pkmnLabel = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(pkmnGenera = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });

            panel.Children.Add(pkmnFlavorText = new Label
            {
                Text = "",
                IsVisible = false,
                BackgroundColor = Color.Yellow,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });

            panel.Children.Add(pkmnStatsTitle = new Label
            {
                IsVisible = false,
                Text = "Stats",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(pkmnStatHP = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });

            panel.Children.Add(pkmnStatAtk = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });

            panel.Children.Add(pkmnStatDef = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });

            panel.Children.Add(pkmnStatSpA = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });

            panel.Children.Add(pkmnStatSpD = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });

            panel.Children.Add(pkmnStatSpe = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            });

            panel.Children.Add(pkmnBaseStatTotal = new Label
            {
                IsVisible = false,
                Text = "Base Stat Total: ",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold
            });


            searchButton.Clicked += OnSearch;
            this.Content = panel;
        }

        private static async void OnSearch(object sender, EventArgs e)
        {
            PokemonSpecies p = await DataFetcher.GetNamedApiObject<PokemonSpecies>(pkmnText.Text.ToLower());
            Pokemon pk = await DataFetcher.GetNamedApiObject<Pokemon>(pkmnText.Text.ToLower());
            pkmnId = p.ID;
            pkmnImage.Source = new Uri("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/" + pkmnId + ".png");
            pkmnImage.Scale = 2.0;
            pkmnName = p.Name;
            pkmnNameC = char.ToUpper(pkmnName.First()) + pkmnName.Substring(1).ToLower();
            pkmnLabel.Text = "#" + pkmnId.ToString() + ": " + pkmnNameC;
            pkmnGenera.Text = p.Genera[2].Name;
            switch (p.Generation.Name) {
                case "generation-v":
                    pkmnFlavorText.Text = p.FlavorTexts[36].FlavorText;
                    break;
                case "generation-vi":
                    pkmnFlavorText.Text = p.FlavorTexts[25].FlavorText;
                    break;
                case "sun-moon":
                    pkmnFlavorText.Text = p.FlavorTexts[2].FlavorText;
                    break;
                default:
                    pkmnFlavorText.Text = p.FlavorTexts[p.FlavorTexts.Length - 1].FlavorText;
                    break;
            }
            pkmnFlavorText.IsVisible = true;
            pkmnStatsTitle.IsVisible = true;
            pkmnStatHP.Text = "HP: " + pk.Stats[5].BaseValue.ToString();
            pkmnStatAtk.Text = "Attack: " + pk.Stats[4].BaseValue.ToString();
            pkmnStatDef.Text = "Defense: " + pk.Stats[3].BaseValue.ToString();
            pkmnStatSpA.Text = "Special Attack: " + pk.Stats[2].BaseValue.ToString();
            pkmnStatSpD.Text = "Special Defense: " + pk.Stats[1].BaseValue.ToString();
            pkmnStatSpe.Text = "Speed: " + pk.Stats[0].BaseValue.ToString();
            var pkmnBSTSum = pk.Stats[0].BaseValue + pk.Stats[1].BaseValue + pk.Stats[2].BaseValue + pk.Stats[3].BaseValue + pk.Stats[4].BaseValue + pk.Stats[5].BaseValue;
            pkmnBaseStatTotal.IsVisible = true;
            pkmnBaseStatTotal.Text += pkmnBSTSum.ToString();
        }
    }
}
