// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.RoadMapController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using m2ostnext.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class RoadMapController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public ActionResult Index() => (ActionResult) this.View();

    public ActionResult CreateGameTile() => (ActionResult) this.View();

    public ActionResult CreateGameTileAction()
    {
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      tbl_academic_tiles tile = new tbl_academic_tiles()
      {
        tile_name = this.Request.Form["Tile_Name"].ToString(),
        tile_position = Convert.ToInt32(this.Request.Form["Position"].ToString()),
        tile_description = this.Request.Form["Description"].ToString(),
        id_org = int32,
        theme_id = Convert.ToInt32(this.Request.Form["acad-tile-theme"])
      };
      tile.url = tile.theme_id != 2 ? (string) null : this.Request.Form["url_value"].ToString();
      int num = new RoadmapLogic().creategametile(tile);
      tile.id_academic_tile = num;
      if (((IEnumerable<string>) System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
      {
        HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["logo-btn"];
        string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["logo-btn"].FileName);
        string str = System.Web.HttpContext.Current.Request["id"];
        if (file != null)
        {
          if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/gametile/")))
            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/gametile/"));
          string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/gametile/"), int32.ToString() + num.ToString() + extension);
          file.SaveAs(filename);
          tile.tile_image = int32.ToString() + num.ToString() + extension;
          new RoadmapLogic().InsertTileimage(tile);
        }
      }
      return (ActionResult) this.RedirectToAction("GameTilesDashboard");
    }

    public ActionResult GameTilesDashboard()
    {
      this.ViewData["tiles"] = (object) new RoadmapLogic().getGameTilesDash(Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION));
      return (ActionResult) this.View();
    }

    public ActionResult EditGameTile(int tileid)
    {
      Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      this.ViewData["tile"] = (object) new RoadmapLogic().getGameTile(tileid);
      return (ActionResult) this.View();
    }

    public ActionResult EditGameTileAction()
    {
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      tbl_academic_tiles tile = new tbl_academic_tiles()
      {
        id_academic_tile = Convert.ToInt32(this.Request.Form["id_tile"].ToString()),
        tile_name = this.Request.Form["Tile_Name"].ToString(),
        tile_position = Convert.ToInt32(this.Request.Form["Position"].ToString()),
        tile_description = this.Request.Form["Description"].ToString(),
        theme_id = Convert.ToInt32(this.Request.Form["acad-them-id"].ToString())
      };
      tile.url = tile.theme_id != 2 ? (string) null : this.Request.Form["url_value"].ToString();
      tile.id_org = int32;
      int num = new RoadmapLogic().UpdateTile(tile);
      tile.id_academic_tile = num;
      if (((IEnumerable<string>) System.Web.HttpContext.Current.Request.Files.AllKeys).Any<string>())
      {
        HttpPostedFile file = System.Web.HttpContext.Current.Request.Files["logo-btn"];
        string extension = Path.GetExtension(System.Web.HttpContext.Current.Request.Files["logo-btn"].FileName);
        if (file != null && file.ContentLength > 0)
        {
          if (!Directory.Exists(this.HttpContext.Server.MapPath("~/Content/gametile/")))
            Directory.CreateDirectory(this.HttpContext.Server.MapPath("~/Content/gametile/"));
          string filename = Path.Combine(this.HttpContext.Server.MapPath("~/Content/gametile/"), int32.ToString() + num.ToString() + extension);
          file.SaveAs(filename);
          tile.tile_image = int32.ToString() + num.ToString() + extension;
          new RoadmapLogic().InsertTileimage(tile);
        }
      }
      return (ActionResult) this.RedirectToAction("GameTilesDashboard");
    }

    public ActionResult DeleteGameTile(int tileid)
    {
      Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      new RoadmapLogic().deletegametile(tileid);
      return (ActionResult) this.RedirectToAction("GameTilesDashboard");
    }

    public ActionResult JourneyTileMapping()
    {
      this.ViewData["tiles"] = (object) new RoadmapLogic().getGameTiles(Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION));
      return (ActionResult) this.View();
    }

    public string GetJourneyTilesForGameTile(int gametiles)
    {
      int orgid = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      List<journey_tile> journeyTileList = new List<journey_tile>();
      DbSet<tbl_brief_category_tile> briefCategoryTile1 = this.db.tbl_brief_category_tile;
      Expression<Func<tbl_brief_category_tile, bool>> predicate = (Expression<Func<tbl_brief_category_tile, bool>>) (t => t.id_organization == (int?) orgid && t.status == "A");
      foreach (tbl_brief_category_tile briefCategoryTile2 in briefCategoryTile1.Where<tbl_brief_category_tile>(predicate).ToList<tbl_brief_category_tile>())
      {
        tbl_brief_category_tile item = briefCategoryTile2;
        tbl_brief_category_tile briefCategoryTile3 = this.db.tbl_brief_category_tile.Where<tbl_brief_category_tile>((Expression<Func<tbl_brief_category_tile, bool>>) (t => t.id_brief_category_tile == item.id_brief_category_tile && t.status == "A")).FirstOrDefault<tbl_brief_category_tile>();
        if (briefCategoryTile3 != null)
        {
          tbl_brief_tile_academic_mapping mappedTile = new RoadmapLogic().GetMappedTile(briefCategoryTile3.id_brief_category_tile, gametiles);
          journeyTileList.Add(new journey_tile()
          {
            id_brief_category_tile = item.id_brief_category_tile,
            category_tile = item.category_tile,
            flag = mappedTile.id_academic_tile != 0
          });
        }
      }
      string str = "";
      foreach (journey_tile journeyTile in journeyTileList)
      {
        str = str + " <tr><td>" + journeyTile.category_tile + "</td>";
        str += " <td>";
        if (journeyTile.flag)
        {
          str = str + " <a  style=\"display:none;\" id=\"add_" + (object) journeyTile.id_brief_category_tile + "\" href=\"#\" onclick=\"addJourneyToGame('" + (object) journeyTile.id_brief_category_tile + "')\"><i class=\"glyphicon glyphicon-plus-sign\"></i></a>";
          str = str + " <i id=\"done_" + (object) journeyTile.id_brief_category_tile + "\" class=\"glyphicon glyphicon-ok\"></i>";
          str = str + " <a id=\"link_" + (object) journeyTile.id_brief_category_tile + "\" href=\"#\" onclick=\"removeJourneyFromGame('" + (object) journeyTile.id_brief_category_tile + "')\"><i class=\"glyphicon glyphicon-remove\"></i></a>";
        }
        else
        {
          str = str + " <a id=\"add_" + (object) journeyTile.id_brief_category_tile + "\" href=\"#\" onclick=\"addJourneyToGame('" + (object) journeyTile.id_brief_category_tile + "')\"><i class=\"glyphicon glyphicon-plus-sign\"></i></a>";
          str = str + " <i style=\"display:none;\"  id=\"done_" + (object) journeyTile.id_brief_category_tile + "\" class=\"glyphicon glyphicon-ok\"></i>";
          str = str + " <a style=\"display:none;\"  id=\"link_" + (object) journeyTile.id_brief_category_tile + "\" href=\"#\" onclick=\"removeJourneyFromGame('" + (object) journeyTile.id_brief_category_tile + "')\"><i class=\"glyphicon glyphicon-remove\"></i></a>";
        }
        str += " </td></tr>";
      }
      return "<table id=\"report-table\" class=\"table table-striped table-bordered dataTable small\" cellspacing=\"0\" width=\"100%\"> <thead>  <tr><th width=\"95%\">Category Tile</th><th width=\"5%\">Action</th> </tr></thead> <tbody>" + str + " </tbody></table>";
    }

    public string addJourneyToGame(int id_gametile, int id_journeytile)
    {
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      return new RoadmapLogic().addJourneyToGame(id_gametile, id_journeytile, int32);
    }

    public string removeJourneyFromGame(int id_gametile, int id_journeytile)
    {
      int int32 = Convert.ToInt32(((UserSession) this.HttpContext.Session.Contents["UserSession"]).id_ORGANIZATION);
      return new RoadmapLogic().removeJourneyFromGame(id_gametile, id_journeytile, int32);
    }
  }
}
