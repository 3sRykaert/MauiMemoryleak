namespace MauiMemoryleak;

public interface INavigationService
{
    Task NavigateAsync<TView>()
        where TView : Page;
}