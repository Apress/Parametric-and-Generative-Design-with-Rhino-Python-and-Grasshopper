using System;
using SD = System.Drawing;

using Rhino.Geometry;

using Grasshopper.Kernel;
using GH_IO.Serialization;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class ProjectComponentCTX_7626f584 : ProjectComponent_Base, IGH_ContextualComponent, IGH_DocumentOwner
  {
    static readonly string s_scriptDataId = "7626f584-c5f9-4ccc-bd90-9c2112a65c23";
    static readonly string s_scriptIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAARJJREFUSEvtk22NAkEQREsCEpBwDsABSDgJSEACDsABOOAcHA7AATiAvGQq6e2dZQj8IYGXdDbZ6Z7qr5G+fBRjSUdJ63zQYCppK+lf0rXYXtJPdlwGh0cYlYsck+2cAyxwyAcVuNwZnyT9lg4A30uxDrsSwLeFk/krYhkEey3CmSCCW9h3kQ/uwYAfDXK1xMzy4RAeDlvRgvLpcRwo2zcYSx/t3OvdAMRQLUN2rKuaZ2eU7fAMJLVKYh2RVwUirDn3bOLPuHYZ9npShuldH4JKmEevAtQ8LF5nfPbR8KP3CEYQZ8jRr4P3umYumf661z5ztjY2q7rmvDwuQrn6CgOUXkuCuNqrfgpmwWK0ZvLlDbgBjpttnTv6QCIAAAAASUVORK5CYII=";

    public override Guid ComponentGuid { get; } = new Guid("7626f584-c5f9-4ccc-bd90-9c2112a65c23");

    public override GH_Exposure Exposure { get; } = GH_Exposure.primary;

    public override bool Obsolete { get; } = false;

    public ProjectComponentCTX_7626f584() : base(GetResource(s_scriptDataId), s_scriptIconData,
        name: "LSystem2D",
        nickname: "L2D",
        description: @"Rewrites a given 2D L-System for the desired number of iterations and outputs the polyline generated based on the expansion string.",
        category: "LSystem2D_contextual",
        subCategory: "Default"
        )
    {
    }

    protected override void RegisterInputParams(GH_InputParamManager _) { }
    protected override void RegisterOutputParams(GH_OutputParamManager _) { }

    protected override void BeforeSolveInstance() => m_script.BeforeSolve(this);

    protected override void SolveInstance(IGH_DataAccess DA) => m_script.Solve(this, DA);

    protected override void AfterSolveInstance() => m_script.AfterSolve(this);

    public override BoundingBox ClippingBox => m_script.GetClipBox(this);

    public override void DrawViewportWires(IGH_PreviewArgs args) => m_script.DrawWires(this, args);

    public override void DrawViewportMeshes(IGH_PreviewArgs args) => m_script.DrawMeshes(this, args);

    #region IGH_ContextualComponent
    GH_Archive IGH_ContextualComponent.Archive => m_script.Archive;
    #endregion

    #region IGH_DocumentOwner
    GH_Document IGH_DocumentOwner.OwnerDocument() => OnPingDocument();

    void IGH_DocumentOwner.DocumentClosed(GH_Document document)
    {
      // Internal docs are embedded, we don't need to do anything.
    }

    void IGH_DocumentOwner.DocumentModified(GH_Document document)
    {
      // Internal docs are embedded, we don't need to do anything.
    }
    #endregion
  }
}
