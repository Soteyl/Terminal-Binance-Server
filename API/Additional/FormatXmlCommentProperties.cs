using System.Text;

using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ixcent.CryptoTerminal.Api.Additional
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
            StringBuilder? stringBuilder = new(text);

            return stringBuilder
                .Replace("<para>", "<p>")
                .Replace("</para>", "</p>")
                .Replace("<para/>", "\n")
                .Replace("<para />", "\n")
                .ToString();
        }
    }
}
