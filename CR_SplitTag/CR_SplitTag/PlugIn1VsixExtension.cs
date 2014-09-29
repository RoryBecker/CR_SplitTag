using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_SplitTag
{
    [Export(typeof(IVsixPluginExtension))]
    public class CR_SplitTagExtension : IVsixPluginExtension { }
}