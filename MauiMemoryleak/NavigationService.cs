namespace MauiMemoryleak;

public class NavigationService : INavigationService
{
    public async Task NavigateAsync<TView>() where TView
        : Page
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            var page = ServiceHelper.GetService<TView>();
            SetMainPage(page);
        });
    }


    private void SetMainPage(Page page)
    {
        var mainPage = Application.Current?.Windows.FirstOrDefault();
        if (mainPage != null)
        {
            if (page is StartPage)
            {
                mainPage.Page = page;
            }
            else
            {
                if (mainPage.Page is not MasterPage masterPage)
                {
                    masterPage = ServiceHelper.GetService<MasterPage>(); //back button does NOT work in DetailPages after this
                    //masterPage = Activator.CreateInstance<MasterPage>(); //back button still works
                    mainPage.Page = masterPage;
                }

                masterPage.Detail = new NavigationPage(page);
            }
        }
    }
}