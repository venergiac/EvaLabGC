using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EVALabGC.ROI;
using Microsoft.Xna.Framework.Graphics;
using EVALabGC.ASL;
using System.Diagnostics;

namespace EVALabGC.Tasks.Forms.Xna
{
    public partial class XnaForm : XYForm
    {

        public XnaForm()
            : base()
        {

        }

        public override void Init(Context context)
        {
            base.Init(context);
            modelViewerControl1.BackColor = this.BackColor;
        }

        protected override ROI.Action MakeAction(int id, string action, string[] actionParameters, bool target)
        {
            return new Action(modelViewerControl1, id, action, actionParameters, target);
        }

        public void DrawActions()
        {
            if ((context == null) || (context.Reader.Status != Reader.ReaderStatus.Started)) return;
            //base.OnPaint(pe);
            //currentStimulus = 0;

            lastDrawTime = currentTime;
            Show(null, (int)(currentX), (int)(currentY), currentTime);
        }

        protected override void InvalidateAction(bool drawing)
        {
            try
            {
                if (drawing)
                {
                    if ((invalidateRadius >= 0) && (targetX >= 0) && (targetY >= 0) && (actionId == stimuli.ActionId))
                    {
                        this.modelViewerControl1.Invalidate(new Rectangle(targetX - invalidateRadius, targetY - invalidateRadius, invalidateRadius * 2, invalidateRadius * 2));
                    }
                    else
                    {
                        actionId = stimuli.ActionId;
                        this.modelViewerControl1.Invalidate();
                    }
                }
                else
                {
                    Show(null, (int)(currentX), (int)(currentY), currentTime);
                }
                targetX = stimuli.TargetX;
                targetY = stimuli.TargetY;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            if ((context == null) || (context.Reader.Status != Reader.ReaderStatus.Started)) return;
            //base.OnPaint(pe);
            //currentStimulus = 0;

            lastDrawTime = currentTime;

            //empty DRAW
        }

    }
}
