namespace MauiMemoryleak;

public static class ReturnString
{
#if ANDROID
    public static string ReturnValue => "This Is Android";
#elif !PLATFORM
    public static string ReturnValue => "This Is NOT Android AND NOT Platform";
#else
    public static string ReturnValue => "This Is NOT Android BUT Platform";
#endif
}