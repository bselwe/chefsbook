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
            SimpleIoc.Default.Register<RecipeDetailsViewModel>();
            SimpleIoc.Default.Register<RecipeEditViewModel>();
        }

        public RecipeCollectionViewModel RecipeCollection => ServiceLocator.Current.GetInstance<RecipeCollectionViewModel>();
        public RecipeDetailsViewModel RecipeDetails => ServiceLocator.Current.GetInstance<RecipeDetailsViewModel>();
        public RecipeEditViewModel RecipeEdit => ServiceLocator.Current.GetInstance<RecipeEditViewModel>();
    }
}
