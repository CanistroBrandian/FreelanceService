using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;

namespace FreelanceService.Web.Models
{
    public class PaginatorViewModel
    {

        public IPaginator Paginator { get; set; }

        public string Action { get; set; }

        public string Controller { get; set; }
        

        public object Properties { get; set; }

        public IUrlHelper Url { get; set; }

        public string GetUrlForPage(int? pageNumber)
        {

            var uri = new Uri(Url.Action(Action, Controller, Properties, Url.ActionContext.HttpContext.Request.Scheme));
            var uriBuilder = new UriBuilder(uri);
            var parameters = HttpUtility.ParseQueryString(uri.Query);
            parameters.Add(nameof(pageNumber), pageNumber?.ToString());
            uriBuilder.Query = parameters.ToString();
            return uriBuilder.Uri.ToString();
        }
    }
}
