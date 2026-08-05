using System;
using SD = System.Drawing;
using SWF = System.Windows.Forms;

using Rhino.Geometry;

using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class ProjectComponent_01d2a7f2 : ProjectComponent_Base
  {
    static readonly string s_scriptDataId = "01d2a7f2-b471-40bb-bc6c-be274207fdb6";
    static readonly string s_scriptIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAAKZJREFUSEtjYBgFVAQCDAwM9lBcD8X7kfB/KD2fgYFBAV0zCBgwMDCsR9JwH6qJHDwB3XAQaMCiEB/+wMDAcAAHDkA3HARAPtiAphDEB1kMwg+QDHdA10wN8B7JApoA5KChOgAFCcwCUNBRHdDcggIkC0ARTnWAnIRpYgEo48AsSECXpAYAhTtN88CoBQQBzS0AFXo0Taag4pemPgABUC0FKtJHGAAAtZha29YcuzIAAAAASUVORK5CYII=";

    public override Guid ComponentGuid { get; } = new Guid("01d2a7f2-b471-40bb-bc6c-be274207fdb6");

    public override GH_Exposure Exposure { get; } = GH_Exposure.primary;

    public override bool Obsolete { get; } = false;

    public ProjectComponent_01d2a7f2() : base(GetResource(s_scriptDataId), s_scriptIconData,
        name: "Turtle2D",
        nickname: "T2D",
        description: @"2D turtle graphics object that creates 2D polylines based on input L-System expansion strings. Allows skips but not branching.",
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
