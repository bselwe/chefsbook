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
                SimpleIoc.Default.Register<IApiService, FakeApiService>();
            }
            else
            {
                SimpleIoc.Default.Register<IApiService, ApiService>();
            }

            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<RecipeCollectionPageViewModel>();
            SimpleIoc.Default.Register<RecipeDetailsPageViewModel>();
            SimpleIoc.Default.Register<EditRecipePageViewModel>();
            SimpleIoc.Default.Register<SignInPageViewModel>();
            SimpleIoc.Default.Register<AppViewModel>();
        }

        public HomePageViewModel HomePageVM => ServiceLocator.Current.GetInstance<HomePageViewModel>();
        public RecipeCollectionPageViewModel RecipeCollectionPageVM => ServiceLocator.Current.GetInstance<RecipeCollectionPageViewModel>();
        public RecipeDetailsPageViewModel RecipeDetailsPageVM => ServiceLocator.Current.GetInstance<RecipeDetailsPageViewModel>();
        public EditRecipePageViewModel EditRecipePageVM => ServiceLocator.Current.GetInstance<EditRecipePageViewModel>();
        public SignInPageViewModel SignInPageVM => ServiceLocator.Current.GetInstance<SignInPageViewModel>();
        public AppViewModel AppVM => ServiceLocator.Current.GetInstance<AppViewModel>();
    }
}
