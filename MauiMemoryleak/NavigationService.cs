namespace MauiMemoryleak;

public class NavigationService : INavigationService
{
    public async Task NavigateAsync<TView>() where TView
        : Page
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            var view = Activator.CreateInstance<TView>();
            var page = view as Page;

            SetMainPage(page);
        });
    }


    private void SetMainPage(Page page)
    {
        var mainPage = Application.Current?.Windows.FirstOrDefault();
        if (mainPage != null)
        {
            if (mainPage.Page is not MasterPage masterPage)
            {
                var newMasterPage = new MasterPage
                {
                    Detail = page
                };
                mainPage.Page.Navigation.PushAsync(new NavigationPage(newMasterPage));
            }
            else
            {
                masterPage.Navigation.PushAsync(new NavigationPage(page));
            }
            //masterPage.Detail = new NavigationPage(page); // This line causes the memory leak
            //masterPage.Detail = page; // This line does not cause the memory leak
        }
    }
}
