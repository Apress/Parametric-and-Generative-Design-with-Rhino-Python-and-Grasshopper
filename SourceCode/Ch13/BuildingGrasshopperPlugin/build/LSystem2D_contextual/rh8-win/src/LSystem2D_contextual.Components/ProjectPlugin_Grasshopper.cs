using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Resources;
using SD = System.Drawing;

using Rhino;
using Grasshopper.Kernel;

namespace RhinoCodePlatform.Rhino3D.Projects.Plugin.GH
{
  public sealed class AssemblyInfo : GH_AssemblyInfo
  {
    static readonly string s_assemblyIconData = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAARJJREFUSEvtk22NAkEQREsCEpBwDsABSDgJSEACDsABOOAcHA7AATiAvGQq6e2dZQj8IYGXdDbZ6Z7qr5G+fBRjSUdJ63zQYCppK+lf0rXYXtJPdlwGh0cYlYsck+2cAyxwyAcVuNwZnyT9lg4A30uxDrsSwLeFk/krYhkEey3CmSCCW9h3kQ/uwYAfDXK1xMzy4RAeDlvRgvLpcRwo2zcYSx/t3OvdAMRQLUN2rKuaZ2eU7fAMJLVKYh2RVwUirDn3bOLPuHYZ9npShuldH4JKmEevAtQ8LF5nfPbR8KP3CEYQZ8jRr4P3umYumf661z5ztjY2q7rmvDwuQrn6CgOUXkuCuNqrfgpmwWK0ZvLlDbgBjpttnTv6QCIAAAAASUVORK5CYII=";
    static readonly string s_categoryIconData = "iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAANZJREFUOE/dkaEKAlEQRU8wGA0Ggx9gNPgZBqPRYPBDDAaDoF3/QKOgwShYBKtBUDAqGAwG5cp1eT5WMO+BYXdnZmfuzEBmKAAjoBgHAvJABxgCY78nNIAnUA+dARXgCNyAObCK81VNDiXG5IAzMLPSD12gFH6oQBpVx5pxIGQCHGKnkYIt8ACmLqR9fKGZZL/QDy1g6UIXoB0m7K3iH8pepMZ670QS7z5PzRLVTXvRM0Y72blp4lC10E7AFejbFr79xnGNklxA8/XcLe2M8q8te2CVWeIFOBEwt8sMcqwAAAAASUVORK5CYII=";

    public static readonly SD.Bitmap PluginIcon = default;
    public static readonly SD.Bitmap PluginCategoryIcon = default;

    static AssemblyInfo()
    {
      if (!s_assemblyIconData.Contains("ASSEMBLY-ICON"))
      {
        using (var aicon = new MemoryStream(Convert.FromBase64String(s_assemblyIconData)))
          PluginIcon = new SD.Bitmap(aicon);
      }

      if (!s_categoryIconData.Contains("ASSEMBLY-CATEGORY-ICON"))
      {
        using (var cicon = new MemoryStream(Convert.FromBase64String(s_categoryIconData)))
          PluginCategoryIcon = new SD.Bitmap(cicon);
      }
    }

    public override Guid Id { get; } = new Guid("102fdf7b-486c-4751-a705-14ca1a845458");

    public override string AssemblyName { get; } = "LSystem2D_contextual.Components";
    public override string AssemblyVersion { get; } = "0.1.0";
    public override string AssemblyDescription { get; } = @"2D L-System with Rewrite String and 2D Turtle combined into a single node, using customized contextual inputs/output.";
    public override string AuthorName { get; } = "Isabel Lupiani";
    public override string AuthorContact { get; } = "isabel.lupiani@gmail.com";
    public override GH_LibraryLicense AssemblyLicense { get; } = GH_LibraryLicense.unset;
    public override SD.Bitmap AssemblyIcon { get; } = PluginIcon;
  }

  public class ProjectComponentPlugin : GH_AssemblyPriority
  {
    static readonly Guid s_projectId = new Guid("102fdf7b-486c-4751-a705-14ca1a845458");
    static readonly dynamic s_projectServer = default;
    static readonly object s_project = default;

    static ProjectComponentPlugin()
    {
      s_projectServer = ProjectInterop.GetProjectServer();
      if (s_projectServer is null)
      {
        RhinoApp.WriteLine($"Error loading Grasshopper plugin. Missing Rhino3D platform");
        return;
      }

      // get project
      dynamic dctx = ProjectInterop.CreateInvokeContext();
      dctx.Inputs["projectAssembly"] = typeof(ProjectComponentPlugin).Assembly;
      dctx.Inputs["projectId"] = s_projectId;
      dctx.Inputs["projectData"] = GetProjectData();

      object project = default;
      if (s_projectServer.TryInvoke("plugins/v1/deserialize", dctx)
            && dctx.Outputs.TryGet("project", out project))
      {
        // server reports errors
        s_project = project;
      }
    }

    public override GH_LoadingInstruction PriorityLoad()
    {
      if (AssemblyInfo.PluginCategoryIcon is SD.Bitmap icon)
      {
        Grasshopper.Instances.ComponentServer.AddCategoryIcon("LSystem2D_contextual", icon);
      }
      Grasshopper.Instances.ComponentServer.AddCategorySymbolName("LSystem2D_contextual", "LSystem2D_contextual"[0]);

      return GH_LoadingInstruction.Proceed;
    }

    public static bool TryCreateScript(GH_Component ghcomponent, string serialized, out object script)
    {
      script = default;

      if (s_projectServer is null) return false;

      dynamic dctx = ProjectInterop.CreateInvokeContext();
      dctx.Inputs["component"] = ghcomponent;
      dctx.Inputs["project"] = s_project;
      dctx.Inputs["scriptData"] = serialized;

      if (s_projectServer.TryInvoke("plugins/v1/gh/deserialize", dctx))
      {
        return dctx.Outputs.TryGet("script", out script);
      }

      return false;
    }

    public static void DisposeScript(GH_Component ghcomponent, object script)
    {
      if (script is null)
        return;

      dynamic dctx = ProjectInterop.CreateInvokeContext();
      dctx.Inputs["component"] = ghcomponent;
      dctx.Inputs["project"] = s_project;
      dctx.Inputs["script"] = script;

      if (!s_projectServer.TryInvoke("plugins/v1/gh/dispose", dctx))
        throw new Exception("Error disposing Grasshopper script component");
    }

    static string GetProjectData()
    {
      var rm = new ResourceManager("Plugin.Data", Assembly.GetExecutingAssembly());
      return rm.GetString("PROJECT-DATA");
    }
  }
}
