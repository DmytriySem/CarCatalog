using CarCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCatalog.Helpers
{
    public static class CarsListHelper
    {
        public static MvcHtmlString CreateCarsList(List<CarTileModel> items, int colsNum)
        {
            TagBuilder divTable = new TagBuilder("div");
            divTable.AddCssClass("div-tile-table");

            int itemsCount = items.Count;
            for (int i = 0; i < itemsCount;)
            {
                TagBuilder divRow = new TagBuilder("div");
                divRow.AddCssClass("div-tile-row");

                for (int j = 0; j < colsNum; j++)
                {
                    if (i == itemsCount)
                        break;

                    TagBuilder divCell = new TagBuilder("div");
                    divCell.AddCssClass("div-tile-cell");

                    TagBuilder divTile = new TagBuilder("div");
                    divTile.AddCssClass("div-tile");

                    if (items[i].PhotoUrl == null)
                        divTile.MergeAttribute("style", "background-image: url(/Content/Images/noModelImage.png);");
                    else
                        divTile.MergeAttribute("style", "background-image: url(/Content/Images/Models/" + items[i].PhotoUrl + ");");

                    TagBuilder divBrandImage = new TagBuilder("div");
                    TagBuilder brandImage = new TagBuilder("img");
                    if (items[i].Photo.Length == 0)
                        brandImage.MergeAttribute("src", "/Content/Images/download.png");
                    else
                        brandImage.MergeAttribute("src", "data:image/jpeg;base64," + Convert.ToBase64String(items[i].Photo));

                    divBrandImage.InnerHtml += brandImage.ToString();
                    divTile.InnerHtml += divBrandImage.ToString();

                    TagBuilder divData = new TagBuilder("div");
                    divData.AddCssClass("div-data");

                    TagBuilder p = new TagBuilder("h5");
                    p.InnerHtml = "<span>Model: </span>" + items[i].Name;
                    divData.InnerHtml += p.ToString();

                    p = new TagBuilder("h5");
                    p.InnerHtml = "<span>Color: </span>" + items[i].Color.ToString();
                    divData.InnerHtml += p.ToString();

                    p = new TagBuilder("h5");
                    p.InnerHtml = "<span>Volume engine: </span>" + Math.Round(items[i].VolumeEngine, 1).ToString();
                    divData.InnerHtml += p.ToString();

                    p = new TagBuilder("h5");
                    p.InnerHtml = "<span>Description: </span>" + items[i].Description;
                    divData.InnerHtml += p.ToString();

                    p = new TagBuilder("h5");
                    p.AddCssClass("data-date-price");
                    p.InnerHtml = "<span>Date: </span>" + items[i].Date.ToShortDateString();
                    divData.InnerHtml += p.ToString();

                    p = new TagBuilder("h5");
                    p.AddCssClass("data-date-price");
                    p.InnerHtml = "<span>Price: </span>" + items[i].Price.ToString() + "$";
                    divData.InnerHtml += p.ToString();

                    divTile.InnerHtml += divData.ToString();
                    divCell.InnerHtml = divTile.ToString();
                    divRow.InnerHtml += divCell.ToString();
                    i++;
                }
                divTable.InnerHtml += divRow.ToString();
            }

            return new MvcHtmlString(divTable.ToString());
        }
    }
}