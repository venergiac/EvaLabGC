﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Media;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using EVALab.Util.Box;
using System.Collections;

namespace EVALabGC.ROI
{
    public class Action
    {

        public enum ActionType {
            Unknown,
            PutIn,
            PutInAt,
            PlaySound,
            PlaySoundAt,
            InterceptAt
        }

        public enum ActionStatus
        {
            Active,
            Inactive
        }

        //public static SolidBrush redBrush = new SolidBrush(Color.Red);
        private int actionId = 0;

        public int ActionId
        {
            get { return actionId; }
            set { actionId = value; }
        }
        protected ActionType action = ActionType.Unknown;
        protected double x0 = -1;
        protected double y0 = -1;
        protected double x1 = -1;
        protected double y1 = -1;
        protected double x2 = -1;
        protected double y2 = -1;
        protected Bitmap image = null;

        protected double t0 = -1;
        protected double t1 = -1;
        protected double t2 = -1;

        //intercept
        protected double evaluatedError = Double.MaxValue;
        protected double errorMargin = 0;
        protected double timeMargin = 0;

        protected Action subActionOK = null;
        protected Action subActionKO = null;

        protected int evaluate = -1; //0= before, 1=on, 2=after
        protected bool evaluated = false;

        //Sound
        protected SoundPlayer soundPlayer = null;

        protected int targetX = -1;
        protected int targetY = -1;

        protected bool target = false;

        protected Action() { }
        
        public Action(int id, string action, string[] actionParameters, bool target)
        {
            DoParse(id, action, actionParameters, target);
        }

        protected void DoParse(int id, string action, string[] actionParameters, bool target)
        {
            this.actionId = id;
            this.target = target;
            if (action.Equals("PutIn"))
            {
                this.action = ActionType.PutIn;
                ParseParametersForImage(actionParameters);
            }
            else if (action.Equals("PutInAt"))
            {
                this.action = ActionType.PutInAt;
                ParseParametersForImage(actionParameters);
            }
            else if (action.Equals("PlaySound"))
            {
                this.action = ActionType.PlaySound;
                ParseParametersForSound(actionParameters);
            }
            else if (action.Equals("PlaySoundAt"))
            {
                this.action = ActionType.PlaySoundAt;
                ParseParametersForSound(actionParameters);
            }
            else if (action.Equals("InterceptAt"))
            {
                this.action = ActionType.InterceptAt;
                ParseParametersForIntercept(actionParameters);
            }
            else
            {
                throw new Exception("Unrecognized Command " + action);
            }
        }

        public bool IsTarget
        {
            get { return target; }
        }

        public long TimeStart
        {
            get
            {
                return (long)t0;
            }
        }

        public long TimeEnd
        {
            get
            {
                return (long)t2;
            }
        }

        public int TargetX
        {
            get { return targetX; }

        }
        public int TargetY
        {
            get { return targetY; }

        }

        private void ParseParametersForIntercept(string[] actionParameters)
        {
            string path = actionParameters[0];
            path = path.Substring(0, path.Length - 2).Trim();

            errorMargin = Double.Parse(actionParameters[1]);
            timeMargin = Double.Parse(actionParameters[2]);

            //Parse parameters for intercept action
            x0 = Double.Parse(actionParameters[3]);
            y0 = Double.Parse(actionParameters[4]);

            t0 = Double.Parse(actionParameters[5]);
            t1 = Double.Parse(actionParameters[6]);

            x1 = Double.Parse(actionParameters[7]);
            y1 = Double.Parse(actionParameters[8]);

            t2 = Double.Parse(actionParameters[9]);
            x2 = Double.Parse(actionParameters[10]);
            y2 = Double.Parse(actionParameters[11]);
            if (t2 <= t1)
            {
                t2 = t1;
                x2 = x1;
                y2 = y1;
            }
            int nextIdx = 12;
            //Parse for positive intercept
            string strActionOK = actionParameters[nextIdx];
            if (strActionOK.Equals("PutIn"))
            {
                nextIdx++;
                subActionOK = new Action(0, strActionOK, new string[] { path + "\\" + actionParameters[nextIdx++], actionParameters[nextIdx++], actionParameters[nextIdx++] }, false);
            }
            else if (strActionOK.Equals("PlaySound"))
            {
                nextIdx++;
                subActionOK = new Action(0, strActionOK, new string[] { path + "\\" + actionParameters[nextIdx++] }, false);
            }

            //Parse for negative intercept
            string strActionKO = actionParameters[nextIdx];
            if (strActionKO.Equals("PutIn"))
            {
                nextIdx++;
                subActionKO = new Action(0, strActionKO, new string[] { path + "\\" + actionParameters[nextIdx++], actionParameters[nextIdx++], actionParameters[nextIdx++] }, false);
            }
            else if (strActionKO.Equals("PlaySound"))
            {
                nextIdx++;
                subActionKO = new Action(0, strActionKO, new string[] { path + "\\" + actionParameters[nextIdx++] }, false);
            }

            // set target
            targetX = (int)x0;
            targetY = (int)y0;
        }

        private void ParseParametersForSound(string[] actionParameters)
        {
            soundPlayer = new SoundPlayer(actionParameters[0]);
            soundPlayer.Load();
            if (action == ActionType.PlaySoundAt)
            {
                t0 = Double.Parse(actionParameters[1]);
                t1 = Double.Parse(actionParameters[2]);
                t2 = t1;
            }
        }


        protected virtual void MakeTexture(string imagePath)
        {
            image= ImagesCache.Instance.GetImage(imagePath);
        }  

        private void ParseParametersForImage(string[] actionParameters)
        {
            MakeTexture(actionParameters[0]);             
            x0 = Double.Parse(actionParameters[1]);
            y0 = Double.Parse(actionParameters[2]);

            if (action == ActionType.PutInAt)
            {
                t0 = Double.Parse(actionParameters[3]);
                t1 = Double.Parse(actionParameters[4]);
                t2 = t1;
            }

            if (actionParameters.Length > 5)
            {
                x1 = Double.Parse(actionParameters[5]);
                y1 = Double.Parse(actionParameters[6]);
            }

            if (actionParameters.Length > 7)
            {
                t2 = Double.Parse(actionParameters[7]);
                x2 = Double.Parse(actionParameters[8]);
                y2 = Double.Parse(actionParameters[9]);
            }
        }

        public ActionStatus GetStatus(Form owner, double currentTime)
        {
            //Debug.WriteLine("timeStart=" + timeStart + " timeEnd=" + timeEnd + " " + currentTime);
            if ((t0 < 0) || (t2 < 0))
            {
                evaluate = 1;
                return ActionStatus.Active;
            }
            else if ((t0 <= currentTime) && (t2 < 0))
            {
                evaluate = 1;
                return ActionStatus.Active;
            }
            else if ((t0 <= currentTime) && (t2 >= currentTime))
            {
                evaluate = 1;
                return ActionStatus.Active;
            }
            else if ((t0 - timeMargin <= currentTime) && (t2 + timeMargin >= currentTime) && (this.action == ActionType.InterceptAt))
            {
                //before intercept 
                evaluate = 0;
                if (currentTime > t2)
                {
                    evaluate = 2;
                }
                Debug.WriteLine("Setting evaluate=" + evaluate + " " + (currentTime >= t0) + "  " + t0 + "-" + timeMargin + " <" + currentTime + "<" + t0 + " << " + t2);
                
                return ActionStatus.Active;
            }
            else
            {
                evaluate = -1;
                evaluated = false;
                return ActionStatus.Inactive;
            }
        }

        private int GetX(double currentTime)
        {
            if (x1 < 0) return (int)x0;
            double percTime = ((double)currentTime- (double)t0) / ((double)t1 - (double)t0);
            if (percTime <= 0) return (int)x0;

            if ((x2 < 0) || (x2 <= x1) || (currentTime<=t1))
            {
                return (int)Math.Round((x1 - x0) * percTime + x0);
            }
            //second block a0
            double v0 = (x1 - x0) / (t1 - t0);
            double a0 = (x2 - x0 - v0 * (t2 - t0)) / ((t2 - t1) * (t2 - t1));
            if (a0 == 0)
            {
                return (int)x1;
            }
             double dtime = (double)currentTime- (double)t1;

             return (int)Math.Round(dtime * dtime * a0 + v0 * dtime + x1);
        }

        private int GetY(double currentTime)
        {
            if (y1 < 0) return (int)y0;
            double percTime = ((double)currentTime - (double)t0) / ((double)t1 - (double)t0);
            if (percTime <= 0) return (int)y0;

            if ((y2 < 0) || (y2 <= y1) || (currentTime <= t1))
            {
                return (int)Math.Round((y1 - y0) * percTime + y0);
            }
            //second block a0
            double v0 = (y1 - y0) / (t1 - t0);
            double a0 = (y2 - y0 - v0 * (t2 - t0)) / ((t2 - t1) * (t2 - t1));
            
            if (a0 == 0)
            {
                return (int)y1;
            }
            double dtime = (double)currentTime - (double)t1;
            
            return (int)Math.Round(dtime * dtime * a0 + v0 * dtime + y1);
        }

        public bool active = false;
        public bool painting = false;
        
        public void Do(Graphics myGraphics, int currentX, int currentY, double currentTime)
        {
            targetX = GetX(currentTime);
            targetY = GetY(currentTime);

            if ((this.action == ActionType.InterceptAt) && (!evaluated)) {
                EvaluateIntercept(myGraphics, currentX, currentY, currentTime,targetX, targetY);
#if DEBUG
            myGraphics.DrawRectangle(new Pen( Color.Blue ), targetX-5, targetY-5, 10, 10);
            myGraphics.DrawRectangle(new Pen(Color.Red), currentX-5, currentY-5, 10, 10);
            //Debug.WriteLine("#EvaluateIntercept x=" + currentX + " y=" + currentY + " " + this.timeStart + ">"+ currentTime + ">" + this.timeEnd);
#endif
            }
            
            if (painting) return;
            painting = true;
            DrawTexture(myGraphics);

            if ((soundPlayer != null) && (!active))
            {
                active = true;
                soundPlayer.Play();
            }
            painting = false;
        }

        protected virtual void DrawTexture(Graphics myGraphics) {
            Debug.WriteLine("DrawTexture");
            if ((myGraphics == null) || (image == null)) return;
            myGraphics.DrawImage(image, new Rectangle(targetX, targetY, image.Width, image.Height));
        }

        private void EvaluateIntercept(Graphics myGraphics, int currentX, int currentY, double currentTime, int targetX, int targetY)
        {
            try
            {
                if (targetX < 0)
                {
                    return;
                }
                if (targetY < 0)
                {
                    return;
                }

                double currentError = Math.Abs(currentX - targetX) + Math.Abs(currentY - targetY);
                //after || before
                Debug.WriteLine(this.actionId + " " + evaluate + " " + errorMargin + " Intercept error=" + currentError +"(min=" + evaluatedError + ") " + currentTime + " " + this.t0 + " " + this.t2);

                //during
                if (evaluate == 1)
                {
                    evaluatedError = Math.Min(currentError, evaluatedError);
                    return;
                }

                
                if (((evaluatedError > errorMargin) && (evaluate == 2)) || ((currentError <= errorMargin) && (evaluate == 0)))
                {
                    evaluated = true;
                    Debug.WriteLine("KO");
                    subActionKO.Do(myGraphics, currentX, currentY, currentTime);
                }
                else if (evaluate == 2)
                {
                    evaluated = true;
                    Debug.WriteLine("OK");
                    subActionOK.Do(myGraphics, currentX, currentY, currentTime);
                }
            }
            catch (Exception e)
            {
                ExceptionForm.Show(null, "Error due to: currentX=" + currentX + " targetX=" + targetX + "currentY=" + currentY + " targetY=" + targetY, e);
            }
        }
    }


    public class ImagesCache
    {
        private Hashtable images = new Hashtable(10);

        private static ImagesCache singletone = new ImagesCache();
        public static ImagesCache Instance
        {
            get
            {
                return singletone;
            }
        }

        public Bitmap GetImage(string path)
        {
            Bitmap image = (Bitmap)images[path];
            if (image!=null) return image;
            
            image = (Bitmap)Bitmap.FromFile(path);
            images.Add(path, image);
            return image;
        }
     }
}
