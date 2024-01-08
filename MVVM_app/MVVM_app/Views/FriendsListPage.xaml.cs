using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM_app.Models;
using MVVM_app.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MVVM_app.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsListPage : ContentPage
    {
        public FriendsListPage()
        {
          
            InitializeComponent();
            BindingContext = new FriendsListViewModel() { Navigation=this.Navigation };
        }

        
    }
}