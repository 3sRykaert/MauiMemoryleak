namespace MauiMemoryleak;

public class NavigationService : INavigationService
{
    public async Task NavigateAsync<TView>() where TView
        : class, IView, new()
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
            if (!(mainPage.Page is MasterPage masterPage))
            {
                masterPage = new MasterPage();
                mainPage.Page = masterPage;
            }

            masterPage.Detail = new NavigationPage(page); // This line causes the memory leak
            //masterPage.Detail = page; // This line does not cause the memory leak
        }
    }
}