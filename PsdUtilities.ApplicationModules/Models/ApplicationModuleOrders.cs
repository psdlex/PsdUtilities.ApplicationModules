namespace PsdUtilities.ApplicationModules.Models;

public static class ApplicationModuleOrders
{
    public const int Default = 0;

    public static class Web
    {
        // Common middleware orders
        public const int HttpsRedirection = 0;
        public const int StaticFiles = 20;
        public const int Routing = 40;
        public const int Cors = 60;
        public const int Authentication = 80;
        public const int Authorization = 100;
        public const int ExceptionHandler = 120;
        public const int Endpoints = 140;

        public static class Debug
        {
            // Diagnostics / debugging
            public const int DeveloperExceptionPage = -100;
            public const int Swagger = -50;
        }
    }
}