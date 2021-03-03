using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HMS.MVCHelper
{
    public static class ModelPopupHelpers
    {

        //Create Button
        public static MvcHtmlString ModelDialog(this HtmlHelper htmlHelper, string text, string title, string url)
        {
            TagBuilder builder = new TagBuilder("button");
            builder.InnerHtml = text;
            builder.Attributes["title"] = title;
            builder.Attributes.Add("type", "button");
            builder.Attributes.Add("class", "btn btn-shadow btn-info btn-popup");
            builder.Attributes.Add("data-url", url);

            return new MvcHtmlString(builder.ToString());
        }

        //Create Prescription
        public static MvcHtmlString ModelPresDialog(this HtmlHelper htmlHelper, string text, string action, string controller, object routeValues = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            TagBuilder builder = new TagBuilder("button");
            builder.InnerHtml = text;
            builder.Attributes.Add("type", "button");
            builder.Attributes.Add("class", "btn btn-outline-light btn-block pres-btn p-2 btn-popup");
            builder.Attributes["data-url"] = urlHelper.Action(action, controller, routeValues);

            return new MvcHtmlString(builder.ToString());
        }

        //Detail Button
        public static MvcHtmlString DetailLink(this HtmlHelper htmlHelper, string action, string controller, object routeValues = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = "<i class='fas fa-angle-double-right'></i>";
            builder.Attributes.Add("type", "button");
            builder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);

            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString DetailLinkPopup(this HtmlHelper htmlHelper, string action, string controller, object routeValues = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("button");
            builder.InnerHtml = "<i class='fas fa-info-circle'></i>";
            builder.Attributes.Add("class", "btn btn-popup");
            builder.Attributes["data-url"] = urlHelper.Action(action, controller, routeValues);

            return new MvcHtmlString(builder.ToString());
        }


        //Edit Button
        public static MvcHtmlString ModelDialogEdit(this HtmlHelper htmlHelper, string title, string action, string controller, object routeValues = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("button");
            builder.SetInnerText(title);
            builder.Attributes.Add("type", "button");
            builder.Attributes.Add("class", "btn btn-focus btn-popup");
            builder.Attributes["data-url"] = urlHelper.Action(action, controller, routeValues);

            return new MvcHtmlString(builder.ToString());
        }

        //Delete Button
        public static MvcHtmlString ModelDialogDelete(this HtmlHelper htmlHelper, string title, string action, string controller, object routeValues = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("button");
            builder.SetInnerText(title);
            builder.Attributes.Add("type", "button");
            builder.Attributes.Add("class", "btn btn-warning btn-popup");
            builder.Attributes["data-url"] = urlHelper.Action(action, controller, routeValues);

            return new MvcHtmlString(builder.ToString());
        }

        //Edit Badge
        public static MvcHtmlString ModelDialogEditTime(this HtmlHelper htmlHelper, string title, string action, string controller, object routeValues = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("div");
            builder.SetInnerText(title);
            builder.Attributes.Add("type", "button");
            builder.Attributes.Add("class", "badge badge-pill badge-dark btn-popup");
            builder.Attributes["data-url"] = urlHelper.Action(action, controller, routeValues);

            return new MvcHtmlString(builder.ToString());
        }

        //Icon Button

        //Edit Button Icon
        public static MvcHtmlString EditIconButton(this HtmlHelper htmlHelper, string action, string controller, object routeValues = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("button");
            builder.Attributes.Add("class", "btn btn-focus btn-popup");
            builder.InnerHtml = "<i class='far fa-edit'></i>";
            builder.Attributes["data-url"] = urlHelper.Action(action, controller, routeValues);

            return new MvcHtmlString(builder.ToString());
        }

        //Edit Button Icon
        public static MvcHtmlString DeleteIconButton(this HtmlHelper htmlHelper, string action, string controller, object routeValues = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("button");
            builder.Attributes.Add("class", "btn btn-warning btn-popup");
            builder.InnerHtml = "<i class='fas fa-trash - alt'></i>";
            builder.Attributes["data-url"] = urlHelper.Action(action, controller, routeValues);

            return new MvcHtmlString(builder.ToString());
        }

    }
}
