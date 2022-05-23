using System.Text;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;

namespace Ixcent.CryptoTerminal.Api.StartupConfiguration
{
    /// <summary> Updates xml comments for SWAGGER documentation generator. </summary>
    /// <remarks> Implements <see cref="IOperationFilter"/> </remarks>
    public class FormatXmlCommentProperties : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Description = Formatted(operation.Description);
            operation.Summary = Formatted(operation.Summary);
        }

        private string Formatted(string text)
        {
            if (text == null) return null;
            var stringBuilder = new StringBuilder(text);

            return stringBuilder
                .Replace("<para>", "<p>")
                .Replace("</para>", "</p>")
                .Replace("<para/>", "\n")
                .Replace("<para />", "\n")
                .ToString();
        }
    }
}
