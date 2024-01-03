using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ExternalServices.Helpers
{
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning disable CS0612 // Type or member is obsolete
    public class HttpRedirectHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (response.StatusCode == HttpStatusCode.MovedPermanently)
            {
                var location = response.Headers.Location;
                if (location == null)
                {
                    return response;
                }

                using var redirect = await RedirectRequest(request, location);
                response = await base.SendAsync(redirect, cancellationToken);
            }
            return response;
        }

        [Obsolete]
        private async Task<HttpRequestMessage> RedirectRequest(HttpRequestMessage request, Uri location)
        {
            var redirect = new HttpRequestMessage(request.Method, location);

            if (request.Content != null)
            {
                redirect.Content = await RedirectContent(request);
                if (request.Content.Headers != null)
                {
                    RedirectHeaders(redirect, request);
                }
            }

            redirect.Version = request.Version;
            RedirectProperties(redirect, request);
            RedirectKeyValuePairs(redirect, request);
            return redirect;
        }

        private async Task<StreamContent> RedirectContent(HttpRequestMessage request)
        {
            var memstrm = new MemoryStream();
            await request.Content.CopyToAsync(memstrm).ConfigureAwait(false);
            memstrm.Position = 0;
            return new StreamContent(memstrm);
        }

        private void RedirectHeaders(HttpRequestMessage redirect, HttpRequestMessage request)
        {
            foreach (var header in request.Content.Headers)
            {
                redirect.Content.Headers.Add(header.Key, header.Value);
            }
        }

        [Obsolete]
        private void RedirectProperties(HttpRequestMessage redirect, HttpRequestMessage request)
        {
            foreach (KeyValuePair<string, object> prop in request.Properties)
            {
                redirect.Properties.Add(prop);
            }
        }

        private void RedirectKeyValuePairs(HttpRequestMessage redirect, HttpRequestMessage request)
        {
            foreach (KeyValuePair<string, IEnumerable<string>> header in request.Headers)
            {
                redirect.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }
    }
#pragma warning restore CS0612 // Type or member is obsolete
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
}
