namespace ResourceServer.Services
{
    static public class WebServiceLocator
    {
        public static T Resolve<T>()
            where T : class
        {
            return UnityConfig.Container.Resolve(typeof(T), null, null) as T;
        }
    }
}