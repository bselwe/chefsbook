using ChefsBook_UWP_App.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace ChefsBook_UWP_App.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IRecipeApiService, FakeRecipeApiService>();
            }
            else
            {
                SimpleIoc.Default.Register<IRecipeApiService, RecipeApiService>();
            }

            SimpleIoc.Default.Register<RecipeCollectionViewModel>();
        }

        public RecipeCollectionViewModel RecipeCollection => ServiceLocator.Current.GetInstance<RecipeCollectionViewModel>();
    }
}
