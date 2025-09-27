using Kanban.API.Client;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace Kanban.Web;

public class KanbanClientFactory(HttpClient httpClient)
{
    private readonly IAuthenticationProvider _authenticationProvider = new AnonymousAuthenticationProvider();

    public ApiClient GetClient() {
        return new ApiClient(new HttpClientRequestAdapter(_authenticationProvider, httpClient: httpClient));
    }
}