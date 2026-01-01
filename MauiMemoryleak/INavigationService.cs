namespace MauiMemoryleak;

public interface INavigationService
{
    Task NavigateAsync<TView>()
        where TView : class, IView, new();
}