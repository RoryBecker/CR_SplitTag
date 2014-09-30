using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using SP = DevExpress.CodeRush.StructuralParser;

namespace CR_SplitTag
{
    public partial class PlugIn1 : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            registerSplitTag();
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion
        public void registerSplitTag()
        {
            DevExpress.CodeRush.Core.CodeProvider SplitTag = new DevExpress.CodeRush.Core.CodeProvider(components);
            ((System.ComponentModel.ISupportInitialize)(SplitTag)).BeginInit();
            SplitTag.ProviderName = "SplitTag"; // Should be Unique
            SplitTag.DisplayName = "Split Tag";
            SplitTag.CheckAvailability += SplitTag_CheckAvailability;
            SplitTag.PreparePreview += SplitTag_PreparePreview;
            SplitTag.Apply += SplitTag_Apply;
            ((System.ComponentModel.ISupportInitialize)(SplitTag)).EndInit();
        }

        private void SplitTag_CheckAvailability(Object sender, CheckContentAvailabilityEventArgs ea)
        {
            LanguageElement element = ea.Element;
            if ((element as SP.HtmlText) == null)
                return;
            SP.HtmlElement tag = (element.Parent as SP.HtmlElement);
            if (tag == null)
                return;
            ea.Available = true;
        }

        void SplitTag_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
        {
            SP.HtmlElement tag = (SP.HtmlElement)ea.Element.Parent;
            string code = String.Format("</{0}>{1}<{0}>", tag.Name, System.Environment.NewLine);
            ea.AddCodePreview(ea.Selection.StartSourcePoint, code);
        }

        private void SplitTag_Apply(Object sender, ApplyContentEventArgs ea)
        {
            SourceRange range = ea.Element.Parent.Parent.Range;
            SP.HtmlElement tag = (SP.HtmlElement)ea.Element.Parent;
            string code = String.Format("</{0}>{1}<{0}>", tag.Name, System.Environment.NewLine);
            using (ea.TextDocument.NewCompoundAction("Split Tag"))
            {
                ea.TextDocument.InsertText(ea.Selection.StartSourcePoint, code);
                ea.TextDocument.Format(range, true);
            }
        }
    }
}