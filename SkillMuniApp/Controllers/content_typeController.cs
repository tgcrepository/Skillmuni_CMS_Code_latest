// Decompiled with JetBrains decompiler
// Type: m2ostnext.Controllers.content_typeController
// Assembly: m2ostnext, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 29DFB152-A316-4A1B-BA38-8352D8AD9E56
// Assembly location: C:\Users\xoriant\Downloads\Skillmuni API Project\Dependent Dlls\m2ostnext.dll

using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace m2ostnext.Controllers
{
  public class content_typeController : Controller
  {
    private db_m2ostEntities db = new db_m2ostEntities();

    public ActionResult Index() => (ActionResult) this.View((object) this.db.tbl_content_type.ToList<tbl_content_type>());

    public ActionResult Details(int? id)
    {
      if (!id.HasValue)
        return (ActionResult) new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      tbl_content_type model = this.db.tbl_content_type.Find(new object[1]
      {
        (object) id
      });
      return model == null ? (ActionResult) this.HttpNotFound() : (ActionResult) this.View((object) model);
    }

    public ActionResult Create() => (ActionResult) this.View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "ID_CONTENT_TYPE,TYPE_NAME,TYPE_DESCRIPTION,STATUS,UPDATED_DATE_TIME")] tbl_content_type tbl_content_type)
    {
      if (!this.ModelState.IsValid)
        return (ActionResult) this.View((object) tbl_content_type);
      this.db.tbl_content_type.Add(tbl_content_type);
      this.db.SaveChanges();
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult Edit(int? id)
    {
      if (!id.HasValue)
        return (ActionResult) new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      tbl_content_type model = this.db.tbl_content_type.Find(new object[1]
      {
        (object) id
      });
      return model == null ? (ActionResult) this.HttpNotFound() : (ActionResult) this.View((object) model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "ID_CONTENT_TYPE,TYPE_NAME,TYPE_DESCRIPTION,STATUS,UPDATED_DATE_TIME")] tbl_content_type tbl_content_type)
    {
      if (!this.ModelState.IsValid)
        return (ActionResult) this.View((object) tbl_content_type);
      this.db.Entry<tbl_content_type>(tbl_content_type).State = EntityState.Modified;
      this.db.SaveChanges();
      return (ActionResult) this.RedirectToAction("Index");
    }

    public ActionResult Delete(int? id)
    {
      if (!id.HasValue)
        return (ActionResult) new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      tbl_content_type model = this.db.tbl_content_type.Find(new object[1]
      {
        (object) id
      });
      return model == null ? (ActionResult) this.HttpNotFound() : (ActionResult) this.View((object) model);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      this.db.tbl_content_type.Remove(this.db.tbl_content_type.Find(new object[1]
      {
        (object) id
      }));
      this.db.SaveChanges();
      return (ActionResult) this.RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        this.db.Dispose();
      base.Dispose(disposing);
    }
  }
}
