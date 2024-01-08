using MVVM_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVM_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DBFriendPage : ContentPage
    {
        public DBFriendPage()
        {
            InitializeComponent();
        }

        private void Loobu_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void Kustuta_Clicked(object sender, EventArgs e)
        {
            Friend friend = (Friend)BindingContext;
            App.Database.DeleteItem(friend.Id);
            Navigation.PopAsync();
        }

        private void Salvesta_Clicked(object sender, EventArgs e)
        {
            Friend friend = (Friend)BindingContext;
            if (!String.IsNullOrEmpty(friend.Name))
            {
                App.Database.SaveItem(friend);
            }
            Navigation.PopAsync();
        }
    }
}