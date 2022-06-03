using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Ixcent.CryptoTerminal.Api.Additional
{
    public static class SignalRAttributeHandlerExtensions
    {
        private static Dictionary<Type, string> __signalRHubs;

        private static Dictionary<Type, string> SignalRHubs
        {
            get
            {
                if (__signalRHubs == null)
                {
                    InitializeSignalRHubs();
                }
                return __signalRHubs;
            }
        }

        /// <summary>
        /// Automaticly finds all <see cref="Hub"/> inheritences and map them.
        /// </summary>
        /// <param name="source"></param>
        public static void MapSignalRHubs(this IEndpointRouteBuilder source)
        {
            MethodInfo method = typeof(HubEndpointRouteBuilderExtensions).GetMethods().First(m => m.Name.Equals(nameof(HubEndpointRouteBuilderExtensions.MapHub)));
            foreach (KeyValuePair<Type, string> hub in SignalRHubs)
            {
                method.MakeGenericMethod(hub.Key).Invoke(null, new object[] { source, hub.Value });
            }
        }

        private static void InitializeSignalRHubs()
        {
            __signalRHubs = new Dictionary<Type, string>();
            IEnumerable<Type>? allHubs = Assembly.GetAssembly(typeof(SignalRAttributeHandlerExtensions))!.GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Hub)));

            foreach (Type? hub in allHubs)
            {
                IEnumerable<Attribute>? attributes = hub.GetCustomAttributes();
                if (!attributes.Any(at => at is SignalRHiddenAttribute))
                {
                    var signalRAttribute = (SignalRHubAttribute)attributes.FirstOrDefault(at => at is SignalRHubAttribute);
                    if (signalRAttribute != null)
                    {
                        __signalRHubs.Add(hub, signalRAttribute.Path);
                    }
                    else
                    {
                        string hubRoute = hub.Name.Substring(hub.Name.Length - 3, 3).Equals("Hub", StringComparison.OrdinalIgnoreCase)
                            ? hub.Name.Substring(0, hub.Name.Length - 3) : hub.Name;
                        __signalRHubs.Add(hub, hubRoute);
                    }
                }
            }
        }
    }
}
