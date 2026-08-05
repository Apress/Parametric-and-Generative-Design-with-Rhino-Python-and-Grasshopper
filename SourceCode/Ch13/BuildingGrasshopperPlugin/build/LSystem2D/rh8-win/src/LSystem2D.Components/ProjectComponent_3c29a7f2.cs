using System;
using SD = System.Drawing;
using SWF = System.Windows.Forms;

using Rhino.Geometry;

using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class ProjectComponent_3c29a7f2 : ProjectComponent_Base
  {
    static readonly string s_scriptDataId = "3c29a7f2-7571-4c25-be42-7aed232e0e07";
    static readonly string s_scriptIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAAOZJREFUSEvtklENAkEMREcCEpCAA5CABByABByABByAA3AADsABOIC8pEN6ZX/4vGRfsrnsttNtZ0/qdEbLTNK8Hv5BUzuV9JT0LmuXcu6SFmnfgubQNfMIbNN+E2eI4FXiLVahmdQAN9abl+WCSyzjqR2HvaRb2n9xt2YdYgSG7rHJHELD11yL5gtiLDiF6BETZPL4dO/iNMKZXSD2A4UZnyBfEimYyQ94jDwKO5fLsoUDqr8en0L5wZiSs9wpuefGGw6oFwBdURALjC3MPtuaqh+A39VzoMtsFcVsTQZt/ps6nbHwARj+RWld6lorAAAAAElFTkSuQmCC";

    public override Guid ComponentGuid { get; } = new Guid("3c29a7f2-7571-4c25-be42-7aed232e0e07");

    public override GH_Exposure Exposure { get; } = GH_Exposure.primary;

    public override bool Obsolete { get; } = false;

    public ProjectComponent_3c29a7f2() : base(GetResource(s_scriptDataId), s_scriptIconData,
        name: "RewriteString",
        nickname: "RW",
        description: @"Rewriting a given L-System for the desired number of iterations.",
        category: "LSystem2D",
        subCategory: "Default"
        )
    {
    }

    protected override void AppendAdditionalComponentMenuItems(SWF.ToolStripDropDown menu)
    {
      base.AppendAdditionalComponentMenuItems(menu);
      if (m_script is null) return;
      m_script.AppendAdditionalMenuItems(this, menu);
    }

    protected override void RegisterInputParams(GH_InputParamManager _) { }

    protected override void RegisterOutputParams(GH_OutputParamManager _) { }

    protected override void BeforeSolveInstance()
    {
      if (m_script is null) return;
      m_script.BeforeSolve(this);
    }

    protected override void SolveInstance(IGH_DataAccess DA)
    {
      if (m_script is null) return;
      m_script.Solve(this, DA);
    }

    protected override void AfterSolveInstance()
    {
      if (m_script is null) return;
      m_script.AfterSolve(this);
    }

    public override void RemovedFromDocument(GH_Document document)
    {
      ProjectComponentPlugin.DisposeScript(this, m_script);
      base.RemovedFromDocument(document);
    }

    public override BoundingBox ClippingBox
    {
      get
      {
        if (m_script is null) return BoundingBox.Empty;
        return m_script.GetClipBox(this);
      }
    }

    public override void DrawViewportWires(IGH_PreviewArgs args)
    {
      if (m_script is null) return;
      m_script.DrawWires(this, args);
    }

    public override void DrawViewportMeshes(IGH_PreviewArgs args)
    {
      if (m_script is null) return;
      m_script.DrawMeshes(this, args);
    }
  }
}
