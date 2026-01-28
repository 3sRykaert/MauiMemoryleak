namespace BarcodeScanningDemo;

public class NavigationService : INavigationService
{
    public async Task NavigateAsync<TView>() where TView
        : Page
    {
        var view = ServiceHelper.GetService<TView>();
        SetMainPage(view);
    }


    private void SetMainPage(Page page)
    {
        Application.Current!.Windows[0].Page = page;
        return;
        //var mainPage = Application.Current?.Windows.FirstOrDefault();
        //if (mainPage != null)
        //{
        //    if (!(mainPage.Page is MasterPage masterPage))
        //    {
        //        masterPage = new MasterPage();
        //        mainPage.Page = masterPage;
        //    }

        //    masterPage.Detail = new NavigationPage(page); // This line causes the memory leak
        //    //masterPage.Detail = page; // This line does not cause the memory leak
        //}
    }
}